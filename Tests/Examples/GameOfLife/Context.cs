using System.Collections.Generic;
using System.Linq;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.GameOfLife
{
    public class Context
    {
        GameOfLife Game;
        List<bool[]> Board;

        [TestInitialize]
        public void SetUp()
        {
            Game = new GameOfLife();    
        }

        public void Given_the_following_setup(params string[] Cells)
        {
            Game.AddRow(Cells.Select(Bool).ToArray());
        }

        public void Given_the_following_setup(params string[][] Cells)
        {
            foreach (var Cell in Cells)
                Game.AddRow(Cell.Select(Bool).ToArray());
        }

        public void Then_I_should_see_the_following_board(params string[] Cells)
        {
            Board.Add(Cells.Select(Bool).ToArray());
        }

        [TestCleanup]
        public void CleanUp() { VerifyBoard(); }

        void VerifyBoard() 
        {
            if (Board.Count == 0) return;

            Game.RowCount.ShouldBe(Board.Count);
            Game.ColumnCount.ShouldBe(Board[0].Length);

            for (var Row = 0; Row < Game.RowCount; Row++)
            for (var Col = 0; Col < Game.ColumnCount; Col++)
                Game.CellIsAlive(Row,Col).ShouldBe(Board[Row][Col]);
        }

        bool Bool(string Value) { return Value != "."; }

        public void When_I_evolve_the_board()
        {
            Board = new List<bool[]>();
            Game.Evolve();
        }

        const string Alive = "alive";

        public void Then_the_center_cell_should_be(string State)
        {
            CenterCellIsAlive().ShouldBe(State == Alive);
        }

        bool CenterCellIsAlive()
        {
            var CenterRow = (Game.RowCount - 1) / 2;
            var CenterCol = (Game.ColumnCount - 1) / 2;

            return Game.CellIsAlive(CenterRow, CenterCol);
        }
    }
}