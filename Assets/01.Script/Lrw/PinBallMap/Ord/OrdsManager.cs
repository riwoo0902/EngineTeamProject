using System;
using System.Collections.Generic;
using System.Linq;
using _01.Script.Lrw.EventBus.CoreSystem;
using _01.Script.Lrw.EventBus.CoreSystem.Events;
using _01.Script.Lrw.EventBus.EventBus.CoreSystem;
using Lrw_Ord;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord
{ 
    public class OrdsManager : MonoBehaviour
    {
        private Dictionary<Collider2D,IOrd> _ords = new Dictionary<Collider2D,IOrd>();

        private void Start()
        {
            _ords = GetComponentsInChildren<IOrd>(true).ToDictionary(ord => ord.Collider);
            EventBus<OrdHitEvent>.OnEvent += OrdHit;
        }

        private void OnDestroy()
        {
            EventBus<OrdHitEvent>.OnEvent -= OrdHit;
        }

        public void OrdHit(OrdHitEvent ordHitEvent)
        {
            if(_ords.TryGetValue(ordHitEvent.MyCollider2D,out IOrd ord))
            {
                ord.Hit(ordHitEvent.Damage);
                Debug.Log(ordHitEvent.MyCollider2D.name + " hit\n" + ordHitEvent.Damage);
            }
        }
    
    }
}
