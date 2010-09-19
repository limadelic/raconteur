using System.CodeDom.Compiler;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CSharp;

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
    }
}