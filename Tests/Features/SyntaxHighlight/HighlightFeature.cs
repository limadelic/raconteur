using System.Linq;
using FluentSpec;
using Raconteur;
using Raconteur.IDEIntegration.SyntaxHighlighting.Classification;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Features.SyntaxHighlight
{
    public class HighlightFeature : FeatureRunner
    {
        protected FeatureTokenTagger Sut { get { return new SUT(Feature); } }

        protected override void Given_the_Feature_contains(string Feature) 
        {
            Given_the_Feature_is(Feature);
        }

        protected void Raconteur_should_highlight(int Count, string Text, string Style)
        {
            Sut.Tags.Where(Tag => 
                Tag.Text == Text && 
                FeatureClassifier.Styles[Tag.Type] == Style)
                .Count().ShouldBe(Count, "Did not find " + Count + " " + Text + " " + Style);
        }

        protected void Raconteur_should_not_highlight(string Text)
        {
            Sut.Tags.Any
            (
                Tag => Tag.Text.TrimLines() == Text.TrimLines()
            ).ShouldBeFalse();
        }

        protected void Raconteur_should_highlight_like_a(string Style, int Count, string Text)
        {
            Raconteur_should_highlight(Count, Text, Style);
        }

        protected void Raconteur_should_highlight_like_a(string Style, string Text)
        {
            Sut.Tags.Any(Tag => 
                Tag.Text.TrimLines() == Text.TrimLines() && 
                FeatureClassifier.Styles[Tag.Type] == Style)
                .ShouldBeTrue();
        }

        class SUT : FeatureTokenTagger
        {
            public SUT(string Feature) : base(Feature) {}

            protected override ITagSpanWrap<FeatureTokenTag> CreateTag(int startLocation, int length, FeatureTokenTypes type)
            {
                var Tag = Create.TestObjectFor<ITagSpanWrap<FeatureTokenTag>>();

                Given.That(Tag).Text.Is(Feature.Substring(startLocation, length));
                Given.That(Tag).Type.Is(type);

                return Tag;
            }
        }
    }
}