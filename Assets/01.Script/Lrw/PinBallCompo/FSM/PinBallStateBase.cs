namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public abstract class PinBallStateBase
    {
        protected PinBall pinBall;
        protected PinBallBrain ballBrain;
        protected PinBallStateBase(PinBall ball,PinBallBrain brain)
        {
            pinBall =  ball;
            ballBrain =  brain;
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