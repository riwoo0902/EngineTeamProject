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
        [SerializeField] private List<PinBallSO> currentHavePinBalls = new();
        
        [SerializeField] private MainPinBallSlot nowPinBallSlot;
        [SerializeField] private List<SubPinBallSlot> pinballSlots = new();
        
        private bool NoHavePinball => currentHavePinBalls.Count == 0;
        
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
            if (currentHavePinBalls.Count > 0)
            {
                nowPinBallSlot.SetPinBall(currentHavePinBalls[0]);
                if (currentHavePinBalls.Count > 1)
                {
                    for (int i = 0; i < pinballSlots.Count; i++)
                    {
                        try
                        {
                            pinballSlots[i].SetPinBall(currentHavePinBalls[i+1]);
                        }
                        catch
                        {
                            pinballSlots[i].SetNull();
                        }
                    }
                }
            }
        }
        
        [ContextMenu("Reset PinBalls")]
        public void ReSet()
        {
            currentHavePinBalls = PinballInventory.Instance.inventory.ToArray().ToList();
            
        }

        [ContextMenu("Use PinBall")]
        public void UsePinBall()
        {
            currentHavePinBalls.RemoveAt(0);
            
        }
        
    }
}