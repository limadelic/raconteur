using System.Linq;
using FluentSpec;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples
{
    public class Context
    {
        GameOfLife Game;

        [TestInitialize]
        public void SetUp()
        {
            Game = new GameOfLife();    
        }

        public void Given_the_following_setup(params string[] Cells)
        {
            Game.AddRow(Cells.Select(Bool).ToArray());
        }

        public void Then_I_should_see_the_following_board(params string[] Cells)
        {
            
        }

        bool Bool(string Value) { return Value != "."; }

        public void When_I_evolve_the_board()
        {
            Game.Evolve();
        }

        const string Alive = "alive";

        public void Then_the_center_cell_should_be(string State)
        {
            CenterCellIsAlive().ShouldBe(State == Alive);
        }

        bool CenterCellIsAlive()
        {
            var CenterRow = (Game.RowCount - 1)/2;
            var CenterCol = (Game.ColumnCount - 1)/2;

            return Game.CellIsAlive(CenterRow, CenterCol);
        }
    }
}