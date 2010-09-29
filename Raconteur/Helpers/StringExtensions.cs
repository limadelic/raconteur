using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string ToValidIdentifier(this string Sentence)
        {
            var Regex = new Regex(
                @"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Nl}\p{Mn}\p{Mc}\p{Cf}\p{Pc}\p{Lm}]");

            var Result = Regex.Replace(Sentence.Trim(), "_"); 
            
            if (!char.IsLetter(Result, 0))
                Result = string.Concat("_", Result);
            
            return CodeDomProvider.CreateProvider("C#")
                .CreateEscapedIdentifier(Result);
        }

        public static bool IsNumeric(this string It)
        {
            var dummyInt = 0;
            var dummyDouble = 0.0;

            return int.TryParse(It, out dummyInt)
                || double.TryParse(It, out dummyDouble);
        }

        public static bool IsDateTime(this string It)
        {
            var dummyDate = new DateTime();

            return DateTime.TryParse(It, out dummyDate);
        }
    }
}