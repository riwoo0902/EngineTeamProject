using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    [RequireComponent(typeof(PinBall))]
    public class PinBallDrawShootLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        
        
        private void Awake()
        {
            EventBus<MousePosEvent>.OnEvent += DrawLine;
        }

        private void DrawLine(MousePosEvent mousePos)
        {
            
        }
        


    }
}

