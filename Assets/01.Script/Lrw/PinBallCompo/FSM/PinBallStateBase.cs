using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public abstract class PinBallStateBase : IState
    {
        protected Transform transform;
        protected Rigidbody2D rigidbody2D;
        
        protected PinBallStateBase(IPinBallContext ball)
        {
            transform = ball.PinBallContextTransform;
            rigidbody2D = ball.PinBallContextRigidbody;
        }
        
        public void Enter()
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