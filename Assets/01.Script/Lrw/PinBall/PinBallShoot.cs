using Lrw_Input;
using UnityEngine;
using UnityEngine.Rendering;

namespace Lrw_PinBall
{
    public class PinBallShoot : MonoBehaviour
    {
        [SerializeField] private InputSO inputSO;
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();

        }

        private void Update()
        {
            _lineRenderer.SetPosition(0,transform.position);
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(inputSO.MousePos); 
            _lineRenderer.SetPosition(1, MousePos);
        }


    }
}

