using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples 
{
    [TestClass]
    public partial class V0_2Screencast 
    {
        
        [TestMethod]
        public void Retweet()
        {         
            Given_a_tweet_by__with_content("limadelic", "check out Raconteur!");        
            When_I_retweet_it();        
            Then_the_update_text_should_be("RT @limadelic check out Raconteur!");
        }
        
        [TestMethod]
        public void HelpFileIsProductSpecific()
        {         
            Load_Products();        
            The_Help_File_should_be("Products.pdf");        
            Select("Webcams");        
            The_Help_File_should_be("Webcams.pdf");
        }
        
        [TestMethod]
        public void ResponseHeaderParser()
        {         
            Given_the_Response(
@"HTTP/1.1 200 OK
Content-Length: 267
Content-Type: application/xml
Date: Wed, 19 Nov 2008 21:48:10 GMT
");        
            The_Header_should_have(200, 267);
        }
        
        [TestMethod]
        public void ScalarMultiplicationOfMatrices()
        {         
            M__
            (        
                new[] {1, 2},        
                new[] {0, -1}
            );        
            _2___M__
            (        
                new[] {2, 4},        
                new[] {0, -2}
            );
        }
        
        [TestMethod]
        public void SupportingMultipleServicesToShortenUrls1()
        {         
            Select_url_shortener("is.gd");        
            Post_a_status_update("http://raconteur.github.com/");        
            Verify_the_urls_was("http://is.gd/gLEoH");
        }
        
        [TestMethod]
        public void SupportingMultipleServicesToShortenUrls2()
        {         
            Select_url_shortener("bit.ly");        
            Post_a_status_update("http://raconteur.github.com/");        
            Verify_the_urls_was("http://bit.ly/cTLoh7");
        }
        
        [TestMethod]
        public void SupportingMultipleServicesToShortenUrls3()
        {         
            Select_url_shortener("tinyurl");        
            Post_a_status_update("http://raconteur.github.com/");        
            Verify_the_urls_was("http://tinyurl.com/3xj9z7t");
        }

    }
}
