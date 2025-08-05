using OpenQA.Selenium;

public interface IDriverWait
{
    bool WaitUntilElementIsNotVisible(string cssSelector);
    bool WaitUntilElementIsNotVisibleUsingID(string id);
    IWebElement WaitUntilElementIsVisible(string cssselector);
    IWebElement WaitUntilElementIsVisibleUsingID(string id);
    IWebElement WaitUntilElementIsVisibleUsingLinkText(string linkText);
    IWebElement WaitUntilElementTextContains(IWebElement element, string expectedText);
}
