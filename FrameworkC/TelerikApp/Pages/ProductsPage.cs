using FrameworkC.CommonLibs.Enums;
using FrameworkC.CommonLibs.Utils;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace FrameworkC.TelerikApp.Pages
{
    public class ProductsPage : BasePage
    {
        private IWebElement Header => driver.FindElement(By.CssSelector("h1"));
        private string locatorBuyNowButtonPerProductName = "//th[contains(@class, 'foo')]/div/a";

        public ProductsPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public string GetHeaderText() {
            return cmnElement.GetText(Header);
        }

        public void ClickBuyNowButtonByProductName(ProductsEnum product) {
            string productName = HelperUtils.GetDescription(product);
            string formatProductName = Regex.Replace(productName, @"^([\w\-]+ )", "");
            string newLocator = locatorBuyNowButtonPerProductName.Replace("foo", formatProductName);
            driver.FindElement(By.XPath(newLocator)).Click();
            WaitUtils.WaitForPageToLoad(driver);
        }
    }
}
