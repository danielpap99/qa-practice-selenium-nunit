using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class ToolsQAPage : IToolsQAPage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;
    private ActionHelpers actionHelpers;

    // Constructor: Assigns WebDriver instance
    public ToolsQAPage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
        actionHelpers = new ActionHelpers(driver);
    }

    #region Elements
    private IWebElement DraggableElement => _driver.FindElement(By.Id("draggable"));
    private IWebElement Dropbox => _driver.FindElement(By.Id("droppable"));
    private IWebElement CartQuantity => _driver.FindElement(By.CssSelector("[data-test='cart-quantity']"));
    private IList<IWebElement> ProduceColumn => _driver.FindElements(By.XPath("//tr/td[1]"));
    #endregion


    #region Methods

    public void DragAndDropElement()
    {
        actionHelpers.ScrollToElement(DraggableElement);
        actionHelpers.DragAndDropElement(DraggableElement, Dropbox);
        driverWait.WaitUntilElementTextContains(Dropbox, "Dropped!");
    }

    #endregion

    #region Asserts

    public string DropboxText => Dropbox.Text;

    #endregion

    #region Asserts



    #endregion

}