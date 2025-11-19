using System;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.UI.PinBalls
{
    public abstract class PinballSlotBase : MonoBehaviour, IPinBallSlot
    {
        [field:SerializeField] public PinBallSO Pinball { get; set; }
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] protected PinBallUISetting pinBallUISetting;
        
        public void SetPinBall(PinBallSO pinball)
        {
            Pinball =  pinball;
            spriteRenderer.sprite = Pinball.PinBallImage;
            SettingPinBallUISetting();
        }

        private void SettingPinBallUISetting()
        {
            pinBallUISetting.pinBallName = Pinball.name;
            pinBallUISetting.pinBallExplanation = Pinball.BallExplanation;
            pinBallUISetting.pinBallBounce = Pinball.Bounciness.ToString();
            pinBallUISetting.pinBallFriction = Pinball.Friction.ToString();
            pinBallUISetting.pinBallMass = Pinball.Mass.ToString();
        }
    }
    
    [Serializable]
    public class PinBallUISetting
    {
        public string pinBallName;
        public string pinBallExplanation;
        public string pinBallBounce;
        public string pinBallFriction;
        public string pinBallMass;
    }
}