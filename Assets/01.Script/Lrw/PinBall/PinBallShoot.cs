using UnityEngine;

namespace _01.Script.Lrw.PinBall
{
    [RequireComponent(typeof(Lrw_PinBall.PinBall))]
    public class PinBallShoot : MonoBehaviour
    {
        [SerializeField] private float shootPower = 10;
        public bool canShoot = true;
        private Lrw_PinBall.PinBall _pinBall;

        private void Awake()
        {
            _pinBall = GetComponent<Lrw_PinBall.PinBall>();
        }

        public void Shoot()
        {
            if (canShoot)
                _pinBall._rigid.AddForce((_pinBall.inputSO.MousePos - (Vector2)transform.position).normalized * shootPower, ForceMode2D.Impulse);
        }


    }
}

