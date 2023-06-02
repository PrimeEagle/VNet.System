namespace VNet.System.Extensions
{
    public static class DoubleExtensions
    {
        public static bool IsLessThan(this double a, double b, double tolerance)
        {
            return a - b < -tolerance;
        }

        public static bool IsLessThanOrEqual(this double a, double b, double tolerance)
        {
            var result = a - b;

            return result < -tolerance || Math.Abs(result) < tolerance;
        }

        public static bool IsGreaterThan(this double a, double b, double tolerance)
        {
            return a - b > tolerance;
        }

        public static bool IsGreaterThanOrEqual(this double a, double b, double tolerance)
        {
            var result = a - b;
            return result > tolerance || Math.Abs(result) < tolerance;
        }
    }
}
