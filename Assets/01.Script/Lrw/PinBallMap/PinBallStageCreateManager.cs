using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallStageCreateManager : MonoBehaviour
    {
        public UnityEvent<int> onBallScoreTrigger;
        public PinBallMap CurrentPinBallMap {get; private set;}
        
        public GameObject testPrefab;
        private Player _player;
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

            onBallScoreTrigger.AddListener(ChangeGameManagerState);
            onBallScoreTrigger.AddListener(AddDamage);
            EventBus<OrbMapReset>.OnEvent += ReSet;
        }

        private void ChangeGameManagerState(int a)
        {
            GameManager.Instance.state = PinBallStates.Idle;
        }

        private void Start()
        {
            //CreatMap(testPrefab);
            _player = FindAnyObjectByType<Player>();
        }

        private void AddDamage(int damage)
        {
            _player.AttackDamageCalculation(AttackType.None, damage);
        }

        public void CreatMap(GameObject mapPrefab)
        {
            CurrentPinBallMap = Instantiate(mapPrefab, transform).GetComponent<PinBallMap>();
            CurrentPinBallMap.SetBallTriggerEvent(onBallScoreTrigger); 
        }

        private void OnDestroy()
        {
            onBallScoreTrigger.RemoveListener(ChangeGameManagerState);
            onBallScoreTrigger.RemoveListener(AddDamage);
            EventBus<OrbMapReset>.OnEvent -= ReSet;
        }
        
        private void ReSet(OrbMapReset reset)
        {
            CurrentPinBallMap.OrdsReSet(reset.NoResetOrb);
        }
        
    }
}

