using System.Collections.Generic;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(PinBall))]
    public class PinBallDrawShootLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private PinBall _pinBall;
        [SerializeField,Range(1,10)] private float drawDistance = 1;
        [SerializeField] private LayerMask ordLayer;
        private void Awake()
        {
            _pinBall =  GetComponent<PinBall>();
            _lineRenderer = GetComponent<LineRenderer>();
            EventBus<MousePosEvent>.OnEvent += DrawLine;
        }

        private void DrawLine(MousePosEvent mousePos)
        {
            Vector2 mouseDir = (mousePos.RealPos - (Vector2)transform.position).normalized;
            Vector2 gravity = Vector2.down * (9.8f * _pinBall.PinBallSo.Mass);
            List<Vector3> drawPoints = new List<Vector3>();
            float t = 0;
            float drawLength = 0;
            Vector2 lastDrawPoint = transform.position;
            while (true)
            {
                Vector2 drawPoint = GetLinePos(transform.position, mouseDir, gravity,_pinBall.PinBallSo.BallShootPower,t);
                drawPoints.Add(drawPoint);
                drawLength += (drawPoint - lastDrawPoint).magnitude;
                t += Time.fixedDeltaTime;
                if(drawLength >= drawDistance) break;
                Vector2 drawVec2 = drawPoint - lastDrawPoint;
                if(Physics2D.Raycast(lastDrawPoint, drawVec2.normalized, drawVec2.magnitude, ordLayer).collider) break;
                lastDrawPoint = drawPoint;
            }
            _lineRenderer.positionCount = drawPoints.Count;
            _lineRenderer.SetPositions(drawPoints.ToArray());
        }

        private static Vector2 GetLinePos(Vector2 startPos,Vector2 moveDir,Vector2 gravity,float power,float time)
        {
            Vector2 gValue = gravity * (0.5f * time * time);
            Vector2 moveValue = moveDir * (power * time);
            return startPos + gValue +  moveValue;
        }

        private void OnDestroy()
        {
            EventBus<MousePosEvent>.OnEvent -= DrawLine;
        }
        
    }
}