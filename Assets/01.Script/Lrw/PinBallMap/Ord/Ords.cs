using System.Collections.Generic;
using System.Linq;
using Lrw_Ord;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.Ord
{ 
    public class Ords : MonoBehaviour
    {
        private Dictionary<Collider2D,IOrd> _ords = new Dictionary<Collider2D,IOrd>();
        private void Awake()
        {
            _ords = GetComponentsInChildren<IOrd>(true).ToDictionary(ord => ord.Collider);
        }

        public void OrdHit(Collider2D collider,float damage)
        {
            if(_ords.TryGetValue(collider,out IOrd ord))
            {
                ord.Hit(damage);
            }
        }
    
    }
}
