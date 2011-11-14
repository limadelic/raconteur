using Microsoft.VisualStudio.Text.Tagging;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class FeatureTokenTag : ITag
    {
        public FeatureTokenTypes Type { get; private set; }

        public FeatureTokenTag(FeatureTokenTypes type) { Type = type; }
    }
}