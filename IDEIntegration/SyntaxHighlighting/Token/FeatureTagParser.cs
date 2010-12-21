using System;
using System.Linq;
using ITagsWrap=System.Collections.Generic.IEnumerable<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;
using TagsWrap=System.Collections.Generic.List<Raconteur.IDEIntegration.SyntaxHighlighting.Token.ITagSpanWrap<Raconteur.IDEIntegration.SyntaxHighlighting.Token.FeatureTokenTag>>;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Token
{
    public class FeatureTagParser : TagsParserBase
    {
        readonly TagsParser Keywords;
        readonly TagsParser Args;
        readonly TagsParser Table;
        readonly TagsParser Comments;
        readonly TagsParser Scenarios;
        readonly TagsParser Multiline;

        public FeatureTagParser(TagFactory TagFactory, string Feature) 
            : base(new ParsingState())
        {
            this.TagFactory = TagFactory;
            this.Feature = Feature;

            Keywords = new KeywordParser(ParsingState);
            Args = new ArgsParser(ParsingState);
            Table = new TableParser(ParsingState);
            Comments = new CommentsParser(ParsingState);
            Scenarios = new ScenariosParser(ParsingState);
            Multiline = new MultilineParser(ParsingState);
        }

        public override ITagsWrap Tags
        {
            get
            {
                Position = 0;
                Action<int> NextPosition = LineLength => Position += LineLength + 2;

                return 
                    Feature
                    .Lines()
                    .ApplyLengthTo(NextPosition)
                    .SelectMany(TagsFromLine);
            }
        }

        ITagsWrap TagsFromLine(string Line)
        {
            FullLine = Line;
            this.Line = Line.Trim();

            return 
                Multiline.Tags ?? 
                Comments.Tags ??
                (Keywords.Tags ??
                 Table.Tags ??
                 Args.Tags ??
                 new TagsWrap())
                 .Union(Scenarios.Tags);
        }
    }
}