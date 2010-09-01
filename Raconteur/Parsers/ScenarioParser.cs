using System.Collections.Generic;

namespace Raconteur.Parsers
{
    public interface ScenarioParser
    {
        List<Scenario> ScenariosFrom(string Content);
    }
}