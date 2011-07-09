using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur.Compilers;
using Raconteur.Helpers;
using Raconteur.IDE;
using Raconteur.Parsers;

namespace Raconteur.IDEIntegration.Intellisense
{
    public class CompletionCalculator
    {
        private readonly FeatureParser Parser = ObjectFactory.NewFeatureParser;

        public virtual FeatureCompiler Compiler { get; private set; }
        public FeatureItem FeatureItem { get; set; }
        public Feature Feature { get; private set; }
       
        private string _featureText;
        public string FeatureText
        {
            get { return _featureText; }
            set
            {
                _featureText = value; 
                Feature = Parser.FeatureFrom(FeatureText, FeatureItem);
            }
        }

        public CompletionCalculator()
        {
            Feature = new Feature();
            Compiler = ObjectFactory.NewFeatureCompiler;
        }

        public IEnumerable<Completion> For(string fragment)
        {
            var possibilities = Compiler.StepNamesOf(Feature, FeatureItem)
                .Union(Feature.Steps.Select(step => step.Name.InNaturalLanguage())
                .Union(Settings.Language.Keywords.Select(keyword => keyword + ":")));
            
            return possibilities
                .Where(possibility => possibility.StartsWith(fragment.Trim(), true, null))
                .Select(possibility => new Completion(possibility));
        }
    }
}