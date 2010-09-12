namespace Raconteur.IDE
{
    public interface Project
    {
        string DefaultNamespace { get; }

        void AddStepDefinitions(string FeatureFile, string Content);
        bool ContainsStepDefinitions(string FeatureFile);
    }
}