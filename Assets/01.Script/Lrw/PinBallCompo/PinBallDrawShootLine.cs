using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(PinBall))]
    public class PinBallDrawShootLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private PinBall _pinBall;
        [SerializeField,Range(1,10)] private float drawDistance = 1;
        [SerializeField,Range(0.1f,1)] private float drawPintDistance = 0.2f;
        private int _drawPointAmount => (int)(drawDistance / drawPintDistance);
        
        private void Awake()
        {
            _pinBall =  GetComponent<PinBall>();
            _lineRenderer = GetComponent<LineRenderer>();
            EventBus<MousePosEvent>.OnEvent += DrawLine;
        }

        private void DrawLine(MousePosEvent mousePos)
        {
            Vector3 mouseDir = (mousePos.RealPos - (Vector2)transform.position).normalized;
            Vector3 gravity = Physics.gravity * _pinBall.PinBallSo.Mass;
            Vector3[] drawPoints = new Vector3[_drawPointAmount];
            Vector3 moveValue = transform.position + (gravity * drawPintDistance);
            for (int i = 0; i < _drawPointAmount; i++)
            {
                drawPoints[i] = transform.position + (mouseDir * (drawPintDistance * i * _pinBall.PinBallSo.BallShootPower)) +
                                (gravity * (Mathf.Pow(i*drawPintDistance, 1) * 0.5f));
            }
            _lineRenderer.positionCount = drawPoints.Length;
            _lineRenderer.SetPositions(drawPoints);


        }
           
    }
}