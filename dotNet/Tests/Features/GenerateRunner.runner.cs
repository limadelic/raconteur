using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class GenerateFeatureRunner 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void GenerateRunnerClass()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Feature Name
");        
            FeatureRunner.The_Runner_should_be(
@"
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Features
{
[TestClass]
public partial class FeatureName
{
}
}
");
        }

    }
}
