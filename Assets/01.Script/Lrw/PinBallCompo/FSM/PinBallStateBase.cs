using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public abstract class PinBallStateBase : IState
    {
        protected PinBallBase pinBallBase;
        protected Rigidbody2D _rigid;
        protected PinBallSO _pinBallSo;
        protected PinBallStateBase(PinBallBase pinBallBase)
        {
            this.pinBallBase =  pinBallBase;
            _rigid = pinBallBase.Rigid;
            _pinBallSo = pinBallBase.PinBallSo;
        }
        
        public virtual void Enter()
        {
            
        }

        
        public virtual void Update()
        {
              
        }

        public virtual void FixedUpdate()
        {
            
        }
        public virtual void Exit()
        {
            
        }
        
    }
}