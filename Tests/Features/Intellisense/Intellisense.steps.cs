using System;
using System.Collections.Generic;
using FluentSpec;
using Raconteur.IDEIntegration.Intellisense;

namespace Features.Intellisense 
{
    public partial class Intellisense
    {
        private CompletionCalculator completions;
        private List<string> results;

        private void Given_the_Feature_contains(string feature)
        {
            completions = new CompletionCalculator { Feature = feature };
        }

        private void When_I_begin_to_type__on_the_next_line(string fragment)
        {
            results = completions.For(fragment);
        }

        private void Then__should_be_displayed(string suggestion)
        {
            results.ShouldContain(suggestion);
        }
    }
}
