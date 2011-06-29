using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Raconteur.Helpers;

namespace Raconteur.Parsers
{
    public interface ScenarioTokenizer 
    {
        List<Scenario> ScenariosFrom(string Content);
    }
    
    public class ScenarioTokenizerClass : ScenarioTokenizer
    {
        public ScenarioParser ScenarioParser { get; set; }

        public string Content;

        public List<Scenario> ScenariosFrom(string Content)
        {
            this.Content = Content;

            return 
            (
                from Definition in ScenarioDefinitions
                select ScenarioParser.ScenarioFrom
                (
                    Definition.Item1, 
                    Definition.Item2
                )
            ).ToList();
        }

        void AddScenario(List<Tuple<List<string>, Location>> Scenarios, string Line)
        {
            var NewScenarioLocation = new Location
            {
                End = Content.Length
            };

            if (Scenarios.Count > 0)
            {
                var LastScenarioLocation = Scenarios.Last().Item2;

                NewScenarioLocation.Start = Content.IndexOf(Line, LastScenarioLocation.Start + 1);
                
                LastScenarioLocation.End = NewScenarioLocation.Start - 1;
                LastScenarioLocation.Set(Content);
            } 
            else
            {
                NewScenarioLocation.Start = Content.IndexOf(Line);
            }

            NewScenarioLocation.Set(Content);

            Scenarios.Add(new Tuple<List<string>, Location>(
                new List<string>(), NewScenarioLocation));
        }

        public List<Tuple<List<string>, Location>> ScenarioDefinitions
        {
            get
            {
                var Results = new List<Tuple<List<string>, Location>>();

                foreach (var Line in Lines)
                {
                    if (IsScenarioStart(Line)) AddScenario(Results, Line);

                    if (Results.Count == 0) continue;

                    Results.Last().Item1.Add(Line);
                }

                return Results;
            }
        }

        bool HasTags;

        bool IsScenarioStart(string Line)
        {
            if (InsideArg) return false;

            Line = Line.TrimStart();

            if (!HasTags && Line.StartsWith("@"))
            {
                HasTags = true;
                return true;
            }

            if (!Line.StartsWith(Settings.Language.Scenario)) return false;

            if (!HasTags) return true;
            
            return HasTags = false;
        }

        IEnumerable<string> Lines
        {
            get
            {
                return Regex.Split(Content, Environment.NewLine)
                    .Select(Line => Line.Trim())
                    .Where(HasCode);
            }
        }

        bool InsideArg;

        bool HasCode(string Line)
        {
            if (!InsideComment && Line == "\"") InsideArg = !InsideArg;

            return InsideArg || !(Line.IsEmpty() || IsComment(Line));
        }

        bool InsideComment;

        bool IsComment(string Line)
        {
            if (Line.StartsWith("/*")) InsideComment = !InsideComment;

            var isComment = InsideComment || Line.StartsWith("//");
            
            if (Line.StartsWith("*/")) InsideComment = !InsideComment;

            return isComment;
        }
    }
}