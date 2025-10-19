using System;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap.PinBallEvent
{
    public class BallTriggers : MonoBehaviour
    {
        public event Action<float> OnBallScoreTrigger;
        private float FinalScore = 0;
        public int NeedAddScoreCounter { get; set; }
        
        
        
        private void Awake()
        {
            BallTrigger[] ballTriggers = GetComponentsInChildren<BallTrigger>();
            foreach (BallTrigger trigger in ballTriggers)
            {
                trigger.OnBallScoreTrigger += AddFinalScore;
            }
            
        }
        public void AddFinalScore(float value)
        {
            FinalScore += value;
            NeedAddScoreCounter--;
            if (NeedAddScoreCounter <= 0)
            {
                OnBallScoreTrigger?.Invoke(FinalScore);
            }
        }
        
        
    }
}