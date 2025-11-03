using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using _01.Script.Lrw.PinBallCompo.FSM.PinBallState;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    public class PinBallMachine
    {
        private FsmBrain _fsmBrain;
        private PinBall _pinBall;
        public PinBallMachine(PinBall a)
        {
            _fsmBrain = new FsmBrain();
            _pinBall = a;
            _fsmBrain.AddState(PinBallStates.Idle,new PinBallIdleState(this));
            _fsmBrain.AddState(PinBallStates.Shooting,new PinBallShootingState(this));
            _fsmBrain.SetState(PinBallStates.Idle);
        }

        public void ChangeState(PinBallStates a)
        {
            _fsmBrain.ChangeState(a);
        }
        
        public bool CheackType<T>()
        {
            return _fsmBrain.CheackType<T>();
        }
        
        public void Update()
        {
            _fsmBrain.Update();
        }

        public void FixedUpdate()
        {
            _fsmBrain.FixedUpdate();
        }
        
    }
}