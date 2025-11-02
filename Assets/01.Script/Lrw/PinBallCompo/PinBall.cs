using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using _01.Script.Lrw.PinBallCompo.FSM.PinBallState;
using Lrw_CustomReadonly;
using Lrw_Input;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour,ICanTriggerEvent,IPinBallContext
    {
        [field:SerializeField] public PinBallSO PinBallSo { get; private set; }
        [field:SerializeField] public InputSO InputSo { get; private set; }
        
        public Rigidbody2D Rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallDrawShootLine _pinBallDrawShootLine;
        [field:SerializeField,ReadOnly] public float Damage { get; private set; }
        private PinBallMachine _PinBallFsmMachine;
        
        private void Awake()
        {
            CreatPinBAllBrain();
            Rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = transform.GetChild(0).GetComponent<PinBallRenderer>();
            _pinBallDrawShootLine = GetComponent<PinBallDrawShootLine>();
            
            Damage = PinBallSo.BaseDamage;//임시
        }
        
        
        #region IPinBallContext
        public Transform PinBallContextTransform { get; private set; }
        public Rigidbody2D PinBallContextRigidbody { get; private set;}
        #endregion
        private void CreatPinBAllBrain()
        {
            PinBallContextTransform = transform;
            PinBallContextRigidbody = gameObject.GetComponent<Rigidbody2D>();
            _PinBallFsmMachine = new PinBallMachine(this);
            
        }

        private void Start()
        {
            SetPinBallSo(PinBallSo);
            EventBus<AddNeedTriggerCountEvent>.Raise(new AddNeedTriggerCountEvent(1));
        }

        private void Update()
        {
            _PinBallFsmMachine.Update();
            
        }

        private void FixedUpdate()
        {
            _PinBallFsmMachine.FixedUpdate();
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

        
    }
}