using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace XtramileTest
{
    [TestClass]
    public class ComputerDatabaseTest
    {
        public static IWebDriver driver = new ChromeDriver();
        public static string url = "https://computer-database.gatling.io/computers";
        public static string title = "Computers database";
        public static string btnAddNewComputer = "Add a new computer";
        public static string mandatoryMessage = "Failed to refine type : Predicate isEmpty() did not fail.";
        public static string wrongDate = "12345";
        public static string errorDateMessage = "Failed to decode date : java.time.format.DateTimeParseException: Text '"+wrongDate+"' could not be parsed at index 0";
        public static string dateValid1 = "1990-08-30";
        public static string dateValid2 = "2020-08-30";
        public static string wrongDiscontinuedDate = "Discontinued date is before introduction date";
        public static string messageSuccess = "Done ! Computer Apple 123 has been created";
        public static string existComputerName = "ASCI Thors Hammer";
        public static string displayPagination = "Displaying 1 to 1 of 1";


        [TestMethod]
        public void Test1_TestOpenUrl()
        {
            OpenBrowserUrl(url);
            Assert.AreEqual(title, driver.Title);
        }

        [TestMethod]
        public void Test2_TestAddNewComputer()
        {
            Assert.AreEqual(btnAddNewComputer, InteractiveElement.GetText(driver, "add", GlobalVariable.typeId));
            InteractiveElement.Click(driver, "add", GlobalVariable.typeId);
            Assert.AreEqual(driver.Url, url + "/new");

            //test form is exist
            Assert.AreEqual(true, InteractiveElement.IsExist(driver, "name", GlobalVariable.typeId));
            Assert.AreEqual(true, InteractiveElement.IsExist(driver, "introduced", GlobalVariable.typeId));
            Assert.AreEqual(true, InteractiveElement.IsExist(driver, "discontinued", GlobalVariable.typeId));
            Assert.AreEqual(true, InteractiveElement.IsExist(driver, "company", GlobalVariable.typeId));

            //test mandatory validation
            driver.FindElement(By.TagName("form")).Submit();
            Assert.AreEqual(mandatoryMessage, driver.FindElement(By.CssSelector(".help-inline")).Text);

            //test invalid date format
            InteractiveElement.EnterText(driver, "name","Apple 123", GlobalVariable.typeId);
            InteractiveElement.EnterText(driver, "introduced", wrongDate, GlobalVariable.typeId);
            InteractiveElement.EnterText(driver, "discontinued", wrongDate, GlobalVariable.typeId);
            driver.FindElement(By.TagName("form")).Submit();
            Assert.AreEqual(errorDateMessage, driver.FindElement(By.XPath("//*[@id='main']/form/fieldset/div[2]/div/span")).Text);
            Assert.AreEqual(errorDateMessage, driver.FindElement(By.XPath("//*[@id='main']/form/fieldset/div[3]/div/span")).Text);

            //test discontinued date not valid
            InteractiveElement.EnterText(driver, "name", "Apple 123", GlobalVariable.typeId);
            InteractiveElement.EnterText(driver, "introduced", dateValid1, GlobalVariable.typeId);
            InteractiveElement.EnterText(driver, "discontinued", dateValid1, GlobalVariable.typeId);
            driver.FindElement(By.TagName("form")).Submit();
            Assert.AreEqual(wrongDiscontinuedDate, driver.FindElement(By.XPath("//*[@id='main']/form/fieldset/div[3]/div/span")).Text);

            //Success create new computer
            InteractiveElement.EnterText(driver, "name", "Apple 123", GlobalVariable.typeId);
            InteractiveElement.EnterText(driver, "introduced", dateValid1, GlobalVariable.typeId);
            InteractiveElement.EnterText(driver, "discontinued", dateValid2, GlobalVariable.typeId);
            InteractiveElement.SelectDropdown(driver, "company", "Apple Inc.", GlobalVariable.typeId);
            driver.FindElement(By.TagName("form")).Submit();
            Assert.AreEqual(driver.Url, url);
            Assert.AreEqual(messageSuccess, driver.FindElement(By.XPath("//*[@id='main']/div[1]")).Text);
        }

        [TestMethod]
        public void Test3_TestDataGrid()
        {
            //test filter by name
            InteractiveElement.EnterText(driver, "searchbox", existComputerName, GlobalVariable.typeId);
            InteractiveElement.Click(driver, "searchsubmit", GlobalVariable.typeId);
            Assert.AreEqual(existComputerName, driver.FindElement(By.XPath("//*[@id='main']/table/tbody/tr/td[1]/a")).Text);
            Assert.AreEqual(displayPagination, driver.FindElement(By.XPath("//*[@id='pagination']/ul/li[2]/a")).Text);
            driver.Navigate().GoToUrl(url);

            //test sorting column asc/desc
            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[1]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=name&d=desc");

            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[2]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=introduced&d=desc");
            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[2]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=introduced&d=asc");

            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[3]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=discontinued&d=asc");
            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[3]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=discontinued&d=desc");

            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[4]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=companyName&d=desc");
            InteractiveElement.Click(driver, "//*[@id='main']/table/thead/tr/th[4]/a", GlobalVariable.typeXPath);
            Assert.AreEqual(driver.Url, url + "?p=0&s=companyName&d=asc");

            //test next/prev page
            InteractiveElement.Click(driver, ".next > a", GlobalVariable.typeCssSelector);
            Assert.AreEqual(driver.Url, url + "?p=1&s=companyName&d=asc");
            InteractiveElement.Click(driver, ".prev > a", GlobalVariable.typeCssSelector);
            Assert.AreEqual(driver.Url, url + "?p=0&s=companyName&d=asc");
        }

        [TestMethod]
        public void Test4_TestCloseBrowser()
        {
            CloseBrowser();
        }



        #region Private

        private void OpenBrowserUrl(string Url)
        {
            driver.Navigate().GoToUrl(Url);
            driver.Manage().Window.Maximize();
        }

        private void CloseBrowser()
        {
            driver.Close();
            driver.Quit();
        }

        #endregion
    }
}
