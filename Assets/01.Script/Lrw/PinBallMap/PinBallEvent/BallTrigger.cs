using System;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap.PinBallEvent
{
    [RequireComponent(typeof(BoxCollider2D ))]
    public class BallTrigger : MonoBehaviour
    {
        public event Action<float> OnBallScoreTrigger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ICanTriggerEvent>(out ICanTriggerEvent iCanTriggerEvent))
            {
                OnBallScoreTrigger?.Invoke(iCanTriggerEvent.GetScore());
#if UNITY_EDITOR
                Debug.Log("Event Invoke");
#endif
            }
            
        }

    }
}