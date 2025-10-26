using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public class AddNeedTriggerCountEvent : IEvent
    {
        public int AddAmount;
        public AddNeedTriggerCountEvent(int a)
        {
            AddAmount = a;
        }
    }
}