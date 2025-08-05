using FluentAssertions;
using TestProject.Utilities;

namespace TestProject.Tests.RahulAcademy
{
    [TestFixture]
    public class LoginTests : Base
    {
        private LoginPage loginPage;

        [SetUp]
        public void PageObjects()
        {
            loginPage = new LoginPage(driver);
        }

        [Test]
        [Category("Smoke")]
        public void EnterEmail()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
            loginPage.GetCodeForUser("danielpap99@gmail.com");
            Thread.Sleep(1000);
            driverWait.WaitUntilElementIsVisible("[data-testid='btn-code']");
            loginPage.IsVerifyButtonDisplayed().Should().BeTrue();
        }

        [Test]
        [Category("Smoke")]
        public void InvalidCodeEntered_MessageDisplays()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
            loginPage.GetCodeForUser("danielpap99@gmail.com");
            loginPage.EnterVerificationCode("0", "0", "0", "0", "0", "0");
            loginPage.ClickVerifyButton();
            loginPage.WaitForInvalidCodeMessageToAppear();
            loginPage.LoginComponentText.Should().Contain("Invalid code. Please try again.");
        }

        [Test]
        [Category("Smoke")]
        public void UntickRememberMeCheckbox()
        {
            driver.Navigate().GoToUrl("https://sso.teachable.com/secure/9521/identity/login/otp");
            loginPage.EnterEmail("danielpap99@gmail.com");
            Thread.Sleep(1000);
            loginPage.UntickRememberMeButton();
            Thread.Sleep(1000);
        }
    }
}
