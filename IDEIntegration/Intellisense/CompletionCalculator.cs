using System.Linq;
using System.Collections.Generic;

namespace Raconteur.IDEIntegration.Intellisense
{
    public class CompletionCalculator
    {
        public string Feature { get; set; }

        public List<string> For(string fragment)
        {
            var possibilities = Feature.TrimLines().Lines()
                .Union(Settings.Language.Keywords);
            
            return possibilities.Where(keyword => 
                keyword.StartsWith(fragment, true, null)).ToList();
        }
    }
}