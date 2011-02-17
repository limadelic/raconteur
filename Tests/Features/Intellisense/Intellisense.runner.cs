using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.Intellisense 
{
    [TestClass]
    public partial class Intellisense 
    {
        
        [TestMethod]
        public void Completion1()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Sc");        
            Then__should_be_displayed("Scenario");
        }
        
        [TestMethod]
        public void Completion2()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Sc");        
            Then__should_be_displayed("Scenario Outline");
        }
        
        [TestMethod]
        public void Completion3()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Ex");        
            Then__should_be_displayed("Examples");
        }
        
        [TestMethod]
        public void Completion4()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Fe");        
            Then__should_be_displayed("Feature");
        }
        
        [TestMethod]
        public void Completion5()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("I");        
            Then__should_be_displayed("I do something");
        }
        
        [TestMethod]
        public void Completion6()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("I");        
            Then__should_be_displayed("I do another thing");
        }
        
        [TestMethod]
        public void Completion7()
        {         
            Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("I");        
            Then__should_be_displayed("If something happens");
        }

    }
}
