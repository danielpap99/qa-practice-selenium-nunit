using System;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TestProject.Helpers
{
    public class ActionHelpers : IActionHelpers
    {
        private IWebDriver _driver;
        private readonly Actions _actions;


        public ActionHelpers(IWebDriver driver)
        {
            _driver = driver;
            _actions = new Actions(driver);
        }

        public void MoveMouseToElement(IWebElement element)
        {
            _actions.MoveToElement(element).Perform();
        }

        public void HoverOverElement(IWebElement element)
        {
            _actions.MoveToElement(element).Pause(TimeSpan.FromSeconds(3)).Perform();
        }

        public void DragAndDropElement(IWebElement drag, IWebElement drop)
        {
            _actions.DragAndDrop(drag, drop).Perform();
        }

        public void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor javaExecutor = (IJavaScriptExecutor)_driver;
            javaExecutor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

    }
}
