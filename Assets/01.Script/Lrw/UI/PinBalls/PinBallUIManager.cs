using System;
using System.Collections.Generic;
using System.Linq;
using _01.Script.Lrw.Inventory;
using _01.Script.Lrw.PinBallMap;
using _01.Script.Lrw.UI.PinBalls.PinBallSlot;
using Lrw_PinBall;
using UnityEngine;


namespace _01.Script.Lrw.UI.PinBalls
{
    public class PinBallUIManager : Custom.MonoSingleton.MonoSingleton<PinBallUIManager>
    {
        [field:SerializeField] public List<PinBallSO> CurrentHavePinBalls { get; private set; } = new();

        private MainPinBallSlot nowPinBallSlot;
        private List<SubPinBallSlot> pinballSlots = new();
        private EnemyTurnManager _enemyTurnManagerl;
        private bool NoHavePinball => CurrentHavePinBalls.Count == 0;
        
        protected override void Awake()
        {
            base.Awake();
            nowPinBallSlot = GetComponentInChildren<MainPinBallSlot>();
            pinballSlots = GetComponentsInChildren<SubPinBallSlot>().ToList();
            
        }

        private void Start()
        {
            _enemyTurnManagerl = FindAnyObjectByType<EnemyTurnManager>();
            _enemyTurnManagerl.EnemyTurnEnd += ReSet;
        }

        private void Update()
        {
            SlotSetting();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _enemyTurnManagerl.EnemyTurnEnd -= ReSet;
        }

        private void SlotSetting()
        {
            if (CurrentHavePinBalls.Count > 0)
            {
                nowPinBallSlot.SetPinBall(CurrentHavePinBalls[0]);
                for (int i = 0; i < pinballSlots.Count; i++)
                {
                    try
                    {
                        pinballSlots[i].SetPinBall(CurrentHavePinBalls[i + 1]);
                    }
                    catch
                    {
                        pinballSlots[i].SetNull();
                    }
                }
            }
            else
            {
                nowPinBallSlot.SetNull();
                for (int i = 0; i < pinballSlots.Count; i++)
                { 
                    pinballSlots[i].SetNull();
                }
            }
        }
        
        [ContextMenu("Reset PinBalls")]
        public void ReSet()
        {
            PinBallSpawner.Instance.SetCanSpawn(true);
            CurrentHavePinBalls = PinballInventory.Instance.inventory.ToArray().ToList();
            PinBallSpawner.Instance.PinBallSpawn();
        }
        
        public void UsePinBall()
        {
            CurrentHavePinBalls.RemoveAt(0);
            
        }
        
    }
}