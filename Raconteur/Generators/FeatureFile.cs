namespace Raconteur.Generators
{
    public class FeatureFile
    {
        public string Name { get; set; }
        public string ProjectRelativePath { get; private set; }
        public string Content { get; set; }

        public FeatureFile(string Path)
        {
            ProjectRelativePath = Path;
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
        }

        public FeatureFile(){}
    }
}