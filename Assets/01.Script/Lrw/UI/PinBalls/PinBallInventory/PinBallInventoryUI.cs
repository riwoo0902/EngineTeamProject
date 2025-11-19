using System.Collections.Generic;
using System.Linq;
using _01.Script.Lrw.Inventory;
using _01.Script.Lrw.UI.PinBalls.PinBallSlot;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.UI.PinBalls.PinBallInventory
{
    public class PinBallInventoryUI : MonoSingleton<PinBallInventoryUI>
    {
        public List<PinBallSO> currentHavePinBalls => PinballInventory.Instance.inventory;
        [SerializeField] private List<PinballInventorySlot> pinballSlots = new();
        
        protected override void Awake()
        {
            base.Awake();
            pinballSlots = GetComponentsInChildren<PinballInventorySlot>().ToList();
            
        }

        private void Update()
        {
            SlotSetting();
        }

        private void SlotSetting()
        {
            for (int i = 0; i < pinballSlots.Count; i++)
            {
                try
                {
                    pinballSlots[i].SetPinBall(currentHavePinBalls[i + 1]);
                }
                catch
                {
                    pinballSlots[i].SetNull();
                }
            }
        }
        
    }
}