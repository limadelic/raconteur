using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Examples.Demo 
{
    [TestClass]
    public partial class DemoForVersion0_2 
    {

        
        [TestMethod]
        public void Retweet()
        {         
            Given_the_tweet__by("check out Raconteur!", "limadelic");        
            When_I_retweet_it();        
            Then_the_tweet_should_be("RT @limadelic check out Raconteur!");
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
@"
HTTP/1.1 200 OK
Content-Length: 267
Content-Type: application/xml
Date: Wed, 19 Nov 2008 21:48:10 GMT
");
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
        public void SupportingMultipleServicesToShortenUrls_1()
        {         
            Select_url_shortener("is.gd");        
            Tweet("visit http://raconteur.github.com/");        
            Verify_the_url_was("http://is.gd/gLEoH");
        }
        
        [TestMethod]
        public void SupportingMultipleServicesToShortenUrls_2()
        {         
            Select_url_shortener("bit.ly");        
            Tweet("visit http://raconteur.github.com/");        
            Verify_the_url_was("http://bit.ly/cTLoh7");
        }
        
        [TestMethod]
        public void SupportingMultipleServicesToShortenUrls_3()
        {         
            Select_url_shortener("tinyurl");        
            Tweet("visit http://raconteur.github.com/");        
            Verify_the_url_was("http://tinyurl.com/3xj9z7t");
        }

    }
}
