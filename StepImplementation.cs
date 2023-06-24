using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Gauge.CSharp.Lib;
using Gauge.CSharp.Lib.Attribute;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace netcore.template
{
    public class StepImplementation
    {
        private HashSet<char> _vowels;
        // Holds the WebDriver instance
        private static IWebDriver _driver;

        [Step("Playwright Page has title <titleString>.")]
        public void VerifyPlaywrightPage(string titleString)
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://playwright.dev");
            IWebElement title = _driver.FindElement(By.ClassName("highlight_gXVj"));
            Console.WriteLine("Title: " + title.Text);
            title.Text.Should().Contain(titleString);

            IWebElement lang = _driver.FindElement(By.XPath("//*[@class='navbar__link']"));
            lang.Click();
            IWebElement lang_python = _driver.FindElement(By.XPath("//*[@data-language-prefix='/python/']"));
            lang_python.Click();
            IWebElement setLang = _driver.FindElement(By.XPath("//*[@class='navbar__title text--truncate']"));
            setLang.Text.Should().Contain("Playwright for Python");
            
            _driver.Close();
        }

        [Step("Google Page has title <titleString>.")]
        public void VerifyGooglePage(string titleString)
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.google.com");
            
            IWebElement signInButton = _driver.FindElement(By.ClassName("gb_Md"));
            signInButton.Click();

            IWebElement title = _driver.FindElement(By.ClassName("oO8pQe"));
            Console.WriteLine("Title: " + title.Text);
            title.Text.Should().Contain(titleString);

            IWebElement emailText = _driver.FindElement(By.XPath("//*[@id='identifierId']"));
            emailText.SendKeys("demo");
            IWebElement nextButton = _driver.FindElement(By.XPath("//*[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']"));
            nextButton.Click();

            //Thread.Sleep(3000);
            WaitForElement(By.XPath("//*[@class='o6cuMc Jj6Lae']"));

            IWebElement errorMsg = _driver.FindElement(By.XPath("//*[@class='o6cuMc Jj6Lae']"));
            Console.WriteLine("Title: " + errorMsg.Text);
            errorMsg.Text.Should().Contain("Couldn’t find your Google Account");

            _driver.Close();
        }

        private static void WaitForElement(By by)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(x => x.FindElement(by));
        }
    }
}
