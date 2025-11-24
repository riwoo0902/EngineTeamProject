using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public struct ScoreEvent : IEvent
    {
        public float Score;

        public ScoreEvent(float score)
        {
            Score  = score;
        }
    }
}