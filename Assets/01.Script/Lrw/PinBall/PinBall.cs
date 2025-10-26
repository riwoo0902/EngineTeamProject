using _01.Script.Lrw.EventBus.CoreSystem;
using _01.Script.Lrw.EventBus.CoreSystem.Events;
using _01.Script.Lrw.EventBus.EventBus.CoreSystem;
using Lrw_CustomReadonly;
using Lrw_Input;
using UnityEngine;

namespace Lrw_PinBall
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour,ICanTriggerEvent
    {
        [SerializeField] private PinBallSO pinBallSO;
        [field:SerializeField] public InputSO inputSO { get; private set; }
        
        public Rigidbody2D _rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallShoot _pinBallShoot;
        [SerializeField,ReadOnly] public float Damage { get; private set; }
        
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
        }
        private void OnEnable()
        {
            #region OnEnableMouseEvent
            inputSO.OnMousePress += () =>
            {
                _pinBallRenderer.SetShowShootingUI(true);
            };
            inputSO.OnMouseReleas += () =>
            {
                _pinBallRenderer.SetShowShootingUI(false);
                _pinBallShoot.Shoot();
            };
            #endregion 
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