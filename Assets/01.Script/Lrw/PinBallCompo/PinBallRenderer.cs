using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    public class PinBallRenderer : MonoBehaviour
    {
        private PinBallCompo.PinBall _pinBall;
        private LineRenderer _lineRenderer;
        private SpriteRenderer _spriteRenderer;
        private bool _showShootingUI = false;
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _lineRenderer = GetComponent<LineRenderer>();
            _pinBall = transform.parent.GetComponent<PinBallCompo.PinBall>();
        }

        private void Update()
        {
            RotationSprite();
            DrawLine();
        }

        private void RotationSprite()
        {
            float a = Mathf.Atan2(_pinBall.Rigid.linearVelocity.y, _pinBall.Rigid.linearVelocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0,0,a);

        }

        private void DrawLine()
        {
            if (_showShootingUI)
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, _pinBall.InputSo.MousePos);
            }
            else
            {
                _lineRenderer.SetPositions(new Vector3[] { Vector3.zero, Vector3.zero });
            }
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

