using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using Lrw_CustomReadonly;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour,ICanTriggerEvent
    {
        [field:SerializeField] public PinBallSO PinBallSo { get; private set; }
        
        public Rigidbody2D Rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallDrawShootLine _pinBallDrawShootLine;
        [field:SerializeField,ReadOnly] public float Damage { get; private set; }
        private PinBallMachine _pinBallFsmMachine;
        
        private void Awake()
        {
            Rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = transform.GetChild(0).GetComponent<PinBallRenderer>();
            Damage = PinBallSo.BaseDamage;//임시
            
        }

        public void PinBallShoot()
        {
            if (GameManager.Instance.state == PinBallStates.Idle)
            {
                GameManager.Instance.state = PinBallStates.Shooting;
                Vector2 force  = Vector2.zero;
                Rigid.AddForce(force, ForceMode2D.Impulse);
                _pinBallFsmMachine.ChangeState(PinBallStates.Shooting);
            }
            
        }
        
        private void CreatPinBAllBrain()
        {
            _pinBallFsmMachine = new PinBallMachine(this);
        }

        private void Start()
        {
            GameManager.Instance.InputSo.OnMousePress += PinBallShoot;
            CreatPinBAllBrain();
            SetPinBallSo(PinBallSo);
            EventBus<AddNeedTriggerCountEvent>.Raise(new AddNeedTriggerCountEvent(1));
        }

        private void Update()
        {
            _pinBallFsmMachine.Update();
        }

        private void FixedUpdate()
        {
            _pinBallFsmMachine.FixedUpdate();
        }

        public void SetPinBallSo(PinBallSO a)
        {
            PinBallSo = a;
            Rigid.gravityScale = a.Mass;
            _pinBallRenderer.SetSprite(a.PinBallImage);
            Rigid.sharedMaterial.friction = a.Friction;
            Rigid.sharedMaterial.bounciness = a.Bounciness;
            Rigid.linearDamping = a.BallLinearDamping;
        }

        public float Score { get; set; }

        public float GetScore()
        {
            return Score;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            EventBus<OrdHitEvent>.Raise(new OrdHitEvent(other.collider,Damage));
            Score += Damage;
        }

        private void OnDestroy()
        {
            GameManager.Instance.InputSo.OnMousePress -= PinBallShoot;
        }
    }
}