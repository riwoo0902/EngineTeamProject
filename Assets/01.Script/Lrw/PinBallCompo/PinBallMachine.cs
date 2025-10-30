using _01.Script.Lrw.PinBallCompo.FSM;
using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using _01.Script.Lrw.PinBallCompo.FSM.PinBallState;
using UnityEngine;

namespace _01.Script.Lrw.PinBallCompo
{
    public class PinBallMachine
    {
        private FsmBrain _fsmBrain;
        public PinBallMachine(IPinBallContext a)
        {
            _fsmBrain = new  FsmBrain();
            _fsmBrain.AddState(PinBallStates.Idle,new PinBallIdleState(a));
            _fsmBrain.AddState(PinBallStates.Shooting,new PinBallShootingState(a));
            _fsmBrain.SetState(PinBallStates.Idle);
        }

        public void ChangeState(PinBallStates a)
        {
            _fsmBrain.ChangeState(a);
        }
        
        public bool CheackType<T>()
        {
            return _fsmBrain.CheackType<T>() is T;
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