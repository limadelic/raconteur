
using System;
using EnvDTE;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Raconteur.IDEIntegration.SyntaxHighlighting;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Features 
{
    public partial class SyntaxHighlighting 
    {
//        ITagger<FeatureTokenTag> tagger = new FeatureTokenTagProvider().CreateTagger<FeatureTokenTag>(new DummyTextBuffer());
// 
        private void When_the_Feature_contains(string step)
        {
//            var spans = new NormalizedSnapshotSpanCollection(new SnapshotSpan()
//            tagger.GetTags()
        }
    }

    public class DummyTextBuffer : ITextBuffer
    {
        public PropertyCollection Properties
        {
            get { throw new NotImplementedException(); }
        }

        public ITextEdit CreateEdit(EditOptions options, int? reiteratedVersionNumber, object editTag)
        {
            throw new NotImplementedException();
        }

        public ITextEdit CreateEdit()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyRegionEdit CreateReadOnlyRegionEdit()
        {
            throw new NotImplementedException();
        }

        public void TakeThreadOwnership()
        {
            throw new NotImplementedException();
        }

        public bool CheckEditAccess()
        {
            throw new NotImplementedException();
        }

        public void ChangeContentType(IContentType newContentType, object editTag)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Insert(int position, string text)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Delete(Span deleteSpan)
        {
            throw new NotImplementedException();
        }

        public ITextSnapshot Replace(Span replaceSpan, string replaceWith)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly(int position)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly(int position, bool isEdit)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly(Span span)
        {
            throw new NotImplementedException();
        }

        public bool IsReadOnly(Span span, bool isEdit)
        {
            throw new NotImplementedException();
        }

        public NormalizedSpanCollection GetReadOnlyExtents(Span span)
        {
            throw new NotImplementedException();
        }

        IContentType ITextBuffer.ContentType
        {
            get { return ContentType; }
        }

        public IContentType ContentType
        {
            get { throw new NotImplementedException(); }
        }

        public ITextSnapshot CurrentSnapshot
        {
            get { throw new NotImplementedException(); }
        }

        public bool EditInProgress
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<SnapshotSpanEventArgs> ReadOnlyRegionsChanged;
        public event EventHandler<TextContentChangedEventArgs> Changed;
        public event EventHandler<TextContentChangedEventArgs> ChangedLowPriority;
        public event EventHandler<TextContentChangedEventArgs> ChangedHighPriority;
        public event EventHandler<TextContentChangingEventArgs> Changing;
        public event EventHandler PostChanged;
        public event EventHandler<ContentTypeChangedEventArgs> ContentTypeChanged;
        PropertyCollection IPropertyOwner.Properties
        {
            get { return Properties; }
        }
    }

}
