namespace VNet.System
{
    public class NormalizedPercentagePair<T> where T : Enum
    {
        public T Item { get; set; }
        public NormalizedDouble Percentage { get; set; }


        public NormalizedPercentagePair(T item, NormalizedDouble percentage)
        {
            Item = item;
            Percentage = percentage;
        }
    }
}