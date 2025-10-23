using System;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallStageCreateManager : MonoBehaviour
    {
        public UnityEvent<float> onBallScoreTrigger;
        public PinBallMap CurrentPinBallMap{get; private set;}
        
        public GameObject TestPrefab;
        
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
                return;
            }
        }

        public void CreatPinBallMap(GameObject prefab)
        {
            CurrentPinBallMap = Instantiate(prefab,transform).GetComponent<PinBallMap>();
            CurrentPinBallMap.SetBallTriggers(onBallScoreTrigger);
        }
        
        [ContextMenu("Create Test")]
        public void CreatPinBallMap()
        {
            CurrentPinBallMap = Instantiate(TestPrefab,transform).GetComponent<PinBallMap>();
            CurrentPinBallMap.SetBallTriggers(onBallScoreTrigger);
        }
    }
}

