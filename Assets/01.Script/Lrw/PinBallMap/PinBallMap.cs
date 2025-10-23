using System;
using _01.Script.Lrw.PinBallMap.PinBallEvent;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallMap : MonoBehaviour
    {
        private BallTriggers  _ballTriggers;
        
        public void AddNeedTriggerCounter(int value) => _ballTriggers.NeedAddScoreCounter += value;
        public void SetNeedTriggerCounter(int value) => _ballTriggers.NeedAddScoreCounter = value;
        

        private void Awake()
        {
            _ballTriggers = GetComponentInChildren<BallTriggers>();
        }

        public void SetBallTriggers(UnityEvent<float> a)
        {
            _ballTriggers.OnBallScoreTrigger += (float ballScore) => a?.Invoke(ballScore);
        }
    }
}