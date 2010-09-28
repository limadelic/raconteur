using Raconteur;
using Raconteur.Generators;
using Raconteur.IDE;

namespace Features
{
    public class FeatureRunner
    {
        protected string Feature;

        protected string Runner
        {
            get
            {
                var FeatureFile = new FeatureFile { Content = Feature };

                var Parser = ObjectFactory.NewFeatureParser;

                var RunnerGenerator = new RunnerGenerator();

                var Project = new VsFeatureItem { DefaultNamespace = "Features" };

                var NewFeature = Parser.FeatureFrom(FeatureFile, Project);

                return RunnerGenerator.RunnerFor(NewFeature);
            } 
        }
    }
}