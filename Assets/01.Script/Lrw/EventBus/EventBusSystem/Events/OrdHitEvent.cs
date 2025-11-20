using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using UnityEngine;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public class OrdHitEvent : IEvent
    {
        public OrdHitEvent(Collider2D coll,int damage)
        {
            MyCollider2D = coll;
            Damage = damage;
        }
        public Collider2D MyCollider2D {get; private set;}
        public int Damage {get; private set;}
    }
}