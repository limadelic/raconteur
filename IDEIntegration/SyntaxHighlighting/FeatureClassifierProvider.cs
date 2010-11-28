using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("Raconteur")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class FeatureClassifierProvider : ITaggerProvider
    {
        [Import] internal IBufferTagAggregatorFactoryService Factory;
        [Import] internal IClassificationTypeRegistryService Registry;

        [Export]
        [Name("Raconteur")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition RaconteurContentType;

        [Export]
        [FileExtension(".feature")]
        [ContentType("Raconteur")]
        internal static FileExtensionToContentTypeDefinition RaconteurFileType;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            var tagAggregator = Factory.CreateTagAggregator<FeatureTokenTag>(buffer);
            return new FeatureClassifier(buffer, tagAggregator, Registry) as ITagger<T>;
        }
    }
}