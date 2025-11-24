using System;
using System.Collections;
using System.Collections.Generic;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.PinBallEvent
{
    public class BallTriggers : MonoBehaviour
    {
        public event Action<int> OnBallScoreTrigger;
        private int _finalScore = 0;
        public int NeedAddScoreCounter { get; set; }
        
        public void AddNeedTriggerCounter(AddNeedTriggerCountEvent a) => NeedAddScoreCounter += a.AddAmount;
        
        private void Awake()
        {
            BallTrigger[] ballTriggers = GetComponentsInChildren<BallTrigger>();
            foreach (BallTrigger trigger in ballTriggers)
            {
                trigger.OnBallScoreTrigger += AddFinalScore;
            }
            EventBus<AddNeedTriggerCountEvent>.OnEvent += AddNeedTriggerCounter;
        }
        
        private void OnDestroy()
        {
            EventBus<AddNeedTriggerCountEvent>.OnEvent -= AddNeedTriggerCounter;
        }
        
        private void AddFinalScore(int value)
        {
            _finalScore += value;
            NeedAddScoreCounter--;
            if (NeedAddScoreCounter <= 0)
            {
                StartCoroutine(WaitForDestroyBall());
            }
        }

        private IEnumerator WaitForDestroyBall()
        {
            int a = _finalScore;
            _finalScore = 0;
            yield return new WaitForSeconds(1);
            OnBallScoreTrigger?.Invoke(a);
        }
        
    }
}