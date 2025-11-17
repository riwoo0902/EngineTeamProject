using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public struct ScoreAddEvent : IEvent
    {
        public float AddScore;
        public ScoreAddEvent(float addScore)
        {
            AddScore = addScore;
        }
    }
}