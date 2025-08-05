using OpenQA.Selenium;

public interface IActionHelpers
{
    void DragAndDropElement(IWebElement drag, IWebElement drop);
    void HoverOverElement(IWebElement element);
    void MoveMouseToElement(IWebElement element);
    void ScrollToElement(IWebElement element);
}
