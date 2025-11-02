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
            EventBus<MousePosEvent>.OnEvent += DrawLine;
        }

        private void DrawLine(MousePosEvent mousePos)
        {
            Vector2 mouseDir = (mousePos.RealPos - (Vector2)transform.position).normalized;
            Vector2 Gravity = Physics2D.gravity * _pinBall.PinBallSo.Mass;
            Vector2[] drawPoints = new Vector2[_drawPointAmount];
        
        


        }
        


    }
}