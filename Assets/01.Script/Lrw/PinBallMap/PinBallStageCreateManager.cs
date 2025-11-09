using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
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
            EventBus<OrbMapReset>.OnEvent += ReSet;
        }

        private void Start()
        {
            CurrentPinBallMap.SetBallTriggerEvent(onBallScoreTrigger);
        }

        private void OnDestroy()
        {
            EventBus<OrbMapReset>.OnEvent -= ReSet;
        }

        [ContextMenu("ReSet")]
        private void ReSet(OrbMapReset reset)
        {
            CurrentPinBallMap.OrdsReSet(reset.NoResetOrb);
        }
        
    }
}

