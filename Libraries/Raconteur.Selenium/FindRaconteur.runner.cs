using MbUnit.Framework;

namespace Raconteur.Selenium 
{
    [TestFixture]
    public partial class FindRaconteur 
    {

        
        [Test]
        public void FindRaconteurInGoogle()
        {         
            Given_I_go_to("http://google.com");        
            When_I_look_for("Raconteur");        
            The_page_title_should_be("raconteur - Google Search");
        }

    }
}
