using System.Text.RegularExpressions;
using Raconteur.Helpers;

namespace Raconteur.Refactoring
{
    public class RenameStep : Refactor
    {
        public string FileName { get; set; }
        public string OriginalName { get; set; }
        public string NewName { get; set; }

        public RenameStep(string FileName, string OriginalName, string NewName)
        {
            this.FileName = FileName;
            this.OriginalName = OriginalName.Replace("_"," ");
            this.NewName = NewName.Replace("_"," ");
        }

        public void ExecuteNew()
        {
/*
            var Feature = ObjectFactory.NewFeatureParser
                .FeatureFrom(FeatureContent, ObjectFactory.NewFeatureItem);
*/

/*
            Feature.Steps
                .Where(s => s.Name == OriginalName)
                .ForEach(s => s.Rename(NewName));

            Feature.Refresh();

            Write(Feature.Content);
*/
        }

        public void Execute()
        {
            var Pattern = @"^((\s|\t)*)(" + OriginalName + @")((\s|\t)*)$";

            Write(Regex.Replace
            (
                FeatureContent, 
                Pattern, 
                "$1" + NewName + "$4", 
                RegexOptions.Multiline
            ));
        }

        string FeatureFile
        {
            get { return FileName.Replace("steps.cs", "feature"); }
        }

        public virtual string FeatureContent
        {
            get { return System.IO.File.ReadAllText(FeatureFile); }
        }

        public virtual void Write(string NewContent)
        {
            System.IO.File.WriteAllText(FeatureFile, NewContent);
        }
    }
}