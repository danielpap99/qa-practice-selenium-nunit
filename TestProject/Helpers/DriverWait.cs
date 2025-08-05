using System;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject.Helpers
{
    public class DriverWait : IDriverWait
    {
        private IWebDriver _driver;


        public DriverWait(IWebDriver driver, int timeoutInSeconds = 10) // Default: 10 seconds
        {
            _driver = driver;
            //WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
        }

        // Custom method for waiting until the element is visible
        public IWebElement WaitUntilElementIsVisible(string cssselector)
        {
            WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            return _driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(cssselector)));
        }

        public IWebElement WaitUntilElementIsVisibleUsingLinkText(string linkText)
        {
            WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            return _driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
        }

        public IWebElement WaitUntilElementIsVisibleUsingID(string id)
        {
            WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            return _driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(id)));
        }

        public IWebElement WaitUntilElementTextContains(IWebElement element, string expectedText)
        {
            WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            _driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, expectedText));
            return element;
        }

        public bool WaitUntilElementIsNotVisible(string cssSelector)
        {
            WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return _driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(cssSelector)));
        }

        public bool WaitUntilElementIsNotVisibleUsingID(string id)
        {
            WebDriverWait _driverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            return _driverWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id(id)));
        }

    }
}
