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

        public FeatureFile GetOrCreateFeatureFile(string featureFileName)
        {
            featureFileName = Path.GetFullPath(Path.Combine(ProjectFolder, featureFileName));

            return FeatureFiles.Find(ff => 
                ff.GetFullPath(this).Equals(featureFileName, StringComparison.InvariantCultureIgnoreCase)) ??
                    new FeatureFile(featureFileName);
        }
    }
}