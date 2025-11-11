using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    public class PinBallRenderer : MonoBehaviour
    {
        private PinBall _pinBall;
        private SpriteRenderer _spriteRenderer;
        private bool _showShootingUI = false;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _pinBall = transform.parent.GetComponent<PinBall>();
        }

        private void Update()
        {
            RotationSprite();
        }

        private void RotationSprite()
        {
            float a = Mathf.Atan2(_pinBall.Rigid.linearVelocity.y, _pinBall.Rigid.linearVelocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0,0,a);

        }

        public void SetShowShootingUI(bool value)
        {
            _showShootingUI = value;
        }

        public void SetSprite(Sprite a)
        {
            _spriteRenderer.sprite = a;
        }


    }
}

