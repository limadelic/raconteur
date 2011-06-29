namespace Raconteur.Parsers
{
    public class Location
    {
        public Location() {}

        public Location(int Start, string Content) 
        {
            this.Start = Start;
            this.Content = Content;

            End = Start + Content.Length;
        }

        public int Start, End;

        public string Content { get; set; }

        public void Set(string Contents)
        {
            Content = Contents.Substring(Start, End - Start);
        }
    }
}