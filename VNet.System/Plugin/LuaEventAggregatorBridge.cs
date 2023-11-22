using NLua;
using VNet.System.Events;

namespace VNet.System.Plugin
{
    public class LuaEventAggregatorBridge
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly Lua _luaEnvironment;
        private Dictionary<string, Delegate> _subscriptions = new Dictionary<string, Delegate>();

        public LuaEventAggregatorBridge(IEventAggregator eventAggregator, Lua luaEnvironment)
        {
            _eventAggregator = eventAggregator;
            _luaEnvironment = luaEnvironment;
        }

        public void Subscribe(string eventName)
        {
            var eventType = Type.GetType(eventName);
            if (eventType == null) return;

            Action<object> action = (args) =>
            {
                var luaFunction = _luaEnvironment.GetFunction("HandleEvent");
                luaFunction?.Call(eventName, args);
            };

            _subscriptions[eventName] = action;

            var method = _eventAggregator.GetType().GetMethod("Subscribe");
            var generic = method.MakeGenericMethod(eventType);
            generic.Invoke(_eventAggregator, new object[] { action });
        }

        public void Unsubscribe(string eventName)
        {
            if (!_subscriptions.TryGetValue(eventName, out var action))
            {
                return;
            }

            var eventType = Type.GetType(eventName);
            if (eventType == null) return;

            var method = _eventAggregator.GetType().GetMethod("Unsubscribe");
            var generic = method.MakeGenericMethod(eventType);
            generic.Invoke(_eventAggregator, new object[] { action });

            _subscriptions.Remove(eventName);
        }
    }
}