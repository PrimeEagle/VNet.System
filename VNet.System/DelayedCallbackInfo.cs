namespace VNet.System
{
    public struct DelayedCallbackInfo
    {
        public delegate void Callback();

        public uint Id;
        public Callback CallbackMethod;
        public double TimeToCall;
        public bool Repeating;
    }
}
