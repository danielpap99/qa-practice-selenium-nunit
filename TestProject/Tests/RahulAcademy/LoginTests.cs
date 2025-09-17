using FluentAssertions;
using OpenQA.Selenium;
using TestProject.Utilities;

namespace TestProject.Tests.RahulAcademy
{
    [TestFixture]
    public class LoginTests : Base
    {
        private LoginPage _loginPage;
        private PracticePage _practicePage;

        [SetUp]
        public void PageObjects()
        {
            _loginPage = new LoginPage(driver);
            _practicePage = new PracticePage(driver);
        }

        [Test]
        [Category("Stage 1")]
        [TestCaseSource(("ValidLoginDataFromJson"))]
        public void SuccessfulLogin(string username, string password)
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

            _loginPage.SuccessfulLogin(username, password);

            _practicePage.CheckoutVisible.Should().BeTrue();
        }

        [Test]
        [Category("Stage 1")]
        [TestCaseSource(("InvalidLoginDataFromJson"))]
        public void UnsuccessfulLogin(string username, string password)
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/loginpagePractise/");

            _loginPage.UnsuccessfulLogin(username, password);

            _loginPage.LoginFormText.Should().Contain("Incorrect username/password.");
        }

        [Test]
        [Category("Stage 2")]
        public void EnterEmail()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
            _loginPage.GetCodeForUser("danielpap99@gmail.com");
            Thread.Sleep(1000);
            driverWait.WaitUntilElementIsVisible("[data-testid='btn-code']");
            _loginPage.IsVerifyButtonDisplayed().Should().BeTrue();
        }

        [Test]
        [Category("Stage 1")]
        public void InvalidCodeEntered_MessageDisplays()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
            _loginPage.GetCodeForUser("danielpap99@gmail.com");
            _loginPage.EnterVerificationCode("0", "0", "0", "0", "0", "0");
            _loginPage.ClickVerifyButton();
            _loginPage.WaitForInvalidCodeMessageToAppear();
            _loginPage.LoginComponentText.Should().Contain("Invalid code. Please try again.");
        }

        [Test]
        [Category("Stage 2")]
        public void UntickRememberMeCheckbox()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
            _loginPage.EnterEmail("danielpap99@gmail.com");
            Thread.Sleep(1000);
            _loginPage.UntickRememberMeButton();
            Thread.Sleep(1000);
        }


        #region Private methods
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
