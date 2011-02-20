using System;
using System.Collections;
using System.Collections.Generic;
using FluentSpec;
using Microsoft.VisualStudio.Language.Intellisense;
using Raconteur.IDEIntegration.Intellisense;
using Specs;

namespace Features.Intellisense 
{
    public partial class Intellisense
    {
        private CompletionCalculator completions;
        private IEnumerable<Completion> results;

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
