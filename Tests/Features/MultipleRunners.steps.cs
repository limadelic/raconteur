using Raconteur;

namespace Features 
{
    public partial class UsingMultipleRunners : FeatureRunner
    {
        void Using(string xUnit)
        {
            Settings.XUnit = xUnit;
        }
    }
}
