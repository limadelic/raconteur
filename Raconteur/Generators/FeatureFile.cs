using System;
using System.IO;
using System.Linq;

namespace Raconteur.Generators
{
    public class FeatureFile
    {
        public string ProjectRelativePath { get; private set; }
        public string CustomNamespace { get; set; }
        
        public string Name { get; set; }
        public string Content { get; set; }
        public string Namespace { get; set; }

        public string GetFullPath(Project Project)
        {
            return Path.GetFullPath(
                Path.Combine(Project.ProjectFolder, ProjectRelativePath));
        }

        public FeatureFile(string Path)
        {
            ProjectRelativePath = Path;
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
        }

        public FeatureFile() { }

        public void Load(Project Project)
        {
            Namespace = Project.DefaultNamespace;

            Content = File.ReadAllLines(GetFullPath(Project)).ToList()
                .Aggregate("", (Result, Current) => 
                    Result + Environment.NewLine + Current);
        }
    }
}