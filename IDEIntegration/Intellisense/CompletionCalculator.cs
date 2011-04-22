using System;
using System.IO;
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
                    var typeName = ProjectNavigator.DefaultNamespace + "."
                                   + Regex.Match(Feature, RegexExpressions.FeatureDefinition)
                                         .Groups[1].Value.CamelCase().ToValidIdentifier();

                    var type = Assembly.LoadFrom(ProjectNavigator.ActiveAssemblyPath)
                        .GetType(typeName);

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