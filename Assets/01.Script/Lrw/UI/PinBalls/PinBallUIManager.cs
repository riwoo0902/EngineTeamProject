using System.Collections.Generic;
using System.Linq;
using _01.Script.Lrw.Inventory;
using _01.Script.Lrw.UI.PinBalls.PinBallSlot;
using Lrw_PinBall;
using UnityEngine;


namespace _01.Script.Lrw.UI.PinBalls
{
    public class PinBallUIManager : Custom.MonoSingleton.MonoSingleton<PinBallUIManager>
    {
        [field:SerializeField] public List<PinBallSO> CurrentHavePinBalls { get; private set; } = new();

        [SerializeField] private MainPinBallSlot nowPinBallSlot;
        [SerializeField] private List<SubPinBallSlot> pinballSlots = new();
        
        private bool NoHavePinball => CurrentHavePinBalls.Count == 0;
        
        protected override void Awake()
        {
            base.Awake();
            nowPinBallSlot = GetComponentInChildren<MainPinBallSlot>();
            pinballSlots = GetComponentsInChildren<SubPinBallSlot>().ToList();
            
        }

        private void Update()
        {
            SlotSetting();
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
            CurrentHavePinBalls = PinballInventory.Instance.inventory.ToArray().ToList();
        }
        
        public void UsePinBall()
        {
            CurrentHavePinBalls.RemoveAt(0);
            
        }
        
    }
}