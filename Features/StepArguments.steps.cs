using FluentSpec;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Features 
{
    public partial class StepArguments 
    {
        private string Runner;
        RunnerGenerator RunnerGenerator;

        void When_a_step_contains_arguments()
        {
            var featureFile = new FeatureFile
            {
                Content = 
                @"
                    Feature: Feature Name

                    Scenario: Scenario Name
                        If ""X"" happens
                "
            };

            var Parser = ObjectFactory.NewFeatureParser;

            RunnerGenerator = new RunnerGenerator();

            var Feature = Parser.FeatureFrom(featureFile, new VsFeatureItem());

            Runner = RunnerGenerator.RunnerFor(Feature);
        }
        
        void The_runner_should_pass_them_in_the_call()
        {
            Runner.ShouldContain(@"If__happens(""X"");");
        }
    }
}
