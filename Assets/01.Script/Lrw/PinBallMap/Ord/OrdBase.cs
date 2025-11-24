using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallCompo;
using _01.Script.Lrw.PinBallMap.Ord;
using UnityEngine;

namespace Lrw_Ord
{
    public abstract class OrdBase : MonoBehaviour,IOrd
    {
        [SerializeField] protected float hp;
        protected float currentHp = 2;
        protected Action<PinBallBase> OnHitEvent;
        protected Action<PinBallBase> OnDestroyEvent;
        public Collider2D Collider { get; set; }
        
        
        protected virtual void Awake()
        {
            currentHp = hp;
            Collider = gameObject.GetComponent<Collider2D>();
        }
        
        public virtual void Hit(float a,PinBallBase b)
        {
            currentHp -= a;
            OnHitEvent?.Invoke(b);
            if (currentHp <= 0)
            {
                OnDestroyEvent?.Invoke(b);
                gameObject.SetActive(false);
            }
        }
        

        public virtual void ReSet()
        {
            gameObject.SetActive(true);
            currentHp = hp;
        }

        protected virtual void OnDestroy()
        {
            EventBus<OrdDestoryEvent>.Raise(new OrdDestoryEvent(Collider));
        }
    }
}

