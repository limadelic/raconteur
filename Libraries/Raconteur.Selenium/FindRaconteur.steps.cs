
using System;
using FluentSpec;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Raconteur.Selenium 
{
    public partial class FindRaconteur
    {
        IWebDriver Driver;

        [FixtureSetUp]
        void Setup()
        {
            Driver = new FirefoxDriver(); 
        }

        void Given_I_go_to(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        void When_I_look_for(string text)
        {
            var query = Driver.FindElement(By.Name("q"));
            query.SendKeys(text);
            query.Submit();
        }

        void The_page_title_should_be(string title)
        {
            (new WebDriverWait(Driver, new TimeSpan(0, 0, 10)))
                .Until(d => d.Title.Equals(title));
            Driver.Title.ShouldBe(title);
        }

        [FixtureTearDown]
        void TearDown()
        {
            Driver.Quit();
        }
    }
}
