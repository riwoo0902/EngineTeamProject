using Lrw_Input;
using UnityEngine;

namespace Lrw_PinBall
{
    public class PinBallRenderer : MonoBehaviour
    {
        private PinBall _pinBall;
        private LineRenderer _lineRenderer;
        private SpriteRenderer _spriteRenderer;
        private bool ShowShootingUI = false;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _lineRenderer = GetComponent<LineRenderer>();
            _pinBall = transform.parent.GetComponent<PinBall>();
        }

        private void Update()
        {
            RotationSprite();
            DrawLine();
        }

        private void RotationSprite()
        {
            float a = Mathf.Atan2(_pinBall._rigid.linearVelocity.y, _pinBall._rigid.linearVelocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0,0,a);

        }

        private void DrawLine()
        {
            if (ShowShootingUI)
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _pinBall.inputSO.MousePos);
            }
            else
            {
                _lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            }
        }

        public void SetShowShootingUI(bool value)
        {
            ShowShootingUI = value;
        }

        public void SetSprite(Sprite a)
        {
            _spriteRenderer.sprite = a;
        }


    }
}

