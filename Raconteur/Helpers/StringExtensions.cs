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

        public static string CamelCase(this string Sentence)
        {
            return Sentence.Split(new [] {' '})
                .Aggregate(string.Empty, (Result, Current) => 
                    Result + Current.Capitalize());
        }

        public static string Underscores(this string Sentence)
        {
            return Sentence.Trim().Replace(' ', '_');
        }

        public static bool IsScenarioDeclaration(this string Line)
        {
            return Line.StartsWith("Scenario: ");
        }
    }
}