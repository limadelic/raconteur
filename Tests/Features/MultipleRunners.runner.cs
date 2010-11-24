using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class UsingMultipleRunners 
    {
        
        [TestMethod]
        public void MbUnitRunner()
        {         
            Using("MbUnit");        
            Given_the_Feature_is(
@"Feature: Feature Name
");        
            The_Runner_should_be(
@"using MbUnit.Framework;

namespace Features
{
[TestFixture]
public partial class FeatureName
{
}
}
");
        }

    }
}
