using System.Linq;
using FluentSpec;
using Raconteur.IDEIntegration.SyntaxHighlighting.Classification;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Features.SyntaxHighlight
{
    public class HighlightFeature : FeatureRunner
    {
        protected FeatureTokenTagger Sut { get { return new SUT(Feature); } }

        protected void Raconteur_should_highlight(int Count, string Text, string Color)
        {
            Sut.Tags.Where(Tag => 
                Tag.Text == Text && 
                FeatureClassifier.Styles[Tag.Type] == Color)
                .Count().ShouldBe(Count, "Did not find " + Count + " " + Text + " " + Color);
        }

        class SUT : FeatureTokenTagger
        {
            public SUT(string Feature) : base(Feature) {}

            protected override ITagSpanWrap<FeatureTokenTag> CreateTagWrap(int startLocation, int length, FeatureTokenTypes type)
            {
                var Tag = Create.TestObjectFor<ITagSpanWrap<FeatureTokenTag>>();

                Given.That(Tag).Text.Is(Feature.Substring(startLocation, length));
                Given.That(Tag).Type.Is(type);

                return Tag;
            }
        }
    }
}