using Raconteur.Generators;
using Raconteur.IDE;

namespace Raconteur.Parsers
{
    public interface FeatureParser 
    {
        Feature FeatureFrom(FeatureFile FeatureFile, FeatureItem FeatureItem);
    }
}