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
            string StepDefinitions;

            public void given_no_existing_defintions_a_generator()
            {
                StepDefinitions = The.StepDefinitionsFor(new Feature
                {
                    Name = "Name",
                    FileName = "File Name",
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
                }, null);
            }

            public void given_existing_defintions_a_generator()
            {
                StepDefinitions = The.StepDefinitionsFor(new Feature
                {
                    Name = "Name",
                    FileName = "File Name",
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
                }, Actors.DefinedFeature.StepsDefintion);
            }

            [Test]
            public void should_generate_the_StepDefinitions_namespace()
            {
                given_no_existing_defintions_a_generator();
                StepDefinitions.ShouldContain("namespace Features");
            }

            [Test]
            public void should_generate_the_StepDefinitions_class()
            {
                given_no_existing_defintions_a_generator();
                StepDefinitions.ShouldContain("public partial class Name");
            }

            [Test]
            public void should_keep_defined_content()
            {
                given_existing_defintions_a_generator();

                StepDefinitions.ShouldContain("public partial class Name");
                StepDefinitions.ShouldNotContain("public partial class FeatureName");
                StepDefinitions.ShouldContain("var thing = string.Empty");
            }
        }
    }
}