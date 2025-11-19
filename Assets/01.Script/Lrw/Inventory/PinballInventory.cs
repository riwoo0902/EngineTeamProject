using System.Collections.Generic;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.Inventory
{
    [DefaultExecutionOrder(-10)]
    public class PinballInventory : Custom.MonoSingleton.MonoSingleton<PinballInventory>
    {
        public List<PinBallSO> inventory = new();
        
        
    }
}