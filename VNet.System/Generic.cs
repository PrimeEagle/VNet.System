namespace VNet.System
{
    public static class Generic
    {
        public static TTarget ConvertFromObject<TTarget>(object? value)
        {
            return (TTarget?)Convert.ChangeType(value, typeof(TTarget)) ?? throw new InvalidOperationException();
        }

        public static TTarget ConvertType<TSource, TTarget>(TSource value)
        {
            return (TTarget?)Convert.ChangeType(value, typeof(TTarget)) ?? throw new InvalidOperationException();
        }
    }
}