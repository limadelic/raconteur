using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Classification
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Feature")]
    [Name("Feature")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class FeatureDefinition : ClassificationFormatDefinition
    {
        public FeatureDefinition()
        {
            DisplayName = "Feature";
            IsBold = true;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "Scenario")]
    [Name("Scenario")]
    [UserVisible(false)]
    [Order(Before = Priority.Default)]
    internal sealed class ScenarioDefinition : ClassificationFormatDefinition
    {
        public ScenarioDefinition()
        {
            DisplayName = "Scenario"; 
            IsItalic = true;
        }
    }
}