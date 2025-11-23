using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallCompo;
using Lrw_Ord;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord.Ords
{
    public class ResetOrb : OrdBase
    {
        protected override void Awake()
        {
            base.Awake();
            OnDestroyEvent += Reset;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnDestroyEvent -= Reset;
        }

        private void Reset(PinBallBase a)
        {
            EventBus<OrbMapReset>.Raise(new OrbMapReset(this));
        } 
    }
}