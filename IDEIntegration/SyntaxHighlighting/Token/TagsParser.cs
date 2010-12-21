using System.Collections.Generic;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    interface TagsParser { IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags { get; } }
}