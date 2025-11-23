using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.PinBallCompo;
using UnityEngine;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public class OrdHitEvent : IEvent
    {
        public OrdHitEvent(Collider2D coll,int damage,PinBallBase ball)
        {
            MyCollider2D = coll;
            Damage = damage;
            PinBallBase =  ball;
        }
        public Collider2D MyCollider2D {get; private set;}
        public int Damage {get; private set;}
        public PinBallBase PinBallBase {get; private set;}
    }
}