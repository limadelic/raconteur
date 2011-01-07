using FluentSpec;
using MbUnit.Framework;
using Raconteur;
using Raconteur.Generators;

namespace Specs
{
    [TestFixture]
    public class When_ignoring_Scenarios
    {
        readonly Scenario Scenario = new Scenario { Tags = { "ignore" } };

        [Test]
        public void should_add_Scenario_Tags_to_tests()
        {
            var Sut = new ScenarioGenerator(Scenario);

            Sut.Code.ShouldContain(@"[Ignore]");
        }
    }
}