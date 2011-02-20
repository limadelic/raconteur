using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;

namespace Raconteur.IDEIntegration.Intellisense
{
    public class CompletionCalculator
    {
        public string Feature { get; set; }

        public IEnumerable<Completion> For(string fragment)
        {
            var possibilities = Feature.TrimLines().Lines()
                .Union(Settings.Language.Keywords);
            
            return possibilities
                .Where(possibility => possibility.StartsWith(fragment.Trim(), true, null))
                .Select(possibility => new Completion(possibility));
        }
    }
}