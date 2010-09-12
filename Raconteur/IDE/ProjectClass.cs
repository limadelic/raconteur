namespace Raconteur.IDE
{
    public class ProjectClass : Project
    {
        public string ProjectName { get; set; }
        public string AssemblyName { get; set; }
        public string ProjectFolder { get; set; }
        public string DefaultNamespace { get; set; }

        public void AddStepDefinitions(string FeatureFile, string Content)
        {
        }
    }
}