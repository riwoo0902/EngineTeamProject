using System.Collections.Generic;
using Lrw_PinBall;

namespace _01.Script.Lrw.Inventory
{
    public class PinballInventory : MonoSingleton<PinballInventory>
    {
        public List<PinBallSO> inventory = new();
        public PinBallSO NowPinBal;
        
        
        
    }
}