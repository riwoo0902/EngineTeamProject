using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using UnityEngine;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public class OrdDestoryEvent : IEvent
    {
        public OrdDestoryEvent(Collider2D coll)
        {
            MyCollider2D = coll;
        }
        public Collider2D MyCollider2D {get; private set;}
    }
}