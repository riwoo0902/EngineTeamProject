using _01.Script.Lrw.PinBallCompo.FSM.Interface;

namespace _01.Script.Lrw.PinBallCompo.FSM.PinBallState
{
    public class PinBallIdleState :PinBallStateBase,ICanShoot
    {
        public PinBallIdleState(PinBallMachine pinBallMachine) : base(pinBallMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _rigid.gravityScale = 0;
        }
        
        
    }
}