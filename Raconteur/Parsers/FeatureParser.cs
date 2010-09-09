using Raconteur.Generators;

namespace Raconteur.Parsers
{
    public interface FeatureParser 
    {
        Feature FeatureFrom(FeatureFile FeatureFile, Project Project);
    }
}