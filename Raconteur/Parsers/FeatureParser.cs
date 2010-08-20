namespace Raconteur.Parsers
{
    public interface FeatureParser 
    {
        Feature FeatureFrom(string Content);
    }
}