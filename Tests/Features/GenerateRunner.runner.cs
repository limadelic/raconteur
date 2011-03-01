using Microsoft.VisualStudio.TestTools.UnitTesting;
using Features;

namespace Features 
{
    [TestClass]
    public partial class GenerateFeatureRunner 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();

        
        [TestMethod]
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
