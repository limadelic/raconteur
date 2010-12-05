using System;

namespace Raconteur
{
    public class ArgBoundary
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Length { get { return End - Start + 1;  } }
    }
}
