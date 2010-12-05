using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class InvalidFeatures 
    {
        
        [TestMethod]
        public void EmptyFeature()
        {         
            Given_the_Feature_is_Empty();        
            The_Runner_should_be("Feature file is Empty");
        }
        
        [TestMethod]
        public void UnparseableFeature()
        { 
        }
        
        [TestMethod]
        public void UnnamedFeature()
        { 
        }

    }
}
