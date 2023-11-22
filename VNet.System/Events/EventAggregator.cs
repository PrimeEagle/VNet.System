using System.Collections.Concurrent;

// ReSharper disable ClassNeverInstantiated.Global


namespace VNet.System.Events
{
    public class EventAggregator : IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> _handlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            var handlers = (ConcurrentBag<object>)_handlers.GetOrAdd(typeof(TEvent), _ => new ConcurrentBag<object>());
            handlers ??= new ConcurrentBag<object>();
            handlers.Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (!_handlers.TryGetValue(typeof(TEvent), out var handlers) || handlers is not ConcurrentBag<Action<TEvent>> bag) return;
            while (bag.TryTake(out var temp))
            {
                if (temp != handler)
                {
                    bag.Add(temp);
                }
            }
        }

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            if (eventToPublish == null)
            {
                throw new ArgumentNullException(nameof(eventToPublish));
            }

            DispatchEvent(eventToPublish);
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            await DispatchEventAsync(@event, cancellationToken);
        }

        private void DispatchEvent<TEvent>(TEvent eventToPublish)
        {
            if (!_handlers.TryGetValue(typeof(TEvent), out var handlers) || handlers is not ConcurrentBag<object> bag) return;
            foreach (var handler in bag)
            {
                if (handler is Action<TEvent> action)
                {
                    action(eventToPublish);
                }
            }
        }

        private async Task DispatchEventAsync<TEvent>(TEvent eventToPublish, CancellationToken cancellationToken)
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handlers) && handlers is ConcurrentBag<object> bag)
            {
                foreach (var handler in bag)
                {
                    switch (handler)
                    {
                        case Action<TEvent> action:
                            action(eventToPublish);
                            break;
                        case Func<TEvent, CancellationToken, Task> asyncHandler:
                            await asyncHandler(eventToPublish, cancellationToken).ConfigureAwait(false);
                            break;
                    }
                }
            }
        }
    }
}