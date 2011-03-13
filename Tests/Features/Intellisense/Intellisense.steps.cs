using System;
using System.Collections.Generic;
using Common;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur.IDEIntegration.Intellisense;

namespace Features.Intellisense 
{
    public partial class Intellisense
    {
        private CompletionCalculator completions;
        private IEnumerable<Completion> results;

        private void When_I_begin_to_type__on_the_next_line(string fragment)
        {
            completions = new CompletionCalculator { Feature = FeatureRunner.Feature };
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
