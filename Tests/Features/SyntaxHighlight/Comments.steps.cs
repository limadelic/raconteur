using System.Linq;
using FluentSpec;
using Raconteur.IDEIntegration.SyntaxHighlighting.Classification;

namespace Features.SyntaxHighlight 
{
    public partial class HighlightComments : HighlightFeature {

        protected string HighlightedText { get; set; }

        void Raconteur_should_highlight(string Text)
        {
            HighlightedText = Text;
        }

        void with__style(string Style)
        {
            Sut.Tags.Any(Tag => 
                Tag.Text == HighlightedText && 
                FeatureClassifier.Styles[Tag.Type] == Style)
                .ShouldBeTrue();
        }
    }
}
