using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Raconteur.Compilers;
using Raconteur.Helpers;
using Raconteur.Parsers;

namespace Raconteur
{
    public enum StepType
    {
        Simple,
        Table,
        HeaderTable,
        ObjectTable
    }

    public class Step
    {
        public StepType Type;

        public Feature Feature { get; set; }

        public Location Location { get; set; }

        public string Name { get; set; }
        public List<string> Args { get; set; }
        public List<string> OriginalArgs { get; set; }

        public bool HasArgs { get { return Args.Count > 0; } }

        public MethodInfo Method
        {
            get { return IsImplemented ? Implementation.Method : null; } 
            set
            {
                if (value == null) return;

                if (!IsImplemented) Implementation = new StepImplementation();

                Implementation.Method = value;
            } 
        }

        public StepImplementation Implementation { get; set; }
        
        public bool IsImplemented { get { return Implementation != null; } }

        public ParameterInfo[] ArgDefinitions { get { return Method.GetParameters(); } }

        public Step()
        {
            Args = new List<string>();
        }

        public string Call
        {
            get
            {
                if (!IsImplemented || Feature == null || !Feature.HasStepDefinitions) 
                    return Name;

                return  IsImplementedInFeatureSteps ? Name :
                    StepDefinitions.Name + "." + Name;
            } 
        }

        bool IsImplementedInFeatureSteps
        {
            get
            {
                return StepDefinitions == null || 
                    StepDefinitions == Feature.DefaultStepDefinitions;
            }
        }

        Type stepDefinitions;
        Type StepDefinitions
        {
            get
            {
                return stepDefinitions ?? (stepDefinitions = 
                    Feature.StepDefinitions.Where(ImplementsStep).FirstOrDefault());
            } 
        }
        
        bool ImplementsStep(Type StepDefinition)
        {
            return StepDefinition == Method.DeclaringType
                || StepDefinition.IsSubclassOf(Method.DeclaringType);
        }

        public bool HasTable { get { return Table != null; } }

        Table table;
        public Table Table
        {
            get { return table; } 
            set
            {
                table = value;

                Type = table.HasHeader? 
                    StepType.HeaderTable : 
                    StepType.Table;
            }
        }

        public void AddRow(List<string> Row)
        {
            if (!HasTable) Table = new Table();

            Table.Add(Row);
        }

        Type objectArg;
        public Type ObjectArg
        {
            get { return objectArg ?? (objectArg = Method.LastArg().ElementType()); }
        }

        public void Rename(string NewName)
        {
            Name = NewName;
            IsDirty = true;
        }

        public bool IsDirty { get; set; }

        string OriginalSentence { get { return Location.Content; } }

        public string Sentence
        {
            get
            {
                if (!IsDirty) return OriginalSentence;

                if (!HasArgs) return Name;

                if (HasMultilineArg) return SentenceWithMultilineArg;

                return SentenceWithArgs;
            } 
        }

        IEnumerable<string> SimpleArgs { get { return Args.Where(a => !a.IsMultiline()); } }

        string SentenceWithMultilineArg
        {
            get
            {
                var originalSentence = Regex.Split
                (
                    OriginalSentence,
                    "^(.*)\"(.*)$",
                    RegexOptions.Multiline
                )[0].TrimLines();

                return OriginalSentence.Replace(originalSentence, SentenceWithArgs);
            } 
        }

        bool HasMultilineArg
        {
            get { return Args.Any(a => a.IsMultiline()); }
        }

        bool StartsWithArg { get { return OriginalSentence.StartsWith("\""); }}

        IEnumerable<string> NameParts { get { return Regex.Split(Name, @"\s\s"); }}

        string SentenceWithArgs
        {
            get
            {
                var ZipNameAndArgs = StartsWithArg ?
                    SentenceStartingWithArgs :
                    SentenceStartingWithName;
                
                return (ZipNameAndArgs + MissingLink).Trim();
            } 
        }

        string MissingLink
        {
            get
            {
                return SimpleArgs.Count() == NameParts.Count() ? "" : " " + (
                    SimpleArgs.Count() > NameParts.Count() ? SimpleArgs.Last().Quoted() : 
                        NameParts.Last());
            }
        }

        string SentenceStartingWithName
        {
            get
            {
                return string.Join(" ", NameParts
                    .Zip(SimpleArgs, (NamePart, Arg) => NamePart + " " + Arg.Quoted()));
            } 
        }

        string SentenceStartingWithArgs
        {
            get
            {
                return string.Join(" ", SimpleArgs
                    .Zip(NameParts, (Arg, NamePart) =>  Arg.Quoted() + " " + NamePart));
            } 
        }
    }
}