namespace Raconteur.Parsers
{
    public class StepParserClass : StepParser 
    {
        public string StepFrom(string Sentence)
        {
            return Sentence.ToValidIdentifier();
        }
    }
}