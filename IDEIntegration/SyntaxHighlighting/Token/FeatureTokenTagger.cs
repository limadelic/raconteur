using System;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Raconteur.IDEIntegration.SyntaxHighlighting.Parsing;
using ITags=System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.Text.Tagging.ITagSpan<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public interface TagFactory
    {
        ITagSpanWrap<FeatureTokenTag> CreateTag(int StartLocation, int Length, FeatureTokenTypes Type);
    }

    public interface ITagSpanWrap<T> where T : ITag
    {
        TagSpan<T> Core { get; }

        string Text { get; }

        FeatureTokenTypes Type { get; }
    }

    public class TagSpanWrap<T> : ITagSpanWrap<T> where T : ITag
    {
        public TagSpan<T> Core { get; set; }

        public string Text { get { return Core.Span.GetText(); } }

        public FeatureTokenTypes Type { get { return (Core.Tag as FeatureTokenTag).Type; } }
    }

    public class FeatureTokenTagger : ITagger<FeatureTokenTag>, TagFactory
    {
        protected string Feature;
        ITextSnapshot Snapshot;

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public ITags GetTags(NormalizedSnapshotSpanCollection Spans)
        {
//            System.Diagnostics.Debugger.Launch();
            Snapshot = Spans[0].Snapshot;
            Feature = Snapshot.GetText();

/*
            return spans.SelectMany(span => AllTags
                .Where(tagSpan => tagSpan.Span.IntersectsWith(span)));
*/
            return
                from Tag 
                in new FeatureTagParser(this, Feature).Tags
                select Tag.Core;
        }

        ITagSpanWrap<FeatureTokenTag> EmptyTag
        {
            get { return CreateTag(0, 0, FeatureTokenTypes.Keyword); }
        }

        public virtual ITagSpanWrap<FeatureTokenTag> CreateTag(int StartLocation, int Length, FeatureTokenTypes Type)
        {
            try 
            {
                var Span = new SnapshotSpan(Snapshot, new Span(StartLocation, Length));

                if (TagsChanged != null) TagsChanged(this, new SnapshotSpanEventArgs(Span));

                var NewTag = new TagSpan<FeatureTokenTag>(Span, new FeatureTokenTag(Type));

                return new TagSpanWrap<FeatureTokenTag> { Core = NewTag };

            } catch { return EmptyTag; }
        }
    }
}