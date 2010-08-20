using System.Linq;

namespace Raconteur
{
    public static class StringExtensions
    {
        public static string Capitalize(this string Word)
        {
            if (string.IsNullOrEmpty(Word)) return Word;

            return Word[0].ToString().ToUpperInvariant() + Word.Substring(1);
        }

        public static string CammelCase(this string Sentence)
        {
            return Sentence.Split(new [] {' '})
                .Aggregate(string.Empty, (Result, Current) => 
                    Result + Current.Capitalize());
        }
    }
}