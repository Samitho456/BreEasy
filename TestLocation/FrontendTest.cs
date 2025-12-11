using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLocation
{
    [TestClass]
    public class FrontendTest
    {
        private static readonly string WebdriverPath = "C:\\Users\\rasmu\\OneDrive\\Dokumenter\\Datamatiker\\Eksamener\\chromedriver-win64";

        private static IWebDriver _driver;
        private static WebDriverWait _wait;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(WebdriverPath);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        [ClassCleanup]
        public static void Teardown()
        {
            _driver.Dispose();
        }

        /// <summary>
        /// This method tests the frontend of the application.
        /// </summary>
        [TestMethod]
        public void Testmethod1()
        {
            // Navigate to the web application
            _driver.Navigate().GoToUrl("http://okronborg.dk/");

            // Verify the page title
            Assert.AreEqual("Vindue åbning", _driver.Title);

            // Verify that the logo is displayed
            var logo = _driver.FindElement(By.ClassName("logo"));
            Assert.IsTrue(logo.Displayed);
            _driver.FindElement(By.ClassName("logo")).Click();

            // Verify that the location name is correct
            _wait.Until(d => d.FindElements(By.ClassName("nav-button")).Count > 0);
            var roomButtons = _driver.FindElements(By.ClassName("nav-button"));
            Assert.IsTrue(roomButtons.Count > 0);

            // Click on the first room button after waiting for the room cards to load
            _wait.Until(d => d.FindElements(By.ClassName("window-card")).Count > 0);
            var roomCards = _driver.FindElements(By.ClassName("window-card"));
            Assert.IsTrue(roomCards.Count > 1);

            // Click on the first room card after waiting for the green buttons to load
            _wait.Until(d => d.FindElements(By.ClassName("green-button")).Count > 0);
            _driver.FindElements(By.ClassName("green-button"))[1].Click();

            // Verify that the window cards are displayed and that you can click the button
            Assert.IsTrue(_driver.FindElements(By.ClassName("card-title")).Count > 0);
            _driver.FindElements(By.ClassName("toggle-open-status"))[0].Click();
        }
    }
}
