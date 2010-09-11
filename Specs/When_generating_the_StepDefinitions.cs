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
                    FileName = "Name"
                });
            }

            [Test]
            public void should_generate_the_StepDefinitions_class()
            {
                StepDefinitions.ShouldContain("public partial class Name");
            }
        }
    }
}