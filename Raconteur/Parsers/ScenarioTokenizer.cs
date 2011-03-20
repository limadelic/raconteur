using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                select ScenarioParser.ScenarioFrom(Definition)
            ).ToList();
        }

        public List<List<string>> ScenarioDefinitions
        {
            get
            {
                var Results = new List<List<string>>();

                foreach (var Line in Lines)
                {
                    if (IsScenarioStart(Line))
                        Results.Add(new List<string>());

                    if (Results.Count == 0) continue;

                    Results.Last().Add(Line);
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