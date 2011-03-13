using MbUnit.Framework;
using Features.StepDefinitions;

namespace Features 
{
    [TestFixture]
    public partial class UsingMultipleRunners 
    {
        public FeatureRunner FeatureRunner = new FeatureRunner();
        public HighlightFeature HighlightFeature = new HighlightFeature();

        
        [Test]
        public void MbUnitRunner()
        {         
            Using("MbUnit");        
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Feature
Scenario: Scenario
");        
            FeatureRunner.The_Runner_should_be(
@"
using MbUnit.Framework;
namespace Features
{
[TestFixture]
public partial class Feature
{
[Test]
public void Scenario()
{
}
}
}
");
        }
        
        [Test]
        public void NUnitRunner()
        {         
            Using("NUnit");        
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Feature
Scenario: Scenario
");        
            FeatureRunner.The_Runner_should_be(
@"
using NUnit.Framework;
namespace Features
{
[TestFixture]
public partial class Feature
{
[Test]
public void Scenario()
{
}
}
}
");
        }
        
        [Test]
        public void XUnitRunner()
        {         
            Using("xUnit");        
            FeatureRunner.Given_the_Feature_is(
@"
Feature: Feature
Scenario: Scenario
");        
            FeatureRunner.The_Runner_should_be(
@"
using Xunit;
namespace Features
{
public partial class Feature
{
[Fact]
public void Scenario()
{
}
}
}
");
        }

    }
}
