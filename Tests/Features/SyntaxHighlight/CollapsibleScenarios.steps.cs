using System.Linq;
using Features.StepDefinitions;
using FluentSpec;
using Raconteur;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Features.SyntaxHighlight 
{
    public partial class MakeScenariosCollapsibles : HighlightFeature
    {
        void Raconteur_should_allow_to_collapse(string Region)
        {
            Sut.Tags.Any
            (
                Tag => Tag.Text.TrimLines() == Region.TrimLines()
                    && Tag.Type == FeatureTokenTypes.ScenarioBody
            )
            .ShouldBeTrue
            (
                string.Format("did not collapse [{0}] in [{1}]", Region.TrimLines(), 
                    Sut.Tags.Aggregate("", (Tags, Tag) => Tags + "," + Tag.Text)) 
            );
        }

        void Raconteur_should_not_allow_to_collapse(string Region)
        {
            Sut.Tags.Any
            (
                Tag => Tag.Text.TrimLines() == Region.TrimLines()
                    && Tag.Type == FeatureTokenTypes.ScenarioBody
            )
            .ShouldBeFalse
            (
                string.Format("should not have collapsed [{0}]", Region.TrimLines()) 
            );
        }
    }
}
