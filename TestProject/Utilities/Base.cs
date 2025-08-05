using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Edge;
using TestProject.Helpers;
using Microsoft.Extensions.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Model;


namespace TestProject.Utilities
{
    public class Base
    {
        public IWebDriver driver = null!; // non-parallel driver. the 'null!' silences the possible null warning
        public DriverWait driverWait;
        public ActionHelpers actionHelpers;
        string? browserName;
        ExtentReports? extent;
        ExtentTest test;

        // command to get to right repos: dotnet cd TestProject
        // command to run filtered tests, eg. only Smoke tests:
        // dotnet test TestProject.csproj -- filter TestCategory=Smoke
        // command to use specific parameters e.g. browser:
        // Remove-Item Env:BROWSER; $env:BROWSER="Edge"; dotnet test TestProject.csproj --filter TestCategory=Smoke



        // HTML report file setup
        [OneTimeSetUp]
        public void Setup()
        {
            extent = new ExtentReports();

            string workingDirectory = Environment.CurrentDirectory;
            var directoryInfo = Directory.GetParent(workingDirectory);

            if (directoryInfo?.Parent?.Parent == null)
            {
                throw new Exception("Cannot find project directory from current working directory.");
            }

            string projectDirectory = directoryInfo.Parent.Parent.FullName;
            string reportPath = projectDirectory + "//index.html";

            var htmlReporter = new ExtentSparkReporter(reportPath);

            extent!.AttachReporter(htmlReporter);
            extent.AddSystemInfo("User", "DP");
            extent.AddSystemInfo("Environment", "Regression");
        }

        [SetUp]
        public void GlobalSetUp()
        {
            test = extent!.CreateTest(TestContext.CurrentContext.Test.Name); //html report

            //var config = new ConfigurationBuilder()
            //.AddJsonFile("Utilities/appsettings.json")
            //.Build();

            //string browserName = GetRandomBrowser();
            //string browserName = config["browser"];
            string browser = GetBrowser();
            
            switch (browser)
            {
                case "Chrome":

                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case "Edge":

                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/#top";
            driverWait = new DriverWait(driver);
            actionHelpers = new ActionHelpers(driver);
        }

        public string GetBrowser()
        {
            browserName = Environment.GetEnvironmentVariable("BROWSER");

            var config = new ConfigurationBuilder()
                .AddJsonFile("Utilities/appsettings.json")
                .Build();

            if (browserName == "Random")
            {
                Random random = new Random();

                string[] browsers = { "Chrome", "Edge" };

                int index = random.Next(browsers.Length); // Generate random index (0, or 1)

                return browsers[index]; // Return the browser at the random index
            }

            if (browserName == null)
            {
                string? browserName = config["browser"]; // the ? tells the compiler that I'm aware it can potentially be null

                if (browserName!.ToLower() == "random" || browserName == "")
                {
                    Random random = new Random();

                    string[] browsers = { "Chrome", "Edge"};

                    int index = random.Next(browsers.Length); // Generate random index (0, or 1)

                    return browsers[index]; // Return the browser at the random index
                }

                else
                {
                    return config["browser"]!; // the ! is a null-forgiving operator, "trust me it's not null, I know for certain"
                }
            }

            else
            {
                return browserName;
            }
        }

        public static JsonReader getDataParser()
        {
            return new JsonReader();
        }

        public Media CaptureScreenshot(IWebDriver driver, string screenshotName)
        {
            ITakesScreenshot capture = (ITakesScreenshot)driver;

            var screenshot = capture.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenshotName).Build();
        }

        [TearDown]
        public void CloseBrowser()
        {
            DateTime time = DateTime.Now;
            string fileName = "Screenshot_" + time.ToString("H:mm:ss") + ".png";

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test failed", CaptureScreenshot(driver, fileName));
                test.Log(Status.Fail, "Test failed with stacktrace " + stackTrace);
            }

            extent!.Flush();
            driver.Quit();
            driver.Dispose();
        }
    }
}
