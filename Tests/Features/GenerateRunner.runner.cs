using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class GenerateFeatureRunner 
    {
        
        [TestMethod]
        public void GenerateRunnerClass()
        {         
            Given_the_Feature_is(
@"Feature: Feature Name
");        
            The_Runner_should_be(
@"using Microsoft.VisualStudio.TestTools.UnitTesting;
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
