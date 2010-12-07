using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Specs
{
    [TestFixture]
    public class When_the_Feature_is_invalid
    {
        [TestFixture]
        public class A_feature_parser : BehaviorOf<FeatureParserClass>
        {
            readonly FeatureFile FeatureFile = new FeatureFile();
            readonly VsFeatureItem VsFeatureItem = new VsFeatureItem();

            [Test]
            public void should_handle_an_empty_feature()
            {
                FeatureFile.Content = string.Empty;
                The.FeatureFrom(FeatureFile, VsFeatureItem).ShouldBeA<InvalidFeature>();
            }
        }

        [TestFixture]
        public class the_Steps
        {
            [Test]
            public void should_be_empty_on_create()
            {
                ObjectFactory.NewStepDefinitionsGenerator(new InvalidFeature(), null)
                    .Code.ShouldBeEmpty();
            }

            [Test]
            public void should_not_change_on_update()
            {
                ObjectFactory.NewStepDefinitionsGenerator(new InvalidFeature(), "Steps")
                    .Code.ShouldBe("Steps");
            }
        }
    }
}