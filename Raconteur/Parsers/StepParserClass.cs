using System.Linq;

namespace Raconteur.Parsers
{
    public class StepParserClass : StepParser 
    {
        public Step StepFrom(string Sentence)
        {
            var Tokens = Sentence.Split(new[]{'"'});
             
            return new Step
            { 
                Name = Tokens.Evens().Aggregate((Name, Token) => 
                    Name + Token).ToValidIdentifier(),
                Args = Tokens.Odds().Select(x => '"' + x +'"').ToList() 
            };
        }
    }
}