using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class ScoreCalculationAlternativeForms 
    {
        
        [TestMethod]
        public void OneSingleSpare()
        {         
            Given_a_new_bowling_game();        
            When_I_roll_the_following_series_(3,7,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);        
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
        
        [TestMethod]
        public void YetAnotherBeginnersGame()
        {         
            Given_a_new_bowling_game();        
            When_my_rolls_are(2);        
            When_my_rolls_are(7);        
            When_my_rolls_are(1);        
            When_my_rolls_are(5);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(3);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(4);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(8);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            When_my_rolls_are(1);        
            Then_my_total_score_should_be(43);
        }

    }
}
