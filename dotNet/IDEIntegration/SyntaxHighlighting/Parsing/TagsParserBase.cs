using System.Collections.Generic;
using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public abstract class TagsParserBase : TagsParser
    {
        public abstract IEnumerable<ITagSpanWrap<FeatureTokenTag>> Tags { get; }

        protected TagsParserBase(ParsingState ParsingState)
        {
            this.ParsingState = ParsingState;
        }

        protected bool IsLastLine
        {
            get
            {
                return Position + FullLine.Length == Feature.Length;
            }
        }

        protected ParsingState ParsingState;

        protected TagFactory TagFactory
        {
            get { return ParsingState.TagFactory;  }
            set { ParsingState.TagFactory = value; }
        }
        protected string Feature
        {
            get { return ParsingState.Feature;  }
            set { ParsingState.Feature = value; }
        }
        protected int Position
        {
            get { return ParsingState.Position;  }
            set { ParsingState.Position = value; }
        }
        protected string FullLine
        {
            get { return ParsingState.FullLine;  }
            set { ParsingState.FullLine = value; }
        }
        protected string Line
        {
            get { return ParsingState.Line;  }
            set { ParsingState.Line = value; }
        }

        protected ITagSpanWrap<FeatureTokenTag> CreateTag(int StartLocation, int Length, FeatureTokenTypes Type)
        {
            return TagFactory.CreateTag(StartLocation, Length, Type);
        }
    }
}