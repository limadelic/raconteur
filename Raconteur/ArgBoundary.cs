namespace Raconteur
{
    public class ArgBoundary
    {
        public int Start { get; set; }
        public int End { get; set; }
        public bool IsOpen { get { return End == 0; } }
    }
}
