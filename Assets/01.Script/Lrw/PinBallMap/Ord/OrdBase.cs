using System;
using UnityEngine;

namespace Lrw_Ord
{
    public abstract class OrdBase : MonoBehaviour,IOrd
    {
        [SerializeField] protected float HP = 2;
        protected Action OnHitEvent;
        protected Action OnDestroyEvent;
        public Collider2D Collider { get; set; }
        protected void Awake()
        {
            Collider = gameObject.GetComponent<Collider2D>();
        }
        
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

