using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.UI.PinBalls
{
    public interface IPinBallSlot
    {
        public PinBallSO Pinball { get; set; }
        public void SetPinBall(PinBallSO pinball);


    }
}