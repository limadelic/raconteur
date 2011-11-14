using FluentSpec;

namespace Examples.Bowling
{
    public class Context
    {
        BowlingGame Game;

        public void Given_a_new_bowling_game()
        {
            Game = new BowlingGame();    
        }

        public void When_all_of_my_balls_are_landing_in_the_gutter()
        {
            for (var i = 0; i < 20; i++) Game.Roll(0);
        }

        public void When_all_of_my_rolls_are_strikes()
        {
            for (var i = 0; i < 12; i++) Game.Roll(10);
        }

        public void Then_my_total_score_should_be(int ExpectedScore)
        {
            Game.Score.ShouldBe(ExpectedScore);
        }

        public void When_I_roll__and(int First, int Second)
        {
            Game.Roll(First);
            Game.Roll(Second);
        }

        public void And_I_roll__and(int First, int Second)
        {
            When_I_roll__and(First, Second);
        }

        public void When_I_roll_the_following_series_(params int[] Rolls)
        {
            foreach (var Pins in Rolls)
                Game.Roll(Pins);
        }

        public void When_I_roll__times__and(int Times, int First, int Second)
        {
            for (var i = 0; i < Times; i++)
                When_I_roll__and(First, Second);
        }

        public void And_I_roll__times__and(int Times, int First, int Second)
        {
            When_I_roll__times__and(Times, First, Second);
        }

        public void When_my_rolls_are(int Pins)
        {
            Game.Roll(Pins);
        }
        public void And_I_roll(int Pins) { When_my_rolls_are(Pins); }
    }
}