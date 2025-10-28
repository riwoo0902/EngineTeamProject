using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallStageCreateManager : MonoBehaviour
    {
        public UnityEvent<float> onBallScoreTrigger;
        public PinBallMap CurrentPinBallMap{get; private set;}
        
        public GameObject testPrefab;
        
        public static PinBallStageCreateManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            CreatPinBallMap(testPrefab);
            CurrentPinBallMap.BallTriggers.NeedAddScoreCounter = 5;//테스트
        }

        public void CreatPinBallMap(GameObject prefab)
        {
            GameObject a = Instantiate(prefab, transform);
            CurrentPinBallMap = a.GetComponent<PinBallMap>();
            CurrentPinBallMap.SetBallTriggerEvent(onBallScoreTrigger);
        }
        
    }
}

