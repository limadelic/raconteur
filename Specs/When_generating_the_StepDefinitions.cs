using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;

namespace Specs
{
    public class When_generating_the_StepDefinitions
    {
        [TestFixture]
        public class a_generator : BehaviorOf<StepDefinitionsGenerator>
        {
            readonly string StepDefinitions;

            public a_generator()
            {
                StepDefinitions = The.StepDefinitionsFor(new Feature
                {
                    FileName = "Name",
                    Namespace = "Features",
                    Scenarios =
                    {
                        new Scenario
                        {
                            Name = "Scenario 1",
                            Steps = { "Unique step", "Repeated step" }
                        },                            
                        new Scenario
                        {
                            Name = "Scenario 2",
                            Steps = { "Repeated step", "Another unique step" }
                        },                            
                    }
                });
            }

            [Test]
            public void should_generate_the_StepDefinitions_namespace()
            {
                StepDefinitions.ShouldContain("namespace Features");
            }

            [Test]
            public void should_generate_the_StepDefinitions_class()
            {
                StepDefinitions.ShouldContain("public partial class Name");
            }
        }
    }
}