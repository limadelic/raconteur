using NSubstitute;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;
using Specs;

namespace Features 
{
    public partial class RefactorFeature
    {
        readonly FeatureItem Item = Substitute.For<FeatureItem>();
        RaconteurGenerator Generator;

        void Given_I_have_already_defined_a_feature()
        {
            Item.ContainsStepDefinitions.Returns(true);
            Item.ExistingStepDefinitions
                .Returns(Actors.DefinedFeature.StepsDefinition);

            Generator = ObjectFactory.NewRaconteurGenerator(Item);
        }

        void If_I_rename_it()
        {
            Generator.Generate("RenamedFeature.cs", 
                Actors.DefinedFeature.RenamedFeatureDefinition);
        }

        void Then_the_steps_and_the_runner_should_reflect_the_change()
        {
            Item.Received().AddStepDefinitions(
                Actors.DefinedFeature.RenamedStepsDefinition);
        }
    }
}
