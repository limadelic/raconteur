using FluentSpec;
using Raconteur.Generators;
using Raconteur.Helpers;
using Raconteur.IDE;

namespace Features 
{
    public partial class RefactorFeature
    {
        FeatureItem Item;
        RaconteurGenerator Generator;

        // Setup

        void Given_the_Step_Definition(string ExistingStepDefinitions)
        {
            Item = Create.TestObjectFor<FeatureItem>();
            Given.That(Item).ContainsStepDefinitions.Is(true);
            Given.That(Item).ExistingStepDefinitions.Are(ExistingStepDefinitions);
        }

        string Feature;
        void for_the_Feature(string Feature)
        {
            this.Feature = Feature;
        }

        // Exercise

        void When_the_Feature_is_renamed(string RenamedFeatureDefinition)
        {
            Generator = ObjectFactory.NewRaconteurGenerator(Item);
            Generator.Generate("RenamedFeature.cs", RenamedFeatureDefinition);
        }

        void When_the_default_namespace_changes_to(string NewNamespace)
        {
            Given.That(Item).DefaultNamespace.Is(NewNamespace);
            Generator = ObjectFactory.NewRaconteurGenerator(Item);

            Generator.Generate("Feature.cs", Feature);
        }

        // Verify

        void Then_the_Step_Definitions_should_be(string RenamedStepsDefinition)
        {
            Item.Should().AddStepDefinitions(RenamedStepsDefinition);
        }
    }
}
