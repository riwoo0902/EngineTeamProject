namespace _01.Script.Lrw.PinBallCompo.FSM.Interface
{
    public interface IState
    {
        public void Enter();
        public void Update();
        public void FixedUpdate();
        public void Exit();
    }
}