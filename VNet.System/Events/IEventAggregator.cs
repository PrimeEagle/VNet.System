namespace VNet.System.Events
{
    public interface IEventAggregator
    {
        public void Publish<TEvent>(TEvent eventToPublish);
        public Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);
        public void Subscribe<TEvent>(Action<TEvent> action);
        public void Unsubscribe<TEvent>(Action<TEvent> action);
    }
}