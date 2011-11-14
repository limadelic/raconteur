using System;

namespace Raconteur.Compilers
{
    public static class StringParser
    {
        public static bool IsNumeric(this string It)
        {
            int dummyInt;
            double dummyDouble;

            return int.TryParse(It, out dummyInt)
                || double.TryParse(It, out dummyDouble);
        }

        public static bool IsMultiline(this string It)
        {
            return It.Contains(Environment.NewLine);
        }

        public static bool IsDateTime(this string It)
        {
            DateTime dummyDate;

            return DateTime.TryParse(It, out dummyDate);
        }

        public static bool IsBoolean(this string It)
        {
            bool dummyBool;

            return bool.TryParse(It, out dummyBool);
        }
    }
}