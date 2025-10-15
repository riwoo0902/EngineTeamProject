using Lrw_Input;
using UnityEngine;

namespace Lrw_PinBall
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour
    {
        [SerializeField] private PinBallSO pinBallSO;
        [field:SerializeField] public InputSO inputSO { get; private set; }
        
        public Rigidbody2D _rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private PinBallShoot _pinBallShoot;
        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = transform.GetChild(0).GetComponent<PinBallRenderer>();
            _pinBallShoot = GetComponent<PinBallShoot>();

            
        }

        private void Start()
        {
            SetPinBallSO(pinBallSO);
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
        private void OnDisable()
        {
            #region OnDisableMouseEvent
            inputSO.OnMousePress -= () =>
            {
                _pinBallRenderer.SetShowShootingUI(true);
            };
            inputSO.OnMouseReleas -= () =>
            {
                _pinBallRenderer.SetShowShootingUI(false);
                _pinBallShoot.Shoot();

            };
            #endregion
        }


        public void SetPinBallSO(PinBallSO a)
        {
            _rigid.gravityScale = a.Mass;
            _pinBallRenderer.SetSprite(a.PinBallImage);
            _rigid.sharedMaterial.friction = a.Friction;
            _rigid.sharedMaterial.bounciness = a.Bounciness;
            _rigid.linearDamping = a.BallLinearDamping;
        }
        


    }
}