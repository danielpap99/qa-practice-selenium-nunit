using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestProject.Helpers;

public class HomePage : IHomePage
{
    private readonly IWebDriver _driver;
    private DriverWait driverWait;

    // Constructor: Assigns WebDriver instance
    public HomePage(IWebDriver driver)
    {
        _driver = driver;
        driverWait = new DriverWait(driver);
    }

    #region Elements
    private IWebElement CategoriesDropdown => _driver.FindElement(By.CssSelector("[data-test='nav-categories']"));
    private IWebElement HandToolsDropdownOption => _driver.FindElement(By.CssSelector("[data-test='nav-hand-tools']"));
    private IWebElement PageTitle => _driver.FindElement(By.CssSelector("[data-test='page-title']"));
    private IWebElement LanguageDropdown => _driver.FindElement(By.CssSelector("[data-test='language-select']"));
    private IWebElement Language(string language) => _driver.FindElement(By.CssSelector($"[data-test='lang-{language}']"));
    private IWebElement Sort => _driver.FindElement(By.XPath("//*[@id=\"filters\"]/h4[1]"));
    private IWebElement ContactButton => _driver.FindElement(By.CssSelector("[data-test='nav-contact']"));
    #endregion


    #region Methods

    public void SetSpanishAsLanguage(string language)
    {
        LanguageDropdown.Click();
        Language(language).Click();
        string languageCode = LanguageText.ToUpper();
        driverWait.WaitUntilElementTextContains(LanguageDropdown, languageCode);
        driverWait.WaitUntilElementTextContains(PageTitle, "Categoría");
    }


    public void SetGermanAsLanguage(string language)
    {
        LanguageDropdown.Click();
        Language(language).Click();
        string languageCode = LanguageText.ToUpper();
        driverWait.WaitUntilElementTextContains(LanguageDropdown, languageCode);
        driverWait.WaitUntilElementTextContains(PageTitle, "Kategorie");
    }

    public void NavigateToHandTools()
    { 
        CategoriesDropdown.Click();
        HandToolsDropdownOption.Click();
        driverWait.WaitUntilElementTextContains(PageTitle, "Hand Tools");
        driverWait.WaitUntilElementIsVisible("[data-test='product-name']");
    }

    public void NavigateToContactPage()
    {
        ContactButton.Click();
        driverWait.WaitUntilElementIsVisible("[data-test='first-name']");
    }

    #endregion

    #region Asserts

    public string PageTitleText => PageTitle.Text;
    public string LanguageText => LanguageDropdown.Text;
    public string SortText => Sort.Text;

    #endregion
}
