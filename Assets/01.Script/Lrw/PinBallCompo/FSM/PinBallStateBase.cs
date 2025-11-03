using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public abstract class PinBallStateBase : IState
    {
        protected PinBallMachine _pinBallMachine;
        protected PinBallStateBase(PinBallMachine pinBallMachine)
        {
            _pinBallMachine = pinBallMachine;
        }
        
        public virtual void Enter()
        {
            
        }

        public void Update()
        {
              
        }

        public void FixedUpdate()
        {
            
        }
        public void Exit()
        {
            
        }
        
    }
}