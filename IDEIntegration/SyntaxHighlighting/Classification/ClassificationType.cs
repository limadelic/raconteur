using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Classification
{
    internal static class ClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Feature")]
        internal static ClassificationTypeDefinition FeatureDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("Scenario")]
        internal static ClassificationTypeDefinition ScenarioDefinition;
    }
}