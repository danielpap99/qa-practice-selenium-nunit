
using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject.Helpers;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject.Tests.ToolShop
{
    //[Parallelizable(ParallelScope.All)] //tests run in parallel within this class
    [Parallelizable(ParallelScope.Self)] // test files running in parallel
    //when using parallel thread driver then driver.Value must be used every time instead of driver
    //also page objects have to be created and isolated locally instead of SetUp as drivers will affect each other

    [TestFixture]
    public class HandTools
    {
        //private static IWebDriver driver; //non-parallel driver
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>(); //parallel driver
        //private HomePage homePage;
        //private HandToolsPage handToolsPage;
        //private DriverWait driverWait;

        [SetUp]
        public void StartChromeBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Value = new ChromeDriver();
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://practicesoftwaretesting.com/";
            //homePage = new HomePage(driver.Value);
            //handToolsPage = new HandToolsPage(driver.Value);
            //driverWait = new DriverWait(driver.Value);
        }

        [Test]
        public void GoToHandToolsSection()
        {
            var homePage = new HomePage(driver.Value!);

            homePage.NavigateToHandTools();
            homePage.PageTitleText.Should().BeEquivalentTo("Category: Hand Tools");
        }

        [Test]
        public void ChangeLanguageToSpanish()
        {
            var homePage = new HomePage(driver.Value!);

            homePage.NavigateToHandTools();
            homePage.SetSpanishAsLanguage("es");

            using (new AssertionScope())
            {
                homePage.PageTitleText.Should().Contain("Categoría");
                homePage.SortText.Should().BeEquivalentTo("Ordenar");
            }
        }

        [Test]
        public void ChangeLanguageToGerman()
        {
            var homePage = new HomePage(driver.Value!);

            homePage.NavigateToHandTools();
            homePage.SetGermanAsLanguage("de");

            using (new AssertionScope())
            {
                homePage.PageTitleText.Should().Contain("Kategorie");
                homePage.SortText.Should().BeEquivalentTo("Sortieren");
            }
        }

        [Test]
        public void AddPliersToCart()
        {
            var homePage = new HomePage(driver.Value!);
            var handToolsPage = new HandToolsPage(driver.Value!);
            var driverWait = new DriverWait(driver.Value!);

            homePage.NavigateToHandTools();
            handToolsPage.ClickOnPliers();

            using (new AssertionScope())
            {
                handToolsPage.ProductNameText.Should().BeEquivalentTo("Pliers");
                handToolsPage.UnitPriceText.Should().BeEquivalentTo("12.01");
            }

            handToolsPage.AddProductToCart();

            using (new AssertionScope())
            {
                handToolsPage.AlertDisplayed.Should().BeTrue();
                handToolsPage.AlertText.Should().BeEquivalentTo("Product added to shopping cart.");
                driverWait.WaitUntilElementIsNotVisibleUsingID("toast-container");
                handToolsPage.CartQuantityText.Should().BeEquivalentTo("1");
            }
        }


        [TearDown]
        public void CloseBrowser()
        {
            driver.Value!.Quit();
        }

        [OneTimeTearDown]
        public void ParallelTeardown()
        {
            driver.Dispose();
        }
    }
}
