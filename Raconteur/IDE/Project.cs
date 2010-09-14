namespace Raconteur.IDE
{
    public interface Project
    {
        string DefaultNamespace { get; set; }

        void AddStepDefinitions(string Content);
        bool ContainsStepDefinitions { get; }
    }
}