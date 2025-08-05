using System.Collections;
using NUnit.Framework;
using FluentAssertions;
using FluentAssertions.Execution;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestProject.Helpers;
using WebDriverManager.DriverConfigs.Impl;
using TestProject.Utilities;

namespace TestProject.Tests.RahulAcademy
{
    [TestFixture]
    public class GreenKart_Table : Base
    {
        private GreenKartPage greenKartPage;

        [SetUp]
        public void PageObjects()
        {
            greenKartPage = new GreenKartPage(driver);
        }

        [Test]
        [Category("Stage1")]
        public void SortProduceAlphabetically()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/offers");
            Select20Results_StaticDropdown();

            //Step 1: Grab array of produce and sort 
            IList<IWebElement> ProduceColumn = driver.FindElements(By.XPath("//tr/td[1]"));
            ArrayList a = new ArrayList();

            foreach (IWebElement item in ProduceColumn)
            {
                a.Add(item.Text);
            }

            a.Sort();

            //Step 2: Sort the produce on the web
            greenKartPage.ClickSortVegFruit();

            //Step 3: Grab array of sorted produce
            IList<IWebElement> ProduceColumnDesc = driver.FindElements(By.XPath("//tr/td[1]"));
            ArrayList b = new ArrayList();

            foreach (IWebElement item in ProduceColumnDesc)
            {
                b.Add(item.Text);
            }

            //Step 4: Compare array a and array b; they should be equal
            Assert.That(a, Is.EquivalentTo(b)); // new Nunit assert style
            a.Should().BeEquivalentTo(b); // fluent assertions
        }

        #region
        private void Select20Results_StaticDropdown()
        {
            greenKartPage.SelectPageSize_StaticDropdown("20");
            Thread.Sleep(1500);
        }

        #endregion
    }
}