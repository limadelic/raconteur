namespace Raconteur.IDE
{
    public interface FeatureItem
    {
        string DefaultNamespace { get; set; }

        void AddStepDefinitions(string Content);
        bool ContainsStepDefinitions { get; }
        string ExistingStepDefinitions { get; }
        string Assembly { get; }

        EnvDTE.Project Project { get; }
    }
}