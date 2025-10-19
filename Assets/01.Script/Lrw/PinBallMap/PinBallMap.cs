using _01.Script.Lrw.PinBallMap.PinBallEvent;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallMap : MonoBehaviour
    {
        private BallTriggers  _ballTriggers;
        public UnityEvent<float> onBallScoreTrigger;
        
        public void AddNeedTriggerCounter(int value) => _ballTriggers.NeedAddScoreCounter += value;
        public void SetNeedTriggerCounter(int value) => _ballTriggers.NeedAddScoreCounter = value;
        
        private void Awake()
        {
            _ballTriggers = GetComponentInChildren<BallTriggers>();
            _ballTriggers.OnBallScoreTrigger += OnInvokeBallScoreTrigger;
#if  UNITY_EDITOR
            _ballTriggers.OnBallScoreTrigger += DebugBallScore;
#endif
            
        }

        private void OnInvokeBallScoreTrigger(float ballScore)  => onBallScoreTrigger?.Invoke(ballScore);

        private void DebugBallScore(float ballScore)
        {  
            Debug.Log(ballScore);
        }
        
        
        
        
    }
}