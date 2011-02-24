using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur.Parsers;

namespace Raconteur.IDEIntegration.Intellisense
{
    public class CompletionCalculator
    {
        public string Feature { get; set; }

        private IEnumerable<string> RelevantText
        {
            get
            {
                var linesWithArgs = Feature.TrimLines().Lines()
                    .SkipWhile(line => !line.StartsWith(Settings.Language.Scenario))
                    .Where(line => !line.StartsWithKeyword()).ToList();

                var tempList = new List<string>();
                var multiLineArgFound = false;

                foreach (var line in linesWithArgs)
                {
                    if (line == "\"") multiLineArgFound = !multiLineArgFound;

                    if (!multiLineArgFound) tempList.Add(line);
                }


                return tempList;
            }
        }

        public IEnumerable<Completion> For(string fragment)
        {
            var possibilities = RelevantText
                .Union(Settings.Language.Keywords.Select(keyword => keyword + ":"));
            
            return possibilities
                .Where(possibility => possibility.StartsWith(fragment.Trim(), true, null))
                .Select(possibility => new Completion(possibility));
        }
    }
}