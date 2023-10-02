using System.Collections.Concurrent;

namespace VNet.System.Events
{
    public class EventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> _handlers = new ConcurrentDictionary<Type, object>();

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
            Action<TEvent> temp;
            while (bag.TryTake(out temp))
            {
                if (temp != handler)
                {
                    bag.Add(temp);
                }
            }
        }

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handlers) && handlers is ConcurrentBag<object> bag)
            {
                foreach (var handler in bag)
                {
                    switch (handler)
                    {
                        case Action<TEvent> syncHandler:
                            syncHandler(@event);
                            break;
                        case Func<TEvent, CancellationToken, Task> asyncHandler:
                            await asyncHandler(@event, cancellationToken);
                            break;
                    }
                }
            }
        }
    }
}