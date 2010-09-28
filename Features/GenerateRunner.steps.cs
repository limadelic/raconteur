using FluentSpec;

namespace Features 
{
    public partial class GenerateFeatureRunner : FeatureRunner
    {
        public void When_the_Runner_for_a_Feature_is_generated()
        {
            Feature = "Feature: Feature Name";
        }

        public void Then_it_should_be_a_TestClass()
        {
            Runner.ShouldContain("[TestClass]");
        }

        void And_it_should_be_named_FeatureName()
        {
            Runner.ShouldContain("public partial class FeatureName");
        }

        public void And_it_should_be_on_the_Feature_Namespace()
        {
            Runner.ShouldContain("namespace Features");
        }
    }
}
