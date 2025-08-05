using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class ContactPage : IContactPage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;

    // Constructor: Assigns WebDriver instance
    public ContactPage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
    }

    #region Elements
    private IWebElement FirstName => _driver.FindElement(By.CssSelector("[data-test='first-name']"));
    private IWebElement SubjectDropdown => _driver.FindElement(By.CssSelector("[data-test='subject']"));
    private IWebElement FirstNameField => _driver.FindElement(By.CssSelector("[data-test='first-name']"));
    private IWebElement LastNameField => _driver.FindElement(By.CssSelector("[data-test='last-name']"));
    private IWebElement EmailField => _driver.FindElement(By.CssSelector("[data-test='email']"));
    private IWebElement MessageField => _driver.FindElement(By.CssSelector("[data-test='message']"));
    private IWebElement SendButton => _driver.FindElement(By.CssSelector("[data-test='contact-submit']"));
    private IWebElement SuccessfulAlert => _driver.FindElement(By.CssSelector(".alert.alert-success.mt-3"));
    private IWebElement MinimumCharacterAlert => _driver.FindElement(By.CssSelector("[data-test='message-error']"));


    #endregion


    #region Methods

    public void SelectSubject(string subject)
    {
        SelectElement dropdown = new SelectElement(SubjectDropdown);
        dropdown.SelectByText(subject);
    }

    public void InputFirstName(string firstName)
    {
        FirstNameField.SendKeys(firstName);
    }

    public void InputLastName(string lastName)
    {
        LastNameField.SendKeys(lastName);
    }

    public void InputEmail(string email)
    {
        EmailField.SendKeys(email);
    }

    public void InputMessage(string message)
    {
        MessageField.SendKeys(message);
    }

    public void InputMessageOver50Characters()
    {
        MessageField.Clear();
        MessageField.SendKeys("Hello");

        for (int i = 0; i < 45; i++)
        {
            MessageField.SendKeys("o");
        }
    }

    public void ClickSend()
    {
        SendButton.Click();
    }

    public void WaitForSuccessfulAlertToAppear()
    {
        driverWait.WaitUntilElementIsVisible(".alert.alert-success.mt-3");
    }

    #endregion

    #region Asserts

    public string SuccessfulAlertText => SuccessfulAlert.Text;
    public string MinimumCharacterAlertText => MinimumCharacterAlert.Text;

    //FindElement will throw NoSuchElementException if element is not on page
    //try catch method to return false when exception is thrown
    public bool IsMinimumCharacterAlertVisible
    {
        get
        {
            try
            {
                return MinimumCharacterAlert.Displayed;
            }

            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }

    //FindElements makes a list of matching elements and returns the first one
    //FirstOrDefault returns NULL if no elements are in the list
    //?. accesses Displayed if element is not null
    //if it is NULL (no element is found), returns false as a default
    public bool IsCharacterAlertVisible()
    {
        var MinimumCharacterAlert = _driver.FindElements(By.CssSelector("[data-test='message-error']")).FirstOrDefault();

        return MinimumCharacterAlert?.Displayed ?? false;
    }



    #endregion
}
