namespace Raconteur.IDE
{
    public class FeatureFile
    {
        public string Name { get; set; }
        public string ProjectRelativePath { get; private set; }
        public string Content { get; set; }

        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(Content); }
        }

        public FeatureFile(){}

        public FeatureFile(string Path)
        {
            ProjectRelativePath = Path;
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
        }
    }
}