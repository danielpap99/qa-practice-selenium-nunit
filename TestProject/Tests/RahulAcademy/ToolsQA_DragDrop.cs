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
    public class ToolsQA_DragDrop : Base
    {
        private ToolsQAPage toolsQAPage;

        [SetUp]
        public void PageObjects()
        {
            toolsQAPage = new ToolsQAPage(driver);
        }

        [Test]
        [Category("Stage2")]
        public void DragAndDropElementSuccessfully_Simple()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/droppable");
            toolsQAPage.DragAndDropElement();
            toolsQAPage.DropboxText.Should().BeEquivalentTo("Dropped!");
        }
 


        #region private methods


        #endregion
    }
}

