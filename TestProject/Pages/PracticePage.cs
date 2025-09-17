using System.Collections;
using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using TestProject.Helpers;

public class PracticePage : IPracticePage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;
    private ActionHelpers actionHelpers;
    public CheckoutPage checkoutPage;

    // Constructor: Assigns WebDriver instance
    public PracticePage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
        actionHelpers = new ActionHelpers(driver);
        checkoutPage = new CheckoutPage(driver);
        PageFactory.InitElements(driver, this); //Page Object Factory, outdated and not really used anymore
    }

    #region Elements
    private IWebElement NameField => _driver.FindElement(By.Id("name"));
    private IWebElement ConfirmButton => _driver.FindElement(By.Id("confirmbtn"));
    private IWebElement AutoSuggestiveCountryDropdown => _driver.FindElement(By.Id("autocomplete"));
    private IWebElement MouseHoverButton => _driver.FindElement(By.Id("mousehover"));
    private IWebElement RahulFrame => _driver.FindElement(By.Id("courses-iframe"));
    private IWebElement NewWindowButton => _driver.FindElement(By.Id("opentab"));
    private IWebElement InterviewLink => _driver.FindElement(By.ClassName("blinkingText"));
    private IWebElement PhotoSlideComponent => _driver.FindElement(By.Id("slider-part"));
    private IWebElement ContinueShoppingButton => _driver.FindElement(By.CssSelector("[data-dismiss='modal']"));
    private IWebElement ShoppingCart => _driver.FindElement(By.CssSelector("[class*='fa-shopping-cart']"));
    private IWebElement CartTable => _driver.FindElement(By.Id("cart_info_table"));
    private IWebElement CheckoutButton => _driver.FindElement(By.CssSelector(".btn-success"));
    private IWebElement Checkout => _driver.FindElement(By.PartialLinkText("Checkout"));
    private IWebElement SuccessAlert => _driver.FindElement(By.CssSelector("[class*='alert-success']"));
    #endregion

    #region Page Object Factory

    //[FindsBy(How = How.Id, Using = "name")]
    //private IWebElement? nameInputField;

    #endregion


    #region Methods

    public void InputName(string name)
    {
        NameField.SendKeys(name);
        //nameInputField!.SendKeys(name);
    }

    public void ClickCheckout()
    {
        Checkout.Click();
    }

    public void ClickConfirmName()
    {
        ConfirmButton.Click();
        Thread.Sleep(1000);
    }

    public void ClickContinueShoppingButton()
    {
        driverWait.WaitUntilElementIsVisible("[data-dismiss='modal']");
        ContinueShoppingButton.Click();
    }

    public void ClickShoppingCart()
    {
        actionHelpers.ScrollToElement(ShoppingCart);
        ShoppingCart.Click();
    }

    public void AcceptJavaAlert()
    {
        _driver.SwitchTo().Alert().Accept();
    }

    public void CancelJavaAlert()
    {
        _driver.SwitchTo().Alert().Dismiss();
    }

    public void EnterFirstThreeCharactersOfCountry_AutoSuggestive(string country)
    {
        AutoSuggestiveCountryDropdown.SendKeys(country);
        Thread.Sleep(1000);
    }

    public void ScrollToRahulFrame()
    {
        actionHelpers.ScrollToElement(RahulFrame);
        Thread.Sleep(1000);
    }

    public void ScrollToMouseHoverButton()
    {
        actionHelpers.ScrollToElement(MouseHoverButton);
        Thread.Sleep(1000);
    }

    public void HoverOnMouseHoverButton()
    {
        actionHelpers.HoverOverElement(MouseHoverButton);
    }

    public void ClickNewTabButton()
    {
        NewWindowButton.Click();
    }

    public void ClickCheckoutButton()
    {
        CheckoutButton.Click();
    }

    public void OpenInterviewLinkInNewTab() // stimulate CTRL + click
    {
        Actions actions = new Actions(_driver);
        actions.KeyDown(Keys.Control)
               .Click(InterviewLink)
               .KeyUp(Keys.Control)
               .Build()
               .Perform();
    }

    #endregion

    #region Asserts

    public string JavaAlertText => _driver.SwitchTo().Alert().Text!;
    public string CartProductCount => CartTable.GetAttribute("childElementCount")!;
    public string CartContentText => CartTable.Text;
    public string SuccessFulPaymentText => SuccessAlert.Text;

    #endregion

    #region Asserts

    public string CountryFieldText => AutoSuggestiveCountryDropdown.GetAttribute("value")!;
    public bool PhotoSlideComponentVisible => PhotoSlideComponent.Displayed;
    public bool CheckoutVisible => Checkout.Displayed;

    #endregion
}