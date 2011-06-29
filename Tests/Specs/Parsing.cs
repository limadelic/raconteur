using MbUnit.Framework;
using Raconteur.Parsers;
using Common;

namespace Specs
{
    [TestFixture]
    public class KeepFeaturePosition
    {
        [Test]
        public void Tokenizer_should_include_Scenario_location()
        {
            var Sut = new ScenarioTokenizerClass
            {
                Content =
                @"
                    Feature: Name

                    Scenario: One
                        Step

                    @tag
                    Scenario: Another
                "
            };

            Sut.ScenarioDefinitions[0]
                .Item2.ShouldBe(59, 125);

            Sut.ScenarioDefinitions[1]
                .Item2.ShouldBe(126, 187);
        }
    }
}