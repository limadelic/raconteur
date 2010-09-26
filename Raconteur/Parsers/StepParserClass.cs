namespace Raconteur.Parsers
{
    public class StepParserClass : StepParser 
    {
        public Step StepFrom(string Sentence)
        {
            return new Step{ Name = Sentence.ToValidIdentifier()};
        }
    }
}