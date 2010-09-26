using System.Collections.Generic;

namespace Raconteur
{
    public class Step
    {
        public string Name { get; set; }
        public List<string> Args { get; set; }

        public Step()
        {
            Args = new List<string>();
        }
    }
}