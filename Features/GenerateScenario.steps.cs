using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Features 
{
    public partial class GenerateScenario 
    {
        private string Runner;
        RunnerGenerator RunnerGenerator;

        public void When_the_Scenario_for_a_feature_is_generated()
        {
            var featureFile = new FeatureFile
            {
                Content =  
                @"
                    Feature: Feature Name
                        In order to do something
                        Another thing should happen

                    Scenario: Scenario Name
                "
            };

            var Parser = ObjectFactory.NewFeatureParser;

            RunnerGenerator = new RunnerGenerator();

            var Feature = Parser.FeatureFrom(featureFile, new VsFeatureItem());

            Runner = RunnerGenerator.RunnerFor(Feature);
        }

        public void Then_it_should_be_a_Test_Method()
        {
            Runner.ShouldContain("[TestMethod]");
        }

        public void And_it_should_be_named_After_the_Scenario_name()
        {
            Runner.ShouldContain(@"public void ScenarioName()");            
        }

        public void When_a_Scenario_with_steps_is_generated()
        {
            var featureFile = new FeatureFile
            {
                Content = 
                @"
                    Feature: Feature Name

                    Scenario: Scenario Name
                        If something happens
                        Then something else should happen
                        If something happens
                        And another thing too
                "
            };

            var Parser = ObjectFactory.NewFeatureParser;

            RunnerGenerator = new RunnerGenerator();

            var Feature = Parser.FeatureFrom(featureFile, new VsFeatureItem());

            Runner = RunnerGenerator.RunnerFor(Feature);
        }

        public void it_should_call_each_step_in_order() 
        {
            Runner.ShouldContainInOrder(
                "If_something_happens();",
                "Then_something_else_should_happen();",
                "If_something_happens();",
                "And_another_thing_too();");
        }
    }
}
