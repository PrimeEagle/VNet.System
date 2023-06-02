namespace VNet.System.Events
{
    public class DelayedCallbackManager
    {
        private List<DelayedCallbackInfo> _callbacks;
        private double currentTime;



        public DelayedCallbackManager()
        {
            _callbacks = new List<DelayedCallbackInfo>();
        }

        public void Update(double deltaMs)
        {
            currentTime += deltaMs;

            while (_callbacks.Count > 0 && _callbacks[0].TimeToCall <= currentTime)
            {
                var callback = _callbacks[0];
                _callbacks.RemoveAt(0);
                callback.CallbackMethod.Invoke();
            }
        }

        public uint AddCallback(DelayedCallbackInfo.Callback callback, double delayUntilCalled, bool repeating = false)
        {
            var id = (uint)_callbacks.Count;
            var delayedCallback = new DelayedCallbackInfo { CallbackMethod = callback, TimeToCall = currentTime + delayUntilCalled, Id = id, Repeating = repeating };
            _callbacks.Add(delayedCallback);
            _callbacks.Sort(HeapCompare);
            return id;
        }

        public bool RemoveCallback(uint id)
        {
            for (int i = 0; i < _callbacks.Count; i++)
            {
                if (_callbacks[i].Id == id)
                {
                    var callbackInfo = _callbacks[i];

                    _callbacks.RemoveAt(i);
                    if (callbackInfo.Repeating)
                    {
                        AddCallback(callbackInfo.CallbackMethod, callbackInfo.TimeToCall, callbackInfo.Repeating);
                    }
                    return true;
                }
            }
            return false;
        }

        public void ChangeTime(uint id, double newTime)
        {
            for (int i = 0; i < _callbacks.Count; i++)
            {
                if (_callbacks[i].Id == id)
                {
                    var temp = _callbacks[i];
                    temp.TimeToCall = newTime;
                    _callbacks[i] = temp;
                    _callbacks.Sort(HeapCompare);
                    break;
                }
            }
        }

        private static int HeapCompare(DelayedCallbackInfo left, DelayedCallbackInfo right)
        {
            return left.TimeToCall.CompareTo(right.TimeToCall);
        }
    }
}