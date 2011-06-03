using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur.Compilers;
using Raconteur.Helpers;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.IDEIntegration.Intellisense
{
    public class CompletionCalculator
    {
        private readonly DTE Dte = Marshal.GetActiveObject("VisualStudio.DTE") as DTE;
        private readonly FeatureParser Parser = ObjectFactory.NewFeatureParser;
        public FeatureCompiler Compiler = ObjectFactory.NewFeatureCompiler;
        private FeatureItem FeatureItem;
        private Feature Feature;

        private string _featureText;
        public virtual string FeatureText
        {
            get { return _featureText; }
            set
            {
                _featureText = value;
                FeatureItem = ObjectFactory.FeatureItemFrom(Dte.ActiveDocument.ProjectItem);
                Feature = Parser.FeatureFrom(FeatureText, FeatureItem);
            }
        }

        public IEnumerable<Completion> For(string fragment)
        {
            var possibilities = Compiler.StepNamesOf(Feature, FeatureItem)
                .Union(Feature.Steps.Select(step => step.Name.IdentifierToEnglish())
                .Union(Settings.Language.Keywords.Select(keyword => keyword + ":")));
            
            return possibilities
                .Where(possibility => possibility.StartsWith(fragment.Trim(), true, null))
                .Select(possibility => new Completion(possibility));
        }
    }
}