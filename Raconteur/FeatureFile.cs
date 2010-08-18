using System.IO;

namespace Raconteur
{
    public class FeatureFile
    {
        public string ProjectRelativePath { get; private set; }
        public string CustomNamespace { get; set; }
        public string GetFullPath(Project project)
        {
            return Path.GetFullPath(Path.Combine(project.ProjectFolder, ProjectRelativePath));
        }

        public FeatureFile(string path)
        {
            ProjectRelativePath = path;
        }
    }
}