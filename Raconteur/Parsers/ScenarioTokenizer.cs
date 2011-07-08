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

        void AddScenario(Location Location)
        {
            var NewScenarioLocation = new Location();

            if (scenarioDefinitions.Count > 0)
            {
                var EndOfCurrentScenario = scenarioDefinitions.Last().Item1.Last().End;

                NewScenarioLocation.Start = Content.IndexOf(Location.Content, EndOfCurrentScenario + 1);

                CurrentScenarioLocation.Content = Content.Substring
                (
                    CurrentScenarioLocation.Start, NewScenarioLocation.Start - CurrentScenarioLocation.Start
                );
            } 
            else
            {
                NewScenarioLocation.Start = Content.IndexOf(Location.Content);
            }

            NewScenarioLocation.Content = Content.Substring(NewScenarioLocation.Start);

            scenarioDefinitions.Add(new Tuple<List<Location>, Location>(
                new List<Location>(), NewScenarioLocation));
        }

        List<Tuple<List<Location>, Location>> scenarioDefinitions;

        Location CurrentScenarioLocation
        {
            get { return scenarioDefinitions.Last().Item2; }
        }

        public List<Tuple<List<Location>, Location>> ScenarioDefinitions
        {
            get
            {
                scenarioDefinitions = new List<Tuple<List<Location>, Location>>();

                foreach (var Location in Locations)
                {
                    if (IsScenarioStart(Location.Content)) AddScenario(Location);

                    if (scenarioDefinitions.Count == 0) continue;

                    scenarioDefinitions.Last().Item1.Add(Location);
                }

                return scenarioDefinitions;
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

        IEnumerable<Location> Locations
        {
            get
            {
                CurrentLocation = 0;

                return Regex.Split(Content, Environment.NewLine)
                    .Select(Location)
                    .Where(HasCode);
            }
        }

        int CurrentLocation;

        Location Location(string Line)
        {
            var TrimmedLine = Line.Trim();

            var Result = TrimmedLine.IsEmpty() ? 
                new Location(CurrentLocation, TrimmedLine) : 
                new Location
                (
                    Content.IndexOf(TrimmedLine, CurrentLocation),
                    TrimmedLine
                );

            CurrentLocation = Result.End;

            return Result;
        }

        bool InsideArg;

        bool HasCode(Location Location) { return HasCode(Location.Content); }

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