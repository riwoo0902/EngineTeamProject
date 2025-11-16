using System.Collections.Generic;
using System.Linq;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using Lrw_Ord;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord
{ 
    public class  OrdsManager : MonoBehaviour
    {
        private Dictionary<Collider2D,IOrd> _ords = new Dictionary<Collider2D,IOrd>();

        private void Start()
        {
            _ords = GetComponentsInChildren<IOrd>(true).ToDictionary(ord => ord.Collider);
            EventBus<OrdHitEvent>.OnEvent += OrdHit;
            EventBus<OrdDestoryEvent>.OnEvent += OrdDestroy;
        }

        private void OnDestroy()
        {
            EventBus<OrdHitEvent>.OnEvent -= OrdHit;
            EventBus<OrdDestoryEvent>.OnEvent -= OrdDestroy;
        }

        public void OrdHit(OrdHitEvent ordHitEvent)
        {
            if(_ords.TryGetValue(ordHitEvent.MyCollider2D,out IOrd ord))
            {
                ord.Hit(ordHitEvent.Damage);
            }
        }

        public void OrdDestroy(OrdDestoryEvent ordDestoryEvent)
        {
            _ords.Remove(ordDestoryEvent.MyCollider2D);
        }

        public void ReSet(IOrd noResetOrb)
        {
            foreach (IOrd ord in _ords.Values)
            {
                if(noResetOrb != ord)
                    ord.ReSet();
            }
        }
    
    }
}
