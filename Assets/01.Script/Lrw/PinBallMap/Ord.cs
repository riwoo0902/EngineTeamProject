using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Lrw_Ord
{
    public abstract class Ord : MonoBehaviour
    {
        [SerializeField] protected int HP = 2;
        private UnityEvent<int> intevent;

        private void Awake()
        {
            intevent.AddListener(a);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            EventBus.Invoke("");

            HP--;
            if(HP <= 0) Destroy(gameObject);

        }

        private void a(int a)
        {
            
        }

    }
}

