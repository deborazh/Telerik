using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using FrameworkC.CommonLibs.Utils;

namespace FrameworkC.CommonLibs.Implementation
{
    class KendoDropdown
    {

        IWebDriver driver;
        String name;
        IWebElement parentElem;

        public KendoDropdown(IWebElement parentElem)
        {
            this.parentElem = parentElem;
            name = parentElem.GetAttribute("aria-owns");
            this.driver = ((IWrapsDriver)parentElem).WrappedDriver;

        }

        /**
         * Returns selected item from the Dropdown
         * @return
         */
        public String GetSelection()
        {
            return parentElem.FindElement(By
                    .CssSelector("span.k-state-default span.k-input"))
                    .Text;
        }

        /**
         * Select item in the Dropdown
         * @param item text
         */
        public void SelectItem(string item)
        {
            Expand();
            driver.FindElement(By.XPath("//div[@class='k-list-scroller']/ul//li/div[normalize-space()='" + item + "']")).Click();
        }

        public void SelectItem(string locator, string item) {
            Expand();
            WaitUtils.WaitForLoaderToDisapear(driver);
            string newLocator = locator.Replace("foo", item);
            HelperUtils.ScrollToElement(driver.FindElement(By.XPath(newLocator)), driver);           
            WaitUtils.WaitForLoaderToDisapear(driver);
        }

        public void SelectCountryItem(string locator, string item) {
            string newLocator = locator.Replace("foo", item);
            HelperUtils.ScrollToElement(driver.FindElement(By.XPath(newLocator)), driver);
        }

        /**
         * Open Dropdown
         */
        public void Expand()
        {
            if (!parentElem.GetAttribute("aria-expanded").Equals("true"))
            {
                parentElem.FindElement(By.CssSelector("span.k-icon")).Click();
            }
        }

        /**
         * Close Dropdown
         */
        public void Collapse()
        {
            if (!parentElem.GetAttribute("aria-expanded").Equals("false"))
            {
                parentElem.FindElement(By.CssSelector("span.k-icon")).Click();
            }
        }  
    }
}
