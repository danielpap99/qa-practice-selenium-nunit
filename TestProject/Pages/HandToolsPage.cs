using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class HandToolsPage : IHandToolsPage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;

    // Constructor: Assigns WebDriver instance
    public HandToolsPage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
    }

    #region Elements
    private IWebElement Pliers => _driver.FindElement(By.CssSelector("[data-test='product-01JVVXV7EQF19P5WVC0SGJ5E96']"));
    private IWebElement AddToCartButton => _driver.FindElement(By.CssSelector("[data-test='add-to-cart']"));
    private IWebElement ProductName => _driver.FindElement(By.CssSelector("[data-test='product-name']"));
    private IWebElement UnitPrice => _driver.FindElement(By.CssSelector("[data-test='unit-price']"));
    private IWebElement AddedToCartAlert => _driver.FindElement(By.Id("toast-container"));
    private IWebElement CartQuantity => _driver.FindElement(By.CssSelector("[data-test='cart-quantity']"));

    #endregion


    #region Methods

    public void ClickOnPliers()
    {
        Pliers.Click();
        driverWait.WaitUntilElementIsVisible("[data-test='add-to-cart']");
    }

    public void AddProductToCart()
    {
        AddToCartButton.Click();
        driverWait.WaitUntilElementIsVisibleUsingID("toast-container");
    }


    #endregion

    #region Asserts
    public string ProductNameText => ProductName.Text;
    public string UnitPriceText => UnitPrice.Text;
    public string AlertText => AddedToCartAlert.Text;
    public string CartQuantityText => CartQuantity.Text;
    public bool AlertDisplayed => AddedToCartAlert.Displayed;

    #endregion
}
