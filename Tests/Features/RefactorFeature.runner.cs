using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class RefactorFeature 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void RenameFeature()
        {         
            Given_the_Step_Definition(
@"
public partial class FeatureName {}
");        
            When_the_Feature_is_renamed(
@"
Feature: Renamed Feature
");        
            Then_the_Step_Definitions_should_be(
@"
public partial class RenamedFeature {}
");
        }
        
        [Test]
        public void ChangeDefaultNamespace()
        {         
            Given_the_Step_Definition(
@"
namespace Features
{
public partial class FeatureName {}
}
");        
            for_the_Feature(
@"
Feature: Feature Name
");        
            When_the_default_namespace_changes_to("NewFeatures");        
            Then_the_Step_Definitions_should_be(
@"
namespace NewFeatures
{
public partial class FeatureName {}
}
");
        }

    }
}
