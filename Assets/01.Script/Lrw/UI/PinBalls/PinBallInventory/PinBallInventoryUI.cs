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

        [SerializeField] private GameObject InventorySlotPrefab;
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
            if (currentHavePinBalls.Count > pinballSlots.Count)
            {
                int j = currentHavePinBalls.Count - pinballSlots.Count;
                for (int i = 0; i < j; i++)
                {
                    pinballSlots.Add(Instantiate(InventorySlotPrefab, transform).GetComponent<PinballInventorySlot>());
                }
            }
            
            for (int i = 0; i < pinballSlots.Count; i++)
            {
                try
                {
                    pinballSlots[i].SetPinBall(currentHavePinBalls[i]);
                }
                catch
                {
                    pinballSlots[i].SetNull();
                }
            }
        }
        
    }
}