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
            CurrentPinBallMap = Instantiate(testPrefab, transform).GetComponent<PinBallMap>();
        }

        private void Start()
        {
            CurrentPinBallMap.SetBallTriggerEvent(onBallScoreTrigger);
        }

        
    }
}

