using System;
using System.Collections.ObjectModel;
using System.Linq;
using FluentSpec;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace Raconteur.Web
{
    public enum BrowserType
    {
        Chrome,  
        Firefox,  
    }

    public class Browser
    {
        public IWebDriver Driver;

        public void Use(string browser)
        {
            Driver = NewBrowser(browser);
        }

        public void End()
        {
            Driver.Quit();
        }

        public void Visit(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Set__to(string name, string value)
        {
            var query = Driver.FindElement(By.Name(name));
            query.SendKeys(value);
            query.Submit(); 
        }

        public void Click_on__with_text(string xpath, string text)
        {
            FindElementWithText(xpath, text).Click();
        }

        public void Title_should_be(string title)
        {
            (new WebDriverWait(Driver, new TimeSpan(0, 0, 10)))
                .Until(d => d.Title.Equals(title));
            Driver.Title.ShouldBe(title);
        }

        public void WaitForPageToLoad()
        {
            (new WebDriverWait(Driver, new TimeSpan(0, 0, 10)))
                .Until(d => !string.IsNullOrEmpty(d.Title));
        }

        public void Find_link_with(string text)
        {
            var link = FindElementWithText("//a", text);
            link.ShouldNotBeNull();
        }

        IWebElement FindElementWithText(string xpath, string text)
        {
            var query = FindElementsByXPath(xpath);
            return query.First(e => e.Text == text);
        }

        public ReadOnlyCollection<IWebElement> FindElementsByXPath(string xpath)
        {
            return Driver.FindElements(By.XPath(xpath));
        }

        IWebDriver NewBrowser(string browser)
        {
            switch ((BrowserType) Enum.Parse(typeof (BrowserType), browser, true))
            {
                case BrowserType.Chrome:
                    return new ChromeDriver();
                case BrowserType.Firefox:
                    return new FirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException("browser", browser, "not supported");
            }
        }
    }
}
