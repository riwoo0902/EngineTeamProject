using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using UnityEngine;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public class MousePosEvent : IEvent
    {
        public MousePosEvent(Vector2 mousePos)
        {
            RealPos = Camera.current.ScreenToWorldPoint(mousePos);
            ScreenPos = mousePos;
        }
        public Vector2 RealPos {get; private set;}
        public Vector2 ScreenPos {get; private set;}
    }
}