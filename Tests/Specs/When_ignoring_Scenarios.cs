using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;

namespace Specs
{
    [TestFixture]
    public class When_ignoring_Scenarios
    {
        [Test]
        public void should_add_Scenario_Tags_to_tests()
        {
            var Scenario = new Scenario { Tags = {"ignore"} };

            var Sut = new ScenarioGenerator(Scenario);

            Sut.Code.ShouldContain(@"[Ignore]");
        }

        [Test]
        public void should_include_ignoring_reason()
        {
            var Scenario = new Scenario { Tags = {"ignore just because"} };

            var Sut = new ScenarioGenerator(Scenario);

            Sut.Code.ShouldContain(@"[Ignore] // just because");
        }
    }
}