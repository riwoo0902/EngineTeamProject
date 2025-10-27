
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallMap.PinBallEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallMap : MonoBehaviour
    {
        public BallTriggers  BallTriggers { get; private set; }
        
        private void Awake()
        {
            BallTriggers = GetComponentInChildren<BallTriggers>();
        }

        public void SetBallTriggerEvent(UnityEvent<float> ue)
        {
            BallTriggers.OnBallScoreTrigger += (f) => { ue?.Invoke(f); };
        }

    }
}