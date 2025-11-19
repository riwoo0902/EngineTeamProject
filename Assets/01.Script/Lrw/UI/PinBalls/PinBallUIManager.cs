using System.Collections.Generic;
using System.Linq;
using Lrw_CustomReadonly;
using UnityEngine;

namespace _01.Script.Lrw.UI.PinBalls
{
    public class PinBallUIManager : MonoBehaviour
    {
        [SerializeField] private List<PinballSlotBase> currentPinballSlots = new();
        [SerializeField, ReadOnly] private PinballSlotBase nowPinBall;
        private List<PinballSlotBase> _pinballSlots = new();
        
        private void Awake()
        {
            _pinballSlots = GetComponentsInChildren<PinballSlotBase>().ToList();
            currentPinballSlots =  _pinballSlots;
            
        }

        private void Update()
        {
            
        }

        [ContextMenu("Use PinBall")]
        public void UsePinBall()
        {
            if (currentPinballSlots.Count == 0)
            {
                Debug.Log("No pinball slots");
                return;
            }
            nowPinBall.gameObject.SetActive(false);
            nowPinBall = currentPinballSlots[0];
            currentPinballSlots.RemoveAt(0);
        }
        
        
        
    }
}