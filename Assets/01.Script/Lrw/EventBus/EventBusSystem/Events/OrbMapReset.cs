using _01.Script.Lrw.EventBus.EventBusSystem.CoreSystem;
using _01.Script.Lrw.PinBallMap.Ord;

namespace _01.Script.Lrw.EventBus.EventBusSystem.Events
{
    public class OrbMapReset :IEvent
    {
        public IOrd NoResetOrb;

        public OrbMapReset(IOrd a)
        {
            NoResetOrb = a;
        }
    }
}