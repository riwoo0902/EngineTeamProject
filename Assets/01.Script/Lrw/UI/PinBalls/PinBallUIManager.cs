using System.Collections.Generic;
using System.Linq;
using Lrw_CustomReadonly;
using Lrw_PinBall;
using UnityEngine;


namespace _01.Script.Lrw.UI.PinBalls
{
    public class PinBallUIManager : MonoSingleton<PinBallUIManager>
    {
        [SerializeField] private List<PinBallSO> currentPinBallSOs = new();
        [SerializeField] private PinballSlotBase nowPinBall;
        [SerializeField] private List<PinballSlotBase> pinballSlots = new();
        
        
        [Header("Test")]
        [SerializeField] private PinBallSO ballSo;
        
        
        protected override void Awake()
        {
            base.Awake();
            pinballSlots = GetComponentsInChildren<PinballSlotBase>().ToList();
            
            
        }

        private void Update()
        {
            
        }

        [ContextMenu("Use PinBall")]
        public void UsePinBall()
        {
            
            
        }
        
        
        
    }
}