using NSubstitute;
using Raconteur;
using Raconteur.Generators;
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
            Item = Substitute.For<FeatureItem>();
            Item.ContainsStepDefinitions.Returns(true);
            Item.ExistingStepDefinitions.Returns(ExistingStepDefinitions);
        }

        string Feature;
        void for_the_Feature(string Feature)
        {
            this.Feature = Feature;
        }

        // Excercise

        void When_the_Feature_is_renamed(string RenamedFeatureDefinition)
        {
            Generator = ObjectFactory.NewRaconteurGenerator(Item);
            Generator.Generate("RenamedFeature.cs", RenamedFeatureDefinition);
        }

        void When_the_default_namespace_changes_to(string NewNamespace)
        {
            Item.DefaultNamespace.Returns(NewNamespace);
            Generator = ObjectFactory.NewRaconteurGenerator(Item);

            Generator.Generate("Feature.cs", Feature);
        }

        // Verify

        void Then_the_Step_Definitions_should_be(string RenamedStepsDefinition)
        {
            Item.Received().AddStepDefinitions(RenamedStepsDefinition);
        }
    }
}
