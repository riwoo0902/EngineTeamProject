
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.PinBallMap.PinBallEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallMap : MonoBehaviour
    {
        private BallTriggers  _ballTriggers;
        
        public void AddNeedTriggerCounter(AddNeedTriggerCountEvent a) => _ballTriggers.NeedAddScoreCounter += a.AddAmount;
        

        private void Awake()
        {
            _ballTriggers = GetComponentInChildren<BallTriggers>();
            EventBus<AddNeedTriggerCountEvent>.OnEvent += AddNeedTriggerCounter;
        }

        public void SetBallTriggers(UnityEvent<float> a)
        {
            _ballTriggers.OnBallScoreTrigger += (float ballScore) => a?.Invoke(ballScore);
        }
    }
}