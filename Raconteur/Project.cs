using System;
using System.Collections.Generic;
using System.IO;

namespace Raconteur
{
    public class Project
    {
        public string ProjectName { get; set; }
        public string AssemblyName { get; set; }
        public string ProjectFolder { get; set; }
        public string DefaultNamespace { get; set; }

        public List<FeatureFile> FeatureFiles { get; private set; }

        public Project()
        {
            FeatureFiles = new List<FeatureFile>();
        }

        public FeatureFile GetOrCreateFeatureFile(string FeatureFileName)
        {
            FeatureFileName = Path.GetFullPath(
                Path.Combine(ProjectFolder, FeatureFileName));

            return FeatureFiles.Find(File => 
                File.GetFullPath(this).Equals(FeatureFileName, StringComparison.InvariantCultureIgnoreCase)) 
                ?? new FeatureFile(FeatureFileName);
        }
    }
}