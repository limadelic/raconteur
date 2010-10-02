using System.Collections.Generic;

namespace Raconteur.Parsers
{
    public interface ScenarioParser
    {
        Scenario ScenarioFrom(List<string> Definition);
    }
}