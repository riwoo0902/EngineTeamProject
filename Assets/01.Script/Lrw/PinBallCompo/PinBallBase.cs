using System.Collections;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.PinBallCompo.FSM.PinBallState;
using _01.Script.Lrw.PinBallMap;
using _01.Script.Lrw.UI;
using _01.Script.Lrw.UI.PinBalls;
using Lrw_CustomReadonly;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PinBallBase : MonoBehaviour,ICanTriggerEvent
    {
        [field:SerializeField] public PinBallSO PinBallSo { get; private set; }
        public Rigidbody2D Rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallDrawShootLine _pinBallDrawShootLine;
        [field:SerializeField,ReadOnly] public int Damage { get; private set; }
        private FsmBrain _pinBallFsmMachine;
        public float BaseDamage { get; private set; }
        public bool IsEnd { get; private set; } = false;

        private void Awake()
        {
            Rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = GetComponentInChildren<PinBallRenderer>();
            BaseDamage = PinBallSo.BaseDamage;
            SetDamage();
            if (GameManager.Instance.state == PinBallStates.None) GameManager.Instance.state = PinBallStates.Idle;
        }

        protected virtual void SetDamage()
        {
            Damage = PinBallSo.BaseDamage;
        }

        public void PinBallShoot()
        {
            if (GameManager.Instance.state == PinBallStates.Idle && !IsEnd && MouseCheckUI.Instance.MouseOn)
            {
                GameManager.Instance.state = PinBallStates.Shooting;
                Vector2 force = (GameManager.Instance.InputSo.MousePos - (Vector2)transform.position).normalized *
                                PinBallSo.BallShootPower;
                Rigid.AddForce(force, ForceMode2D.Impulse);
                _pinBallFsmMachine.ChangeState(PinBallStates.Shooting);
            }
        }
        
        private void CreatPinBallBrain()
        {
            _pinBallFsmMachine = new FsmBrain();
            _pinBallFsmMachine.AddState(PinBallStates.Idle,new PinBallIdleState(this));
            _pinBallFsmMachine.AddState(PinBallStates.Shooting,new PinBallShootingState(this));
            _pinBallFsmMachine.SetState(PinBallStates.Idle);
        }

        protected virtual void Start()
        {
            GameManager.Instance.InputSo.OnMousePress += PinBallShoot;
            CreatPinBallBrain();
            SetPinBallSo(PinBallSo);
            EventBus<AddNeedTriggerCountEvent>.Raise(new AddNeedTriggerCountEvent(1));
        }

        protected virtual void Update()
        {
            _pinBallFsmMachine.Update();
        }

        protected virtual void FixedUpdate()
        {
            _pinBallFsmMachine.FixedUpdate();
        }

        public void SetPinBallSo(PinBallSO a)
        {
            Rigid = GetComponent<Rigidbody2D>();
            PinBallSo = a;
            Rigid.gravityScale = a.Mass;
            _pinBallRenderer = GetComponentInChildren<PinBallRenderer>();
            _pinBallRenderer.SetSprite(a.PinBallImage);
            Rigid.sharedMaterial.friction = a.Friction;
            Rigid.sharedMaterial.bounciness = a.Bounciness;
            Rigid.linearDamping = a.BallLinearDamping;
        }

        public int Score { get; set; }

        public int GetScore()
        {
            StartCoroutine(ActiveFalse());
            return Score;
        }

        private IEnumerator ActiveFalse()
        {
            IsEnd = true;
            yield return new WaitForSeconds(1);
            PinBallUIManager.Instance.UsePinBall();
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            EventBus<OrdHitEvent>.Raise(new OrdHitEvent(other.collider,Damage));
            Score += Damage;
            EventBus<ScoreAddEvent>.Raise(new ScoreAddEvent(Damage));
        }

        private void OnDisable()
        {
            GameManager.Instance.InputSo.OnMousePress -= PinBallShoot;
        }
    }
}