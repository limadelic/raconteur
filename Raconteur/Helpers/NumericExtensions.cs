namespace Raconteur
{
    public static class NumericExtensions
    {
        public static bool IsOdd(this int Number)
        {
            return !Number.IsEven();
        }

        public static bool IsEven(this int Number)
        {
            return Number % 2 == 0;
        }
    }
}