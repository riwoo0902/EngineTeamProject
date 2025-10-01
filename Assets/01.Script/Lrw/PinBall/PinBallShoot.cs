using UnityEngine;

namespace Lrw_PinBall
{
    [RequireComponent(typeof(PinBall))]
    public class PinBallShoot : MonoBehaviour
    {
        [SerializeField] private float ShootPower = 10;
        private PinBall _pinBall;

        private void Awake()
        {
            _pinBall = GetComponent<PinBall>();
        }

        public void Shoot()
        {
            _pinBall._rigid.AddForce((_pinBall.inputSO.MousePos - (Vector2)transform.position).normalized * ShootPower * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        private void FixedUpdate()
        {
            Shoot();
        }


    }
}

