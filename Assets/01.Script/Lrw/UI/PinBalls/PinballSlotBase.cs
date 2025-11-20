using System;
using Lrw_PinBall;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _01.Script.Lrw.UI.PinBalls
{
    public abstract class PinballSlotBase : MonoBehaviour, IPinBallSlot ,IPointerEnterHandler,IPointerExitHandler
    {
        [field:SerializeField] public PinBallSO Pinball { get; set; }
        [SerializeField] private Image spriteRenderer;
        [SerializeField] protected PinBallUISetting pinBallUISetting;
        
        public void SetPinBall(PinBallSO pinball)
        {
            Pinball =  pinball;
            spriteRenderer.sprite = Pinball.PinBallImage;
            spriteRenderer.color = new Color(255,255,255,255);
            SettingPinBallUISetting();
        }

        private void SettingPinBallUISetting()
        {
            pinBallUISetting = new PinBallUISetting();
            pinBallUISetting.pinBallName = Pinball.name;
            pinBallUISetting.pinBallExplanation = Pinball.BallExplanation;
            pinBallUISetting.pinBallBounce = Pinball.Bounciness.ToString();
            pinBallUISetting.pinBallFriction = Pinball.Friction.ToString();
            pinBallUISetting.pinBallMass = Pinball.Mass.ToString();
        }

        public void SetNull()
        {
            Pinball =  null;
            spriteRenderer.sprite = null;
            spriteRenderer.color = new Color(255,255,255,0);
            pinBallUISetting = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(pinBallUISetting != null)
                PinBall_Explanation.Instance.PointerOnEnter(pinBallUISetting, transform);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PinBall_Explanation.Instance.PointerOnExit();
            
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