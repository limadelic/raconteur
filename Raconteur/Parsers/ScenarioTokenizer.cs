using System.Collections.Generic;

namespace Raconteur.Parsers
{
    public interface ScenarioTokenizer 
    {
        List<Scenario> ScenariosFrom(string Content);
    }
}