using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallCompo.FSM;
using Lrw_CustomReadonly;
using Lrw_Input;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour,ICanTriggerEvent
    {
        [SerializeField] private PinBallSO pinBallSo;
        [field:SerializeField] public InputSO InputSo { get; private set; }
        
        public Rigidbody2D Rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallDrawShootLine _pinBallDrawShootLine;
        [field:SerializeField,ReadOnly] public float Damage { get; private set; }
        private PinBallBrain pinBallBrain;
        private void Awake()
        {
            pinBallBrain = new PinBallBrain(this);
            Rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = transform.GetChild(0).GetComponent<PinBallRenderer>();
            _pinBallDrawShootLine = GetComponent<PinBallDrawShootLine>();
            
            Damage = pinBallSo.BaseDamage;//임시
        }

        private void Start()
        {
            SetPinBallSo(pinBallSo);
            EventBus<AddNeedTriggerCountEvent>.Raise(new AddNeedTriggerCountEvent(1));
        }

        private void Update()
        {
            pinBallBrain.Update();
            EventBus<MousePosEvent>.Raise(new MousePosEvent(InputSo.MouseScreenPos,InputSo.MousePos));
        }

        private void FixedUpdate()
        {
            pinBallBrain.FixedUpdate();
        }

        public void SetPinBallSo(PinBallSO a)
        {
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
        }
    }
}