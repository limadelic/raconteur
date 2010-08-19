using System.IO;

namespace Raconteur
{
    public class FeatureFile
    {
        public string ProjectRelativePath { get; private set; }
        public string CustomNamespace { get; set; }
        
        public string Name { get; set; }
        public string Content { get; set; }

        public string GetFullPath(Project Project)
        {
            return Path.GetFullPath(Path.Combine(Project.ProjectFolder, ProjectRelativePath));
        }

        public FeatureFile(string Path)
        {
            ProjectRelativePath = Path;
        }

        public FeatureFile() { }
    }
}