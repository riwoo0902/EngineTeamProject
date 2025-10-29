using System.Collections.Generic;
using _01.Script.Lrw.PinBallCompo.FSM.Interface;
using _01.Script.Lrw.PinBallCompo.FSM.PinBallState;

namespace _01.Script.Lrw.PinBallCompo.FSM
{
    public class PinBallBrain
    {
        private Dictionary<PinBallStates,IState> _states = new Dictionary<PinBallStates, IState>();
        public IState CurrentState { get; private set; }

        #region StateSetting
        
        public void AddState(PinBallStates a ,  IState b) => _states.Add(a,b);
        
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

        public bool CheackType<T>()
        {
            return CurrentState is T;
        }
        #endregion

        #region StateUpdate

        public void Update()
        {
            CurrentState.Update();
        }

        public void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }
        #endregion
        
    }
}