using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class ArgTypes 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]        
        [Category("wip")]
        public void SimpleArgs()
        {         
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Arg Types
Scenario: Use string instead of int
Given the zipcode is ""33131""
");        
            FeatureRunner.The_Runner_should_contain(
@"
Given_the_zipcode_is(""33131"");
");
        }
        
        [Test]
        public void Tables()
        { 
        }
        
        [Test]
        public void TablesWithHeader()
        { 
        }
        
        [Test]
        public void ScenarioOutlines()
        { 
        }
        
        [Test]
        public void TablesWithArgs()
        { 
        }
        
        [Test]
        public void TablesWithHeaderAndArgs()
        { 
        }

    }
}
