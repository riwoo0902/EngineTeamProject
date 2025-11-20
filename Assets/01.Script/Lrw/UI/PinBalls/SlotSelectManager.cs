using System;
using UnityEngine;

namespace _01.Script.Lrw.UI.PinBalls
{
    public class SlotSelectManager : MonoBehaviour
    {
        
        
        
        private void Awake()
        {
            PinballSlotBase[] a = FindObjectsByType<PinballSlotBase>(FindObjectsSortMode.None);
            
            
        }
        
        
    }
}