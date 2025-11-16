using _01.Script.Lrw.PinBallCompo.FSM.Interface;

namespace _01.Script.Lrw.PinBallCompo.FSM.PinBallState
{
    public class PinBallShootingState :PinBallStateBase
    {
        public PinBallShootingState(PinBallBase pinBallBase) : base(pinBallBase)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _rigid.gravityScale = _pinBallSo.Mass;
        }
    }
}