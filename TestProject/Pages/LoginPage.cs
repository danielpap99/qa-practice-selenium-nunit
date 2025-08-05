using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class LoginPage : ILoginPage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;

    // Constructor: Assigns WebDriver instance and called when you create a new LoginPage in a test class
    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
    }

    // Locators (Elements)
    private IWebElement EmailInput => _driver.FindElement(By.CssSelector("[data-testid='email-input']"));
    private IWebElement LoginButton => _driver.FindElement(By.CssSelector("[data-testid='btn-login']"));
    private IWebElement VerifyButton => _driver.FindElement(By.CssSelector("[data-testid='btn-code']"));
    private IWebElement LoginComponent => _driver.FindElement(By.Id("otp-login"));
    private IWebElement RememberMeCheckboxByXpath => _driver.FindElement(By.XPath("//html/body/main/div/div[1]/div/div[1]/label[2]/input[@type='checkbox']"));
    private IWebElement OtpButton(int number) => _driver.FindElement(By.CssSelector($"[data-testid='otp-input-{number}']"));
    private IWebElement InvalidCodeMessage => _driver.FindElement(By.CssSelector(".col-12 uni-mt-16 flex-align-items-center ur4-L tOteG OlBRA"));
    private IWebElement UsernameField => _driver.FindElement(By.Id("username"));
    private IWebElement PasswordField => _driver.FindElement(By.Name("password"));
    private IWebElement TAndCTickbox => _driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input"));
    private IWebElement SignInButton => _driver.FindElement(By.XPath("//input[@value='Sign In']"));

    #region Methods

    public void SuccessfulLogin(string username, string password)
    {
        UsernameField.SendKeys(username);
        PasswordField.SendKeys(password);
        TAndCTickbox.Click();
        SignInButton.Click();
    }

    public void EnterEmail(string email)
    {
        EmailInput.SendKeys(email);
    }

    public void GetCodeForUser(string email)
    {
        EnterEmail(email);
        LoginButton.Click();
    }

    public void EnterVerificationCode(string digit0, string digit1, string digit2, string digit3, string digit4, string digit5)
    {
        driverWait.WaitUntilElementIsVisible("[data-testid='btn-code']");
        OtpButton(0).SendKeys(digit0);
        OtpButton(1).SendKeys(digit1);
        OtpButton(2).SendKeys(digit2);
        OtpButton(3).SendKeys(digit3);
        OtpButton(4).SendKeys(digit4);
        OtpButton(5).SendKeys(digit5);
    }

    public void UntickRememberMeButton()
    {
        RememberMeCheckboxByXpath.Click();
    }

    public void ClickVerifyButton()
    {
        driverWait.WaitUntilElementIsVisible("[data-testid='btn-code']");
        VerifyButton.Click();
    }

    public void WaitForInvalidCodeMessageToAppear()
    {
        driverWait.WaitUntilElementTextContains(LoginComponent, "Invalid code. Please try again.");
    }

    #endregion

    #region Asserts
    public string InvalidCodeMessageText => InvalidCodeMessage.Text;
    public string LoginComponentText => LoginComponent.Text;

    public bool IsVerifyButtonDisplayed() => VerifyButton.Displayed;
    #endregion
}

