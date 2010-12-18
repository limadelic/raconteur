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
@"
Feature: Feature
Scenario: Scenario
");        
            The_Runner_should_be(
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
        
        [TestMethod]
        public void NUnitRunner()
        {         
            Using("NUnit");        
            Given_the_Feature_is(
@"
Feature: Feature
Scenario: Scenario
");        
            The_Runner_should_be(
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
        
        [TestMethod]
        public void XUnitRunner()
        {         
            Using("xUnit");        
            Given_the_Feature_is(
@"
Feature: Feature
Scenario: Scenario
");        
            The_Runner_should_be(
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
