using System;
using UnityEngine;

namespace Lrw_Ord
{
    public abstract class OrdBase : MonoBehaviour,IOrd
    {
        [SerializeField] protected float HP = 2;
        protected Action OnHitEvent;
        protected Action OnDestroyEvent;
        public void Hit(float a)
        {
            HP -= a;
            OnHitEvent?.Invoke();
            if (HP <= 0)
            {
                OnDestroyEvent?.Invoke();
                Destroy(gameObject);
            }
        }
        
    }
}

