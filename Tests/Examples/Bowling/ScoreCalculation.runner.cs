using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class ScoreCalculation 
    {
        
        [TestMethod]
        public void GutterGame()
        {         
            Given_a_new_bowling_game();        
            When_all_of_my_balls_are_landing_in_the_gutter();        
            Then_my_total_score_should_be(0);
        }
        
        [TestMethod]
        public void BeginnersGame()
        {         
            Given_a_new_bowling_game();        
            When_I_roll__and(2, 7);        
            And_I_roll__and(3, 4);        
            And_I_roll__times__and(8, 1, 1);        
            Then_my_total_score_should_be(32);
        }
        
        [TestMethod]
        public void AnotherBeginnersGame()
        {         
            Given_a_new_bowling_game();        
            When_I_roll_the_following_series_(2,7,3,4,1,1,5,1,1,1,1,1,1,1,1,1,1,1,5,1);        
            Then_my_total_score_should_be(40);
        }
        
        [TestMethod]
        public void AllStrikes()
        {         
            Given_a_new_bowling_game();        
            When_all_of_my_rolls_are_strikes();        
            Then_my_total_score_should_be(300);
        }
        
        [TestMethod]
        public void OneSingleSpare()
        {         
            Given_a_new_bowling_game();        
            When_I_roll_the_following_series_(2,8,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);        
            Then_my_total_score_should_be(29);
        }
        
        [TestMethod]
        public void AllSpares()
        {         
            Given_a_new_bowling_game();        
            When_I_roll__times__and(10, 1, 9);        
            And_I_roll(1);        
            Then_my_total_score_should_be(110);
        }

    }
}
