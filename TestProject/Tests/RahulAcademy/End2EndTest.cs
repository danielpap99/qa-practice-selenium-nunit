﻿using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Utilities;

namespace TestProject.Tests.RahulAcademy
{
    [TestFixture]
    public class End2EndTest : Base
    {
        public PracticePage _practicePage;
        public LoginPage _loginPage;

        [SetUp]
        public void PageObjects()
        {
            _practicePage = new PracticePage(driver);
            _loginPage = new LoginPage(driver);
        }

        [Test]
        [TestCaseSource(("ValidLoginDataFromJson"))] // global test cases for username and password
        [Category("Regression")]
        [Category("Team 3")]
        public void EndToEndFlow(string username, string password) //uses arrays to make sure correct products are added to card

        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new string[2]; // empty string array

            _loginPage.SuccessfulLogin(username, password);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> products = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement product in products)
            {

                if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))

                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }

            }
            Thread.Sleep(1000);
            _practicePage.ClickCheckout();
            IList<IWebElement> checkoutCards = driver.FindElements(By.CssSelector("h4 a"));

            for (int i = 0; i < 2; i++)

            {
                actualProducts[i] = checkoutCards[i].Text; // add each product to the empty array then compare
            }

            Thread.Sleep(1000);
            expectedProducts.Should().Equal(actualProducts);

            _practicePage.ClickCheckoutButton();
            _practicePage.checkoutPage.EnterCountry("Ind");
            _practicePage.checkoutPage.ClickOnCountrySuggestion("India");
            _practicePage.checkoutPage.TickTermsAndConditionsCheckbox();
            _practicePage.checkoutPage.ClickPurchaseButton();

            _practicePage.SuccessFulPaymentText.Should().Contain("Success! Thank you!");
        }


        #region private methods

        public static IEnumerable<TestCaseData> LoginTestCases() //test cases hard-coded here
        {
            yield return new TestCaseData("rahulshettyacademy", "learning");
            yield return new TestCaseData("rahulshettyacademy123", "learning123");
        }

        public static IEnumerable<TestCaseData> ValidLoginDataFromJson() //test cases parsed from json file
        {
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"));
        }

        public static IEnumerable<TestCaseData> InvalidLoginDataFromJson() //test cases parsed from json file
        {
            yield return new TestCaseData(getDataParser().extractData("wrong_username"), getDataParser().extractData("wrong_password"));
        }

        #endregion

    }
}
