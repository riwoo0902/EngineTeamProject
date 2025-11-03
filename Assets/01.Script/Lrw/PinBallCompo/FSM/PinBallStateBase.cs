using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using Lrw_PinBall;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public abstract class PinBallStateBase : IState
    {
        protected PinBallMachine _pinBallMachine;
        protected Rigidbody2D _rigid;
        protected PinBallSO _pinBallSo;
        protected PinBallStateBase(PinBallMachine pinBallMachine)
        {
            _pinBallMachine = pinBallMachine;
            _rigid = pinBallMachine.PinBall.Rigid;
            _pinBallSo = pinBallMachine.PinBall.PinBallSo;
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