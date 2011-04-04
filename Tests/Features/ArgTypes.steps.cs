
namespace Features 
{
    public partial class ArgTypes 
    {
        public void Given_the_zipcode_is(string ZipCode) {}

        public void Given_the_Board(params string[][] Board) {}

        public void Given_the_Addresses_(string State, string Zip) {}

        public void Given_the__Addresses_(string Country, string State, string Zip) {}

        public void Given__is_next_on_the_Board(string Player, params string[][] Board) {}
    }
}
