using System.Collections.Generic;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(PinBallBase))]
    public class PinBallDrawShootLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private PinBallBase _pinBallBase;
        [SerializeField,Range(1,10)] private float drawDistance = 1;
        [SerializeField] private LayerMask ObstacleLayer;
        private void Awake()
        {
            _pinBallBase =  GetComponent<PinBallBase>();
            _lineRenderer = GetComponent<LineRenderer>();
            EventBus<MousePosEvent>.OnEvent += DrawLine;
        }

        private void DrawLine(MousePosEvent mousePos)
        {
            if (GameManager.Instance.state == PinBallStates.Shooting || _pinBallBase.IsEnd)
            {
                _lineRenderer.positionCount = 0;
                return;
            }
            Vector2 mouseDir = (mousePos.RealPos - (Vector2)transform.position).normalized;
            Vector2 gravity = Vector2.down * (9.8f * _pinBallBase.PinBallSo.Mass);
            List<Vector3> drawPoints = new List<Vector3>();
            float t = 0;
            float drawLength = 0;
            Vector2 lastDrawPoint = transform.position;
            while (true)
            {
                Vector2 drawPoint = GetLinePos(transform.position, mouseDir, gravity,_pinBallBase.PinBallSo.BallShootPower,t);
                drawPoints.Add(drawPoint);
                drawLength += (drawPoint - lastDrawPoint).magnitude;
                t += Time.fixedDeltaTime;
                if(drawLength >= drawDistance) break;
                Vector2 drawVec2 = drawPoint - lastDrawPoint;
                if(Physics2D.Raycast(lastDrawPoint, drawVec2.normalized, drawVec2.magnitude, ObstacleLayer).collider) break;
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