using FrameworkC.CommonLibs.Implementation;
using FrameworkC.CommonLibs.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkC.TelerikApp.Pages
{
    public class BasePage
    {

        public IWebDriver driver;
        public CommonElement cmnElement;

        private IWebElement AcceptCookiesButton => driver.FindElement(By.CssSelector("#onetrust-accept-btn-handler"));

        public BasePage(IWebDriver driver) {
            cmnElement = new CommonElement();
            this.driver = driver;
        }

        public void ClickAcceptAllCookies()
        {
            WaitUtils.WaitForPageToLoad(driver);
            //if (cmnElement.isEnabled(AcceptCookiesButton) && cmnElement.isDisplayed(AcceptCookiesButton))
            //{
            //    cmnElement.ClickElement(AcceptCookiesButton);
            //}
            cmnElement.ElementToBeClickable(AcceptCookiesButton);
            cmnElement.ClickElement(AcceptCookiesButton);
            WaitUtils.WaitForPageToLoad(driver);
        }
    }
}
