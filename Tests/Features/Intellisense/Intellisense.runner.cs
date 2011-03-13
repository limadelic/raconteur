using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features.Intellisense 
{
    [TestFixture]
    public partial class Intellisense 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void Completion_1()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Sc");        
            Then__should_be_displayed("Scenario:");
        }
        
        [Test]
        public void Completion_2()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Sc");        
            Then__should_be_displayed("Scenario Outline:");
        }
        
        [Test]
        public void Completion_3()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Ex");        
            Then__should_be_displayed("Examples:");
        }
        
        [Test]
        public void Completion_4()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
");        
            When_I_begin_to_type__on_the_next_line("Fe");        
            Then__should_be_displayed("Feature:");
        }
        
        [Test]
        public void Completion_5()
        {         
            FeatureRunner.Given_the_Feature_contains(
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
        
        [Test]
        public void Completion_6()
        {         
            FeatureRunner.Given_the_Feature_contains(
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
        
        [Test]
        public void Completion_7()
        {         
            FeatureRunner.Given_the_Feature_contains(
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
        
        [Test]
        public void IgnoreNon_steps_1()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Feature: Feature Name
Description
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
""
I am an arg
""
");        
            When_I_begin_to_type__on_the_next_line("De");        
            Then__should_not_be_displayed("Description");
        }
        
        [Test]
        public void IgnoreNon_steps_2()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Feature: Feature Name
Description
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
""
I am an arg
""
");        
            When_I_begin_to_type__on_the_next_line("Sc");        
            Then__should_not_be_displayed("Scenario: Scenario Name");
        }
        
        [Test]
        public void IgnoreNon_steps_3()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Feature: Feature Name
Description
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
""
I am an arg
""
");        
            When_I_begin_to_type__on_the_next_line("Fe");        
            Then__should_not_be_displayed("Feature: Feature Name");
        }
        
        [Test]
        public void IgnoreNon_steps_4()
        {         
            FeatureRunner.Given_the_Feature_contains(
@"
Feature: Feature Name
Description
Scenario: Scenario Name
I do something
I do another thing
If something happens
Then this should happen
""
I am an arg
""
");        
            When_I_begin_to_type__on_the_next_line("I");        
            Then__should_not_be_displayed("I am an arg");
        }

    }
}
