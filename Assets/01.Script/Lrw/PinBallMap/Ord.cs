using UnityEngine;
using UnityEngine.Events;

namespace Lrw_Ord
{
    public class Ord : MonoBehaviour
    {
        [SerializeField] private UnityEvent OrdCollisionEvent;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            OrdCollisionEvent?.Invoke();
            Destroy(gameObject);
        }

    }
}

