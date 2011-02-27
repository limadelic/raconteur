using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Raconteur
{
    public static class StringEx
    {
        public static string Capitalize(this string Word)
        {
            if (string.IsNullOrEmpty(Word)) return Word;

            return Word[0].ToString().ToUpperInvariant() + Word.Substring(1);
        }

        public static string RemoveTail(this string Word, int Lenght)
        {
            if (string.IsNullOrEmpty(Word)) return Word;

            return Word.Remove(Word.Length - Lenght);
        }

        public static string Quoted(this string Word)
        {
            return "\"" + Word + "\"";
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

        public static bool IsEmpty(this string It)
        {
            return string.IsNullOrEmpty(It);
        }

        public static bool HasValue(this string It)
        {
            return !IsEmpty(It);
        }

        public static string Times(this int Count, char c)
        {
            return new string(c, Count);
        }

        public static string TrimLines(this string It)
        {
            var TrimmedLines = It.Lines()
                .Where(Line => !string.IsNullOrWhiteSpace(Line))
                .Select(Line => Line.Trim());

            return string.Join(Environment.NewLine, TrimmedLines);
        }

        public static bool StartsWithKeyword(this string line)
        {
            return Settings.Language.Keywords.Any(line.StartsWith);
        }

        public static IEnumerable<string> Lines(this string It)
        {
            return Regex.Split(It, Environment.NewLine);
        }

        public static bool EqualsEx(this string One, string Another)
        {
            return One.Equals(Another, StringComparison.InvariantCultureIgnoreCase);
        }

        public static List<Boundary> ArgBoundaries(this string Whole)
        {
            var results = new List<Boundary>();
            var positions = Whole.FindQuotePositions();

            for (var i = 1; i < positions.Count; i += 2)
                results.Add(new Boundary {
                    Start = positions[i-1], 
                    End = positions[i],
                });

            if (positions.Count % 2 != 0) 
                results.Add(new Boundary
                {
                    Start = positions[positions.Count-1], 
                    End = Whole.Length - 1, 
                });
            
            return results;
        }

        private static List<int> FindQuotePositions(this string Whole)
        {
            var positions = new List<int>();
            var currentPosition = 0;

            while (Whole.Contains('"'))
            {
                positions.Add(currentPosition + Whole.IndexOf('"'));

                currentPosition += Whole.IndexOf('"') + 1;
                Whole = Whole.Substring(Whole.IndexOf('"') + 1);
            }

            return positions;
        }

        public static IEnumerable<string> ApplyLengthTo(this IEnumerable<string> Lines, Action<int> ApplyFunc)
        {
            foreach (var Line in Lines)
            {
                yield return Line;
                ApplyFunc(Line.Length);
            }
        }

        public static string FirstWord(this string Sentence)
        {
            return Sentence.Split(' ')[0];
        }

        public static IEnumerable<int> IndexesOf(this string Whole, string Part)
        {
            return 
                from Match Match 
                in Regex.Matches(Whole, Regex.Escape(Part)) 
                select Match.Index;
        }
    }
}