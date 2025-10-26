using _01.Script.Lrw.EventBus.CoreSystem;

namespace _01.Script.Lrw.EventBus.EventBus.CoreSystem
{
    public static class EventBus<T> where T : IEvent
    {
        public delegate void Event(T evt);
        
        public static event Event OnEvent;
        
        public static void Raise(T evt) => OnEvent?.Invoke(evt);
        
    }
}