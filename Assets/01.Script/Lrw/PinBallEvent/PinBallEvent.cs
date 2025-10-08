using UnityEngine;
using UnityEngine.Events;

namespace Lrw_PinBallEvent
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PinBallEvent : MonoBehaviour
    {

        [SerializeField] private UnityEvent OnTriggerEvent;


        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            OnTriggerEvent?.Invoke();
#if UNITY_EDITOR
            Debug.Log("Event Invoke");
#endif
        }

    }
}