using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XtramileTest
{
    class InteractiveElement
    {
        //Enter Text
        public static void EnterText(IWebDriver driver, string element, string value ,string type)
        {
            if (type == GlobalVariable.typeId)
                driver.FindElement(By.Id(element)).SendKeys(value);
            if (type == GlobalVariable.typeName)
                driver.FindElement(By.Name(element)).SendKeys(value);
        }

        //Click
        public static void Click(IWebDriver driver, string element, string type)
        {
            if (type == GlobalVariable.typeId)
                driver.FindElement(By.Id(element)).Click();
            if (type == GlobalVariable.typeName)
                driver.FindElement(By.Name(element)).Click();
            if (type == GlobalVariable.typeClassName)
                driver.FindElement(By.ClassName(element)).Click();
            if (type == GlobalVariable.typeCssSelector)
                driver.FindElement(By.CssSelector(element)).Click();
            if (type == GlobalVariable.typeXPath)
                driver.FindElement(By.XPath(element)).Click();
        }

        //Get Text
        public static string GetText(IWebDriver driver, string element, string type)
        {
            if (type == GlobalVariable.typeId)
                return driver.FindElement(By.Id(element)).Text;
            if (type == GlobalVariable.typeName)
                return driver.FindElement(By.Name(element)).Text;
            if (type == GlobalVariable.typeClassName)
                return driver.FindElement(By.ClassName(element)).Text;
            if (type == GlobalVariable.typeXPath)
                return driver.FindElement(By.XPath(element)).Text;
            else return String.Empty;
        }

        public static void SelectDropdown(IWebDriver driver, string element, string value, string type)
        {
            if (type == GlobalVariable.typeId)
            {
                SelectElement dropDown = new SelectElement(driver.FindElement(By.Id(element)));
                dropDown.SelectByText(value);
            }
            if (type == GlobalVariable.typeName)
            {
                SelectElement dropDown = new SelectElement(driver.FindElement(By.Name(element)));
                dropDown.SelectByText(value);
            }
        }

        //Is Element Exist
        public static bool IsExist(IWebDriver driver, string element, string type)
        {
            if (type == GlobalVariable.typeId)
            {
                List<IWebElement> e = new List<IWebElement>();
                e.AddRange(driver.FindElements(By.Id(element)));
                if(e.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (type == GlobalVariable.typeName)
            {
                List<IWebElement> e = new List<IWebElement>();
                e.AddRange(driver.FindElements(By.Name(element)));
                if (e.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return false;
        }
    }
}
