using System.Collections;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class GreenKartPage : IGreenKartPage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;

    // Constructor: Assigns WebDriver instance
    public GreenKartPage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
    }

    #region Elements
    private IWebElement PageSizeDropdown => _driver.FindElement(By.Id("page-menu"));
    private IWebElement CartQuantity => _driver.FindElement(By.CssSelector("[data-test='cart-quantity']"));
    private IList<IWebElement> ProduceColumn => _driver.FindElements(By.XPath("//tr/td[1]"));
    private IWebElement SortVegFruit => _driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div/div/div/div/table/thead/tr/th[1]"));

    #endregion


    #region Methods

    public void SelectPageSize_StaticDropdown(string size)
    {
        SelectElement dropdown = new SelectElement(PageSizeDropdown);
        dropdown.SelectByValue(size);
    }

    public void ClickSortVegFruit()
    {
        SortVegFruit.Click();
    }

    public void GetArrayListOfProduce()
    {
        ArrayList a = new ArrayList();

        foreach (IWebElement item in ProduceColumn)
        {
            a.Add(item.Text);
        }
    }

    public void GetAndSortArrayListofProduce()
    {
        ArrayList a = new ArrayList();

        foreach (IWebElement item in ProduceColumn)
        {
            a.Add(item.Text);
        }

        a.Sort();
    }
}

    #endregion

#region Asserts


#endregion
