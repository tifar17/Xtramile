using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XtramileTest
{
    [TestClass]
    public class InternetHookuappTest
    {
        public static IWebDriver driver = new ChromeDriver();
        public static string url = "https://the-internet.herokuapp.com/javascript_alerts";
        public static string urlForkMe = "https://github.com/saucelabs/the-internet";
        public static string urlSelenium = "https://elementalselenium.com/";

        public static string title = "The Internet";
        public static string inputJsPrompt = "this is selenium";
        public static string btnJsAlert = "Click for JS Alert";
        public static string btnJsConfirm = "Click for JS Confirm";
        public static string btnJsPrompt = "Click for JS Prompt";
        public static string alertJsAlert = "I am a JS Alert";
        public static string alertJsConfirm = "I am a JS Confirm";
        public static string alertJsPrompt = "I am a JS prompt";
        public static string messageSuccessJsAlert = "You successfully clicked an alert";
        public static string messageSuccessJsConfirmOk = "You clicked: Ok";
        public static string messageSuccessJsConfirmCancel = "You clicked: Cancel";
        public static string messageSuccessJsPromptOk = "You entered: "+ inputJsPrompt;
        public static string messageSuccessJsPromptCancel = "You entered: null";



        [TestMethod]
        public void Test1_TestOpenUrl()
        {
            OpenBrowserUrl(url);
            Assert.AreEqual(title, driver.Title);
        }

        [TestMethod]
        public void Test2_TestJsAlert()
        {
            //test button js exist
            Assert.AreEqual(btnJsAlert, InteractiveElement.GetText(driver, "//*[@id='content']/div/ul/li[1]/button", GlobalVariable.typeXPath));
            Assert.AreEqual(btnJsConfirm, InteractiveElement.GetText(driver, "//*[@id='content']/div/ul/li[2]/button", GlobalVariable.typeXPath));
            Assert.AreEqual(btnJsPrompt, InteractiveElement.GetText(driver, "//*[@id='content']/div/ul/li[3]/button", GlobalVariable.typeXPath));

            //test click js alert
            InteractiveElement.Click(driver, "//*[@id='content']/div/ul/li[1]/button", GlobalVariable.typeXPath);
            Assert.AreEqual(alertJsAlert, driver.SwitchTo().Alert().Text);
            driver.SwitchTo().Alert().Accept();
            Assert.AreEqual(messageSuccessJsAlert, InteractiveElement.GetText(driver, "result", GlobalVariable.typeId));
        }

        [TestMethod]
        public void Test3_TestJsConfirm()
        {
            //test click js confirm ok
            InteractiveElement.Click(driver, "//*[@id='content']/div/ul/li[2]/button", GlobalVariable.typeXPath);
            Assert.AreEqual(alertJsConfirm, driver.SwitchTo().Alert().Text);
            driver.SwitchTo().Alert().Accept();
            Assert.AreEqual(messageSuccessJsConfirmOk, InteractiveElement.GetText(driver, "result", GlobalVariable.typeId));

            //test click js confirm cancel
            InteractiveElement.Click(driver, "//*[@id='content']/div/ul/li[2]/button", GlobalVariable.typeXPath);
            Assert.AreEqual(alertJsConfirm, driver.SwitchTo().Alert().Text);
            driver.SwitchTo().Alert().Dismiss();
            Assert.AreEqual(messageSuccessJsConfirmCancel, InteractiveElement.GetText(driver, "result", GlobalVariable.typeId));
        }

        [TestMethod]
        public void Test4_TestJsPrompt()
        {
            //test click js prompt ok
            InteractiveElement.Click(driver, "//*[@id='content']/div/ul/li[3]/button", GlobalVariable.typeXPath);
            Assert.AreEqual(alertJsPrompt, driver.SwitchTo().Alert().Text);
            driver.SwitchTo().Alert().SendKeys(inputJsPrompt);
            driver.SwitchTo().Alert().Accept();
            Assert.AreEqual(messageSuccessJsPromptOk, InteractiveElement.GetText(driver, "result", GlobalVariable.typeId));

            //test click js prompt cancel
            InteractiveElement.Click(driver, "//*[@id='content']/div/ul/li[3]/button", GlobalVariable.typeXPath);
            Assert.AreEqual(alertJsPrompt, driver.SwitchTo().Alert().Text);
            driver.SwitchTo().Alert().SendKeys(inputJsPrompt);
            driver.SwitchTo().Alert().Dismiss();
            Assert.AreEqual(messageSuccessJsPromptCancel, InteractiveElement.GetText(driver, "result", GlobalVariable.typeId));
        }

        [TestMethod]
        public void Test5_TestLink()
        {
            //test click elemental selenium
            InteractiveElement.Click(driver, "#page-footer > div > div > a", GlobalVariable.typeCssSelector);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Assert.AreEqual(urlSelenium, driver.Url);

            //test click fork me on GitHub
            driver.SwitchTo().Window(driver.WindowHandles.First());
            InteractiveElement.Click(driver, "/html/body/div[2]/a/img", GlobalVariable.typeXPath);
            Assert.AreEqual(urlForkMe, driver.Url);
        }

        [TestMethod]
        public void Test6_TestCloseBrowser()
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
