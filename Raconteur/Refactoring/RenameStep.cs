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

        public void Execute()
        {
            var FeatureFile = FileName.Replace("steps.cs", "feature");
            var FeatureContent = System.IO.File.ReadAllText(FeatureFile);
            var NewContent = FeatureContent.Replace(OriginalName, NewName);

            System.IO.File.WriteAllText(FeatureFile, NewContent);
        }
    }
}