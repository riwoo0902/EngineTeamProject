using System;
using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.EventBus.EventBusSystem.Events;
using _01.Script.Lrw.Manager;
using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.UI.PinBalls;
using Custom.MonoSingleton;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Script.Lrw.PinBallMap
{
    public class PinBallStageCreateManager : MonoSingleton<PinBallStageCreateManager>
    {
        public UnityEvent<int> onBallScoreTrigger;
        public PinBallMap CurrentPinBallMap {get; private set;}
        
        private Player _player;
        protected override void Awake()
        {
            base.Awake();

            onBallScoreTrigger.AddListener(ChangeGameManagerState);
            onBallScoreTrigger.AddListener(AddDamage);
            EventBus<OrbMapReset>.OnEvent += OrbReSet;
        }

        private void ChangeGameManagerState(int a)
        {
            GameManager.Instance.state = PinBallStates.Idle;
        }

        private void Start()
        {
            _player = FindAnyObjectByType<Player>();
        }

        private void AddDamage(int damage)
        {
            _player.AttackDamageCalculation(AttackType.None, damage);
        }

        private void ReLoadPinBallSlot()
        {
            
        }

        public void CreatMap(GameObject mapPrefab)
        {
            if (CurrentPinBallMap != null)
            {
                Destroy(CurrentPinBallMap.gameObject);
                CurrentPinBallMap = null;
            }
            CurrentPinBallMap = Instantiate(mapPrefab, transform).GetComponent<PinBallMap>();
            CurrentPinBallMap.SetBallTriggerEvent(onBallScoreTrigger); 
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            onBallScoreTrigger.RemoveAllListeners();
            EventBus<OrbMapReset>.OnEvent -= OrbReSet;
        }
        
        private void OrbReSet(OrbMapReset reset)
        {
            CurrentPinBallMap.OrdsReSet(reset.NoResetOrb);
        }
        
    }
}

