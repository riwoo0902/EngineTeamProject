using System.Collections.Generic;
using _01.Script.Lrw.PinBallCompo.FSM.PinBallState;

namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public class PinBallBrain
    {
        private Dictionary<PinBallStates,PinBallStateBase> _states = new Dictionary<PinBallStates, PinBallStateBase>();
        public PinBallStateBase CurrentState { get; private set; }

        public PinBallBrain(PinBall ball)
        {
            AddState(PinBallStates.Idle,new PinBallIdle(ball,this));
            AddState(PinBallStates.Shooting,new PinBallShooting(ball,this));
            SetState(PinBallStates.Idle);
        }

        #region StateSetting
        
        public void AddState(PinBallStates a ,  PinBallStateBase b) => _states.Add(a,b);
        
        public void SetState(PinBallStates a)
        {
            CurrentState =  _states[a];
            CurrentState.Enter();
        }
        
        public void ChangeState(PinBallStates a)
        {
            CurrentState.Exit();
            CurrentState = _states[a];
            CurrentState.Enter();
        }
        public bool CheackType<T>() => CurrentState is T;
        
        #endregion

        #region StateUpdate
        public void Update() => CurrentState.Update();
        
        public void FixedUpdate() => CurrentState.FixedUpdate();
        #endregion
        
    }
}