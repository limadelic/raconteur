using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Raconteur.IDEIntegration.SyntaxHighlighting.Classification;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting
{
// ReSharper disable RedundantDefaultFieldInitializer
    [Export(typeof(ITaggerProvider))]
    [ContentType("Raconteur")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class FeatureClassifierProvider : ITaggerProvider
    {
        [Import] internal IBufferTagAggregatorFactoryService Factory = null;
        [Import] internal IClassificationTypeRegistryService Registry = null;

        [Export]
        [Name("Raconteur")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition RaconteurContentType = null;

        [Export]
        [FileExtension(".feature")]
        [ContentType("Raconteur")]
        internal static FileExtensionToContentTypeDefinition RaconteurFileType = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            var tagAggregator = Factory.CreateTagAggregator<FeatureTokenTag>(buffer);
            return new FeatureClassifier(tagAggregator, Registry) as ITagger<T>;
        }
    }
// ReSharper restore RedundantDefaultFieldInitializer
}