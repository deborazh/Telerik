using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkC.CommonLibs.Implementation
{
    public class CommonElement
    {
        public void ClickElement(IWebElement element) => element.Click();

        public void ClearText(IWebElement element) => element.Clear();

        public void SetText(IWebElement element, string text) => element.SendKeys(text);

        public string GetText(IWebElement element) => element.Text;

        public bool isDisplayed(IWebElement element) => element.Displayed;

        public bool isSelected(IWebElement element) => element.Selected;

        public bool isEnabled(IWebElement element) => element.Enabled;
        public bool isNotEnabled(IWebElement element) => !element.Enabled;

        public bool isElementDisplayedByLocator(IWebDriver driver, By by) {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        public void ClearTextAndSetText(IWebElement element, string text)
        {
            ClearText(element);
            SetText(element, text);
        }

        public Func<IWebDriver, IWebElement> ElementToBeClickable(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    if (element != null && element.Displayed && element.Enabled)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }
    }
}
