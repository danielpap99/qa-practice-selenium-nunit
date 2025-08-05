using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject.Helpers;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject.Tests.ToolShop
{
    [Parallelizable(ParallelScope.Self)] // test files running in parallel
    //[Parallelizable(ParallelScope.All)] // tests running in parallel within this class
    [TestFixture]
    public class ContactForm
    {
        //private IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>(); //parallel driver

        //private HomePage homePage;
        //private ContactPage contactPage;

        [SetUp]
        public void StartChromeBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver.Value = new ChromeDriver();
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://practicesoftwaretesting.com/";
            //homePage = new HomePage(driver.Value);
            //contactPage = new ContactPage(driver.Value);
        }

        [Test]
        public void SubmitAnEnquiry()
        {
            var homePage = new HomePage(driver.Value!); // re-create page objects locally if tests are running in parallel
            var contactPage = new ContactPage(driver.Value!);

            homePage.NavigateToContactPage();

            EnterContactDetails("Brian", "Smith", "briansmith@test.com");
            contactPage.SelectSubject("Payments");
            contactPage.InputMessageOver50Characters();

            contactPage.ClickSend();
            contactPage.WaitForSuccessfulAlertToAppear();

            contactPage.SuccessfulAlertText.Should().BeEquivalentTo("Thanks for your message! We will contact you shortly.");
            
            Thread.Sleep(2000);
        }

        [Test]
        public void MinimumCharacterCountNotReached_AlertIsDisplayed()
        {
            var homePage = new HomePage(driver.Value!);
            var contactPage = new ContactPage(driver.Value!);

            homePage.NavigateToContactPage();

            EnterContactDetails("Brian", "Smith", "briansmith@test.com");
            contactPage.SelectSubject("Payments");
            contactPage.InputMessage("Hello");
            contactPage.ClickSend();

            contactPage.IsMinimumCharacterAlertVisible.Should().BeTrue();
            contactPage.MinimumCharacterAlertText.Should().BeEquivalentTo("Message must be minimal 50 characters");
            
            contactPage.InputMessageOver50Characters();

            using (new AssertionScope())
            {
                contactPage.IsMinimumCharacterAlertVisible.Should().BeFalse(); // try catch method
                contactPage.IsCharacterAlertVisible().Should().BeFalse(); // ?? method
            }

            Thread.Sleep(2000);
        }

        #region private methods

        private void EnterContactDetails(string firstName, string lastName, string email)
        {
            var contactPage = new ContactPage(driver.Value!);

            contactPage.InputFirstName(firstName);
            contactPage.InputLastName(lastName);
            contactPage.InputEmail(email);
        }

        #endregion

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
