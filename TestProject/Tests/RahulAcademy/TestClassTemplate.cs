using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject.Tests.RahulAcademy
{
    public class TestClassTemplate // for nUnit
    {
        private IWebDriver driver;

        public TestClassTemplate() // constructor, only needed in xUnit instead of SetUp method, where you would add page objects as well
        {
        }

        [SetUp] // sets up environment for driver. add any page objects here before declaring it at the top
        public void StartChromeBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void NavigateToRahulShettyAcademy()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
        }

        [TearDown] // method to close down the browser at the end of tests
        public void CloseBrowser()
        {
            driver.Quit();
            driver.Dispose();
        }

    }
}
