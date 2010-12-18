
using System.Linq;
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
            ).ShouldBeTrue();
        }
    }
}
