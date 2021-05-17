using FrameworkC.CommonLibs.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkC.CommonLibs.Utils
{
    class WaitUtils
    {
        public static void WaitForPageToLoad(IWebDriver driver) {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void WaitForElementPresentBy(IWebDriver driver, By by)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (TimeoutException te)
            {
                Assert.Fail("The element with selector {0} didn't appear. The exception was:\n {1}", by, te.ToString());
            }
        }

        public static void WaitForLoaderToDisapear(IWebDriver driver) {
            WaitForElementPresentBy(driver, By.CssSelector("div.panel-body div.loader-content:not(.is-loading)"));
        }

        //public static IWebElement WaitUntilElementVisible(By elementLocator, int timeout = 10)
        //{
        //    try
        //    {
        //        var wait = new WebDriverWait(cmdDriver.Driver, TimeSpan.FromSeconds(timeout));
        //        return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));
        //    }
        //    catch (NoSuchElementException)
        //    {
        //        Console.WriteLine("Element with locator: '" + elementLocator + "' was not found.");
        //        throw;
        //    }
        //}
    }
}
