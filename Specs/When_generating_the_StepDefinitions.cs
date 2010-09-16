using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;

namespace Specs
{
    public class When_generating_the_StepDefinitions
    {
        static string StepDefinitions;

        [TestFixture]
        public class for_the_first_time : BehaviorOf<StepDefinitionsGenerator>
        {
            [FixtureSetUp]
            public void SetUp()
            {
                StepDefinitions = The.StepDefinitionsFor(Actors.Feature, null);
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

        [TestFixture]
        public class with_existing_steps : BehaviorOf<StepDefinitionsGenerator>
        {
            [FixtureSetUp]
            public void SetUp()
            {
                StepDefinitions = The.StepDefinitionsFor(Actors.Feature, Actors.DefinedFeature.StepsDefintion);
            }

            [Test]
            public void should_change_the_class_name()
            {
                StepDefinitions.ShouldContain("public partial class Name");
                StepDefinitions.ShouldNotContain("public partial class FeatureName");
            }

            [Test]
            public void should_keep_defined_content()
            {
                StepDefinitions.ShouldContain("var thing = string.Empty");
            }
        }
    }
}