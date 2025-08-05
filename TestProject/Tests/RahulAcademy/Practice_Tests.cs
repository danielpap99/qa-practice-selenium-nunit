using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using TestProject.Utilities;

namespace TestProject.Tests.RahulAcademy
{
    [TestFixture]
    public class Practice_Tests : Base
    {
        private PracticePage practicePage;

        [SetUp]
        public void PageObjects()
        {
            practicePage = new PracticePage(driver);
        }

        [Test]
        public void SelectRadioButton()
        {
            IList<IWebElement> radioButtons = driver.FindElements(By.CssSelector("input[type='radio']"));

            foreach (IWebElement button in radioButtons)
            {
                var value = button.GetAttribute("value");

                if (value != null && value.Equals("radio3"))
                {
                    button.Click();
                    Thread.Sleep(3000);
                }
            }
        }

        [Test]
        public void JavaAlertOpens_ContainsName()
        {
            string name = "Bobby Robertson";

            driverWait.WaitUntilElementIsVisibleUsingID("name");

            practicePage.InputName(name);
            practicePage.ClickConfirmName();
            
            practicePage.JavaAlertText.Should().BeEquivalentTo($"Hello {name}, Are you sure you want to confirm?");

            practicePage.AcceptJavaAlert();
            
        }

        [Test]
        public void AutoSuggestiveDropdown_IndiaIsSelected()
        {
            practicePage.EnterFirstThreeCharactersOfCountry_AutoSuggestive("ind");

            IList<IWebElement> suggestions = driver.FindElements(By.CssSelector(".ui-menu-item div"));

            foreach (IWebElement suggestion in suggestions)
            {
                if (suggestion.Text.Equals("India"))
                {
                    suggestion.Click();
                }
            }

            practicePage.CountryFieldText.Should().BeEquivalentTo("India");
        }

        [Test]
        public void VeryLongMethodToAddProductsToCart_UsingText()
        {
            driver.Url = "https://www.automationexercise.com/brand_products/Polo";
            Thread.Sleep(1000);

            IList<IWebElement> displayedProducts = driver.FindElements(By.CssSelector("div.col-sm-4"));

            foreach (IWebElement product in displayedProducts)
            {
                if (product.Text.Contains("Blue Top") || product.Text.Contains("Fancy Green Top"))
                {
                    actionHelpers.HoverOverElement(product);
                    IWebElement addToCart = product.FindElement(By.CssSelector("[class*='add-to-cart']")); //partial class name
                    actionHelpers.ScrollToElement(addToCart);
                    addToCart.Click();
                    practicePage.ClickContinueShoppingButton();
                }
            }

            practicePage.ClickShoppingCart();

            using (new AssertionScope())
            {
                practicePage.CartProductCount.Should().BeEquivalentTo("2");
                practicePage.CartContentText.Should().Contain("Blue Top", "Fancy Green Top");
            }
        }

        [Test]
        public void HowToHandleFrames()
        {
            practicePage.ScrollToRahulFrame();

            // id, name, or index of frame can be used
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();
            Thread.Sleep(2000);

            driver.SwitchTo().DefaultContent(); //switch back to web page from frame
        }

        [Test]
        public void ScrollToAnElementUsingJavaExecutor()
        {
            practicePage.ScrollToMouseHoverButton();
            practicePage.HoverOnMouseHoverButton();
        }

        [Test]
        public void HandleNewTabsOrWindows()
        {
            string parentWindow = driver.CurrentWindowHandle; //to go back to this window later.
                                                              // WindowHandles[0] also works

            practicePage.OpenInterviewLinkInNewTab();
            Thread.Sleep(3000);
            driver.WindowHandles.Count.Should().Be(2);

            string childWindow1 = driver.WindowHandles[1];
            driver.SwitchTo().Window(childWindow1);

            string pageTitle = driver.FindElement(By.ClassName("page-title")).Text;

            //get the email from the text like this:
            string pageText =   driver.FindElement(By.CssSelector(".red")).Text;
            string[] splitPageText = pageText.Split("at");
            string[] truncatedText = splitPageText[1].Trim().Split(" ");
            string emailAddress = truncatedText[0];

            using (new AssertionScope())
            {
                emailAddress.Should().BeEquivalentTo("mentor@rahulshettyacademy.com");
                pageTitle.Should().BeEquivalentTo("DOCUMENTS REQUEST");
            }

            driver.SwitchTo().Window(parentWindow); // switch back to parent window or tab to continue
            driver.FindElement(By.Id("autocomplete")).SendKeys(emailAddress);

            Thread.Sleep(3000);
        }


        #region private methods


        #endregion
    }
}
