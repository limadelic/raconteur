using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.Minesweeper
{
    public static class Extensions
    {
        public static void ShouldBe(this string[][] ExpectedBoard, string[][] ActualBoard)
        {
            ExpectedBoard.ForEachCell((Row, Col) => 
                Assert.AreEqual(ExpectedBoard[Row][Col].Sanitize(), ActualBoard[Row][Col].Sanitize(), 
                    "on [" + Row + "," + Col + "]"));
        }

        public static string Sanitize(this string s)
        {
            return string.IsNullOrWhiteSpace(s) || s == "0" ? null : s;
        }

        public static void ForEach<T>(this IEnumerable<T> Items, Action<T> Do)
        {
            foreach (var Item in Items) Do(Item);
        }

        public static string[][] CloneIt(this string[][] Dolly)
        {
            var result = new string[Dolly.Length][];
            for (int i = 0; i < Dolly.Length; i++)
            {
                result[i] = new string[Dolly[i].Length];
                for (int j = 0; j < Dolly[i].Length; j++) 
                    result[i][j] = Dolly[i][j];
            }
            return result;
        }

        public static string[][] Empty(this string[][] Dolly)
        {
            var result = new string[Dolly.Length][];
            for (int i = 0; i < Dolly.Length; i++)
                result[i] =  new string[Dolly[i].Length];
            return result;
        }

        public static void ForEachCell(this string[][] Board, Action<int, int> Do)
        {
            for (var Row = 0; Row < Board.Length; Row++)
                for (var Col = 0; Col < Board[0].Length; Col++)
                    Do(Row, Col);
        }

        public static bool AnyCell(this string[][] Board, Func<int, int, bool> Match)
        {
            for (var Row = 0; Row < Board.Length; Row++)
                for (var Col = 0; Col < Board[0].Length; Col++)
                    if (Match(Row, Col)) return true;

            return false;
        }

        public static IEnumerable<Tuple<int, int>> AdjacentCells(this object o, int Row, int Col)
        {
            yield return new Tuple<int, int>(Row - 1, Col - 1);
            yield return new Tuple<int, int>(Row - 1, Col);
            yield return new Tuple<int, int>(Row - 1, Col + 1);
            yield return new Tuple<int, int>(Row, Col - 1);
            yield return new Tuple<int, int>(Row, Col + 1);
            yield return new Tuple<int, int>(Row + 1, Col - 1);
            yield return new Tuple<int, int>(Row + 1, Col);
            yield return new Tuple<int, int>(Row + 1, Col + 1);
        }

        public static bool HasMineOn(this string[][] board, int Row, int Col)
        {
            return board[Row][Col] == "*";
        }
    }
}