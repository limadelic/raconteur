namespace Raconteur.Generators
{
    public interface RaconteurGenerator
    {
        string Generate(string FeatureFilePath, string Content);
    }
}