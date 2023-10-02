namespace VNet.System.Events
{
    public abstract class EventBase : IEvent
    {
        public DateTime Timestamp => DateTime.Now;
    }
}