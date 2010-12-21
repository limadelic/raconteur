using Raconteur.IDEIntegration.SyntaxHighlighting.Token;

namespace Raconteur.IDEIntegration.SyntaxHighlighting.Parsing
{
    public class ParsingState
    {
        public TagFactory TagFactory;
        public string Feature;

        public int Position;
        public string FullLine;
        public string Line;
    }
}