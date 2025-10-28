using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using Lrw_CustomReadonly;
using Lrw_Input;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour,ICanTriggerEvent
    {
        [SerializeField] private PinBallSO pinBallSO;
        [field:SerializeField] public InputSO inputSO { get; private set; }
        
        public Rigidbody2D _rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallShoot _pinBallShoot;
        [field:SerializeField,ReadOnly] public float Damage { get; private set; }
        
        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = transform.GetChild(0).GetComponent<PinBallRenderer>();
            _pinBallShoot = GetComponent<PinBallShoot>();
            
            Damage = pinBallSO.BaseDamage;//임시
        }

        private void Start()
        {
            SetPinBallSo(pinBallSO);
            EventBus<AddNeedTriggerCountEvent>.Raise(new AddNeedTriggerCountEvent(1));
        }

        private void Update()
        {
            EventBus<MousePosEvent>.Raise(new MousePosEvent(inputSO.));
        }

        public void SetPinBallSo(PinBallSO a)
        {
            _rigid.gravityScale = a.Mass;
            _pinBallRenderer.SetSprite(a.PinBallImage);
            _rigid.sharedMaterial.friction = a.Friction;
            _rigid.sharedMaterial.bounciness = a.Bounciness;
            _rigid.linearDamping = a.BallLinearDamping;
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