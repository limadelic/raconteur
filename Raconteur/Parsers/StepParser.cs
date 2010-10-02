namespace Raconteur.Parsers
{
    public interface StepParser
    {
        Step StepFrom(string Sentence);
        Step OutlineStepFrom(string Sentence);
    }
}