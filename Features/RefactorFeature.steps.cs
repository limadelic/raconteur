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
                .Returns(Actors.DefinedFeature.StepsDefintion);

            Generator = ObjectFactory.NewRaconteurGenerator(Item);
        }

        void If_I_rename_it()
        {
            var RenamedFeature = Actors.DefinedFeature.FeatureDefintion
                .Replace("Feature Name", "Renamed Feature");

            Generator.Generate("FileName.cs", RenamedFeature);
        }

        void Then_the_steps_and_the_runner_should_reflect_the_change()
        {
            var NewDefinitions = Actors.DefinedFeature.StepsDefintion
                .Replace("FeatureName", "RenamedFeature");

            Item.Received().AddStepDefinitions(NewDefinitions);
        }
    }
}
