using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class UserSettings 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void DefaultSettings()
        {         
            Given_the_settings("");        
            When_the_project_is_loaded();        
            The_Settings_should_be_("MsTest", "English");
        }
        
        [Test]
        public void AllSettings()
        {         
            Given_the_settings(
@"
xUnit: MbUnit
language: es
lib: Library
lib: another Library
using: StepDefinitions
using: Another Step Definitions
");        
            When_the_project_is_loaded();        
            The_Settings_should_be_("MbUnit", "Spanish");        
            The_Libraries_should_be_
            (        
                new[] {"Library", "another Library"}
            );        
            The_Step_Definitions_should_be_
            (        
                new[] {"StepDefinitions", "AnotherStepDefinitions"}
            );
        }

    }
}
