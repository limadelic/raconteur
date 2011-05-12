using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using EnvDTE;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur.Helpers;
using Raconteur.IDEIntegration.Helpers;

namespace Raconteur.IDEIntegration.Intellisense
{
    public class CompletionCalculator
    {
        public virtual string Feature { get; set; }
        
        string FeatureWithoutArgValues
        {
            get { return Regex.Replace(Feature, "\"[^\"]*\"", "\"\""); }
        }

        private IEnumerable<string> RelevantText
        {
            get
            {
                return FeatureWithoutArgValues.TrimLines().Lines()
                    .SkipWhile(line => !line.StartsWith(Settings.Language.Scenario))
                    .Where(line => !line.StartsWithKeyword()).ToList();
            }
        }

        public IEnumerable<Completion> For(string fragment)
        {
            var possibilities = RelevantText.Union(MethodList)
                .Union(Settings.Language.Keywords.Select(keyword => keyword + ":"));
            
            return possibilities
                .Where(possibility => possibility.StartsWith(fragment.Trim(), true, null))
                .Select(possibility => new Completion(possibility));
        }

        protected IEnumerable<string> MethodList
        {
            get
            {
                try
                {
                    var typeName = Regex.Match(Feature, RegexExpressions.FeatureDefinition)
                                     .Groups[1].Value.CamelCase().ToValidIdentifier();

                    var type = ProjectNavigator.TypeInCurrentProject(typeName);

                    return type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Select(method => method.Name.IdentifierToEnglish());
                }
                catch (Exception)
                {
                    return new List<string>();
                }
            }
        }
    }
}