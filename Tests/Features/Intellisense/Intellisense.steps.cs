using System.Collections.Generic;
using System.Reflection;
using Common;
using Microsoft.VisualStudio.Language.Intellisense;
using NSubstitute;
using Raconteur.IDE;
using Raconteur.IDEIntegration.Intellisense;

namespace Features.Intellisense 
{
    public partial class Intellisense : BaseSteps
    {
        private CompletionCalculator completions;
        private IEnumerable<Completion> results;

        private void When_I_begin_to_type__on_the_next_line(string fragment)
        {
            var featureItem = Substitute.For<FeatureItem>();
            featureItem.DefaultNamespace = "Features.Intellisense";
            featureItem.Assembly.Returns(Assembly.GetExecutingAssembly().CodeBase);

            completions = new CompletionCalculator
            {
                FeatureItem = featureItem,
                FeatureText = FeatureRunner.Feature
            };

            results = completions.For(fragment);
        }

        private void Then__should_be_displayed(string suggestion)
        {
            results.ShouldContain(suggestion);
        }

        private void Then__should_not_be_displayed(string suggestion)
        {
            results.ShouldNotContain(suggestion);
        }
    }
}
