

namespace _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem
{
    public static class EventBus<T> where T : IEvent
    {
        public delegate void Event(T evt);
        
        public static event Event OnEvent;
        
        public static void Raise(T evt) => OnEvent?.Invoke(evt);
        
    }
}