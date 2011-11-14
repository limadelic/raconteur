namespace Raconteur.Parsers
{
    public class Location
    {
        public Location() {}

        public Location(int Start, string Content) 
        {
            this.Start = Start;
            this.Content = Content;
        }

        public int Start { get; set; }

        public string Content { get; set; }

        public int End { get { return Start + Content.Length; } }
    }
}