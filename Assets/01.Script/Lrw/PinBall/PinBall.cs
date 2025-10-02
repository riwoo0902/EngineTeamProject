using Lrw_Input;
using UnityEngine;

namespace Lrw_PinBall
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PinBall : MonoBehaviour
    {
        [field:SerializeField] public InputSO inputSO { get; private set; }
        public Rigidbody2D _rigid { get; private set; }
        private PinBallRenderer _pinBallRenderer;
        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _pinBallRenderer = GetComponentInChildren<PinBallRenderer>();
            

            SetMouseEvent();
        }

        private void SetMouseEvent()
        {
            inputSO.OnMousePress += () =>
            {
                _pinBallRenderer.SetShowShootingUI(true);
            };
            inputSO.OnMouseReleas += () =>
            {
                _pinBallRenderer.SetShowShootingUI(false);

            };

            if (TryGetComponent(out PinBallShoot pinBallShoot))
            {
                inputSO.OnMouseReleas += () =>
                {
                    pinBallShoot.Shoot();

                };
            }
        }

        


    }
}