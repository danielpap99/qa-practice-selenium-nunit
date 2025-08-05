using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class CheckoutPage : ICheckoutPage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;

    // Constructor: Assigns WebDriver instance
    public CheckoutPage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
    }

    #region Elements
    private IWebElement CountryField => _driver.FindElement(By.Id("country"));
    private IWebElement TermsConditionsCheckbox => _driver.FindElement(By.CssSelector("label[for*='checkbox2']"));
    private IWebElement PurchaseButton => _driver.FindElement(By.CssSelector("[value='Purchase']"));

    #endregion


    #region Methods

    public void EnterCountry(string country)
    {
        CountryField.SendKeys(country);
    }

    public void ClickOnCountrySuggestion(string country)
    {
        driverWait.WaitUntilElementIsVisibleUsingLinkText(country);
        _driver.FindElement(By.LinkText(country)).Click();
    }

    public void TickTermsAndConditionsCheckbox()
    {
        TermsConditionsCheckbox.Click();
    }

    public void ClickPurchaseButton()
    {
        PurchaseButton.Click();
    }


    #endregion

    #region Asserts


    #endregion
}
