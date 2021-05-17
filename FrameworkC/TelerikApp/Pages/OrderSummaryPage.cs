using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkC.TelerikApp.Pages
{
    public class OrderSummaryPage : BasePage
    {
        private IWebElement Header => driver.FindElement(By.CssSelector("h1"));
        private IWebElement CompanyBillingInfo => driver.FindElement(By.CssSelector("dd.e2e-billing-info-company"));



        public OrderSummaryPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public string GetHeaderText() {
            return cmnElement.GetText(Header);
        }

        public string GetCompanyBillingInfoText() {
            return cmnElement.GetText(CompanyBillingInfo).Trim();
        }
    }
}
