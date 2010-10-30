using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features 
{
    [TestClass]
    public partial class RefactorFeature 
    {
        
        [TestMethod]
        public void RenameFeature()
        {         
            Given_the_Step_Definition(
@"public partial class FeatureName {}
");        
            When_the_Feature_is_renamed(
@"Feature: Renamed Feature
");        
            Then_the_Step_Definitions_should_be(
@"public partial class RenamedFeature {}
");
        }
        
        [TestMethod]
        public void ChangeDefaultNamespace()
        {         
            Given_the_Step_Definition(
@"namespace Features
{
public partial class FeatureName {}
}
");        
            for_the_Feature(
@"Feature: Feature Name
");        
            When_the_default_namespace_changes_to("NewFeatures");        
            Then_the_Step_Definitions_should_be(
@"namespace NewFeatures
{
public partial class FeatureName {}
}
");
        }

    }
}
