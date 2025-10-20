using System;
using UnityEngine;

namespace _01.Script.Lrw.PinBallMap.PinBallEvent
{
    public class BallTriggers : MonoBehaviour
    {
        public event Action<float> OnBallScoreTrigger;
        private float _finalScore = 0;
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
            _finalScore += value;
            NeedAddScoreCounter--;
            if (NeedAddScoreCounter <= 0)
            {
                OnBallScoreTrigger?.Invoke(_finalScore);
#if UNITYEDITOR
                Debug.Log("FinalScore : " + FinalScore);
#endif
                
            }
        }
        
        
    }
}