using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    interface TagsParser { IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags { get; } }
}