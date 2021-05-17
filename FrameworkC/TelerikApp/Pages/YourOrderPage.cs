using FrameworkC.CommonLibs.Enums;
using FrameworkC.CommonLibs.Implementation;
using FrameworkC.CommonLibs.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace FrameworkC.TelerikApp.Pages
{
    public class YourOrderPage : BasePage
    {

        private IWebElement Header => driver.FindElement(By.CssSelector("h1"));

        private ReadOnlyCollection<IWebElement> DiscountRangeList => driver.FindElements(By.CssSelector("div.discount-label"));
        private ReadOnlyCollection<IWebElement> DiscountYearList => driver.FindElements(By.CssSelector("div.discount-item"));
        private ReadOnlyCollection<IWebElement> QuantityList => driver.FindElements(By.XPath("//div[@class='k-list-scroller']/ul//li/div"));
        private ReadOnlyCollection<IWebElement> ProductList => driver.FindElements(By.CssSelector("td.product-name-cell"));
        private IWebElement LicenseQuantity => driver.FindElement(By.CssSelector("quantity-select span.k-dropdown-wrap"));
        private IWebElement MaintenanceAndSupportQuantity => driver.FindElement(By.CssSelector("period-select span.k-dropdown-wrap"));
        private IWebElement UnitPriceLabel => driver.FindElement(By.XPath("//*[@data-label='Licenses']//span[contains(@class, 'price-per-license-label')]"));
        private IWebElement SavingsLabelForUnitPrice => driver.FindElement(By.XPath("//*[@data-label='Licenses']//span[contains(@class, 'savings-label')]"));
        private IWebElement MnSSubscriptionPrice => driver.FindElement(By.CssSelector("label > span:not(.tooltip-holder)"));
        private IWebElement YearlyPriceLabel => driver.FindElement(By.XPath("//*[contains(@data-label, 'Maintenance')]//span[contains(@class, 'price-per-license-label')]"));
        private IWebElement SavingsLabelForYearlyPrice => driver.FindElement(By.XPath("//*[@data-label='Maintenance & Support']//span[contains(@class, 'savings-label')]"));
        private IWebElement SubtotalValue => driver.FindElement(By.CssSelector("div.e2e-cart-item-subtotal"));
        private IWebElement TotalLicensesPrice => driver.FindElement(By.CssSelector("span.e2e-licenses-price"));
        private IWebElement TotalMaintenancePrice => driver.FindElement(By.CssSelector("span.e2e-maintenance-price"));
        private IWebElement TotalDiscounts => driver.FindElement(By.CssSelector("span.e2e-total-discounts-price"));
        private IWebElement TotalValue => driver.FindElement(By.CssSelector("span.e2e-total-price"));
        private IWebElement EmptyCardMessage => driver.FindElement(By.CssSelector("h2.e2e-empty-shopping-cart-heading"));
        private IWebElement BrowseProductsButton => driver.FindElement(By.CssSelector("a.e2e-browse-products"));
        private IWebElement CouponInput => driver.FindElement(By.CssSelector("input.e2e-enter-coupon-code"));
        private IWebElement ApplyCouponButton => driver.FindElement(By.CssSelector("button.e2e-save-coupon-code"));
        private IWebElement CouponErrorMessage => driver.FindElement(By.CssSelector("div.error-message"));
        private IWebElement ContinueShoppingButton => driver.FindElement(By.CssSelector("a.continue-shopping"));

        private IWebElement ContinueButton => driver.FindElement(By.CssSelector("button.e2e-continue"));


        private By SavingsLabelForUnitPriceLocator = By.XPath("//*[@data-label='Licenses']//span[contains(@class, 'savings-label')]");
        private By SavingsLabelForYearlyPriceLocator = By.XPath("//*[@data-label='Maintenance & Support']//span[contains(@class, 'savings-label')]");
        private string locatorForLicenseDropdownValues = "//div[@class='k-list-scroller']/ul//li/div[normalize-space()='foo']";
        private string locatorForYearlyDropdownValues = "//div[@class='k-list-scroller']/ul//li/div//span[@class='ng-star-inserted' and normalize-space()='foo']";
        private string locatorForDeleteItemBttnByProductName = "//div[contains(@class, 'product-name') and text() = 'foo']/../../div[contains(@class, 'license-agreement')]/a[contains(@class,'btn-delete-item')]";
        private string locatorForProductName = "//div[contains(@class, 'product-name') and normalize-space(text()) = 'foo']";
        private string locatorForAutoRenewalCheckboxByProductName = "//div[contains(@class, 'product-name') and normalize-space(text()) = 'foo']/ancestor::tr//following-sibling::auto-renewal[1]//input";
        private string locatorForProductDetailsByProductName = "//div[contains(@class, 'product-name') and normalize-space(text()) = 'foo']/ancestor::td/div[contains(@class, 'ng-star-inserted') and not(contains(@class, 'license-agreement')) and not (contains(@class, 'price-small'))]";

        public YourOrderPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public bool IsSavingsLabelForUnitPriceDisplayed()
        {
            return cmnElement.isElementDisplayedByLocator(driver, SavingsLabelForUnitPriceLocator);
        }

        public bool IsSavingsLabelForYearlyPriceDisplayed()
        {
            return cmnElement.isElementDisplayedByLocator(driver, SavingsLabelForYearlyPriceLocator);
        }

        public bool IsProductDisplayedByProductName(string productName) {
            string newLocator = locatorForProductName.Replace("foo", productName);
            return cmnElement.isDisplayed(driver.FindElement(By.XPath(newLocator)));
        }

        public bool IsProductDetailsDisplayedByProductName(string productName) {
            string newLocator = locatorForProductDetailsByProductName.Replace("foo", productName);
            IWebElement element = driver.FindElement(By.XPath(newLocator));
            if (element.GetAttribute("aria-expanded") == "false")
            {
                return true;
            }
            else {
                return false;
            }
        }

        public int GetQuantityCount()
        {
            OpenLicenseQuantityDropdown();
            return QuantityList.Count;
        }

        public int GetYearlyCount()
        {
            OpenYearQuantityDropdown();
            return QuantityList.Count;
        }

        public double GetUnitPrice()
        {
            WaitUtils.WaitForLoaderToDisapear(driver);

            string unitPriceLabelText = UnitPriceLabel.Text;
            string removeDollarSignUnitPrice = Regex.Replace(unitPriceLabelText, @"[$ ]+", "");
            return Double.Parse(removeDollarSignUnitPrice);
        }

        public double GetSavingsForUnitPrice()
        {
            WaitUtils.WaitForLoaderToDisapear(driver);

            string savings = SavingsLabelForUnitPrice.Text;
            string removeDollarSignSavings = Regex.Replace(savings, @"[$ ]+", "");
            return Double.Parse(removeDollarSignSavings);
        }

        public int GetRandomLicenseQuantity()
        {
            int quantityCount = GetQuantityCount();
            return HelperUtils.GetRandomValue(quantityCount);
        }

        public int GetRandomLicenseQuantityWithMinValue(int min)
        {
            int quantityCount = GetQuantityCount();
            return HelperUtils.GetRandomValue(min, quantityCount);
        }

        public int GetRandomYearQuantityWithMinValue(int min)
        {
            int yearQuantityCount = GetYearlyCount();
            return HelperUtils.GetRandomValue(min, yearQuantityCount);
        }

        public int GetRandomYearQuantity()
        {
            return GetRandomYearQuantityWithMinValue(1);
        }

        public int GetQuantityDiscounts(int randomQuantity)
        {
            Discounts discounts = new Discounts(DiscountType.LICENSE);

            OpenLicenseQuantityDropdown();
            int quantityCount = GetQuantityCount();
            discounts.ReadDiscountsFromList(DiscountRangeList, quantityCount);
            CloseLicenseQuantityDropdown();

            return discounts.GetDiscountByKey(randomQuantity);
        }

        public int GetYearQuantityDiscounts(int randomYearQuantity)
        {
            Discounts discounts = new Discounts(DiscountType.YEARLY);

            OpenYearQuantityDropdown();
            int yearQuantityCount = GetYearlyCount();
            discounts.ReadDiscountsFromList(DiscountYearList, yearQuantityCount);
            CloseYearQuantityDropdown();

            return discounts.GetDiscountByKey(randomYearQuantity - 1);
        }

        public int GetSelectedLicenceQuantityValue()
        {
            string text = LicenseQuantity.Text;
            return int.Parse(text);
        }

        public double GetYearlyQuantityValue()
        {
            string text = YearlyPriceLabel.Text;
            if (text.Contains("Included"))
            {
                return 0;
            }
            else
            {
                string removeDollarSign = Regex.Replace(text, @"[$ ]+", "");
                return Double.Parse(removeDollarSign);
            }
        }

        public string GetSelectedMaintenanceAndSupportQuantityValue()
        {
            return MaintenanceAndSupportQuantity.Text;
        }

        public double GetMnSSubscriptionPrice()
        {
            string mNsSubscriptionPrice = MnSSubscriptionPrice.Text;
            string removeDollarSign = Regex.Replace(mNsSubscriptionPrice, @"[$ ]+", "");
            return Double.Parse(removeDollarSign);
        }

        public double GetSavingsLabelForUnitPrice()
        {
                string removeText = Regex.Replace(SavingsLabelForUnitPrice.Text, @"[+$A-z ]+", "");
                return Double.Parse(removeText);
        }

        public double GetSavingsLabelForYearlyPrice()
        {
                string text = Regex.Replace(SavingsLabelForYearlyPrice.Text, @"[+$A-z ]+", "");
                return Double.Parse(text);
        }

        public double GetTotalMaintenancePrice()
        {
            string text = Regex.Replace(TotalMaintenancePrice.Text, @"[+$A-z ]+", "");
            return Double.Parse(text);
        }

        public string GetEmptyCardMessageText()
        {
            return EmptyCardMessage.Text.Trim();
        }

        public double GetSubtotalValue()
        {
            string text = Regex.Replace(SubtotalValue.Text, @"[+$A-z ]+", "");
            return Double.Parse(text);
        }

        public double GetTotalLicensesPrice()
        {
            string text = Regex.Replace(TotalLicensesPrice.Text, @"[+$A-z ]+", "");
            return Double.Parse(text);
        }

        public double GetTotalDiscounts()
        {
            string text = Regex.Replace(TotalDiscounts.Text, @"[-+$A-z ]+", "");
            return Double.Parse(text);
        }

        public double GetTotalValue()
        {
            string text = Regex.Replace(TotalValue.Text, @"[-+$A-z ]+", "");
            return Double.Parse(text);
        }

        public string GetCouponErrorMessageText() {
            return CouponErrorMessage.Text.Trim();
        }

        public int GetProductsCountFromPage() {
            return ProductList.Count;
        }

        public void SetLicenseQuantity(int item)
        {
            SetQuantity(LicenseQuantity, item, locatorForLicenseDropdownValues);
        }

        public void SetMaintenanceAndSupportQuantity(int item)
        {
            string text = HelperUtils.GetDescription((YearQuantity)item - 1);
            SetQuantityAsString(MaintenanceAndSupportQuantity, text, locatorForYearlyDropdownValues);
            WaitUtils.WaitForLoaderToDisapear(driver);
        }

        public void SetQuantity(IWebElement element, int item, string locator)
        {
            SetQuantityAsString(element, item.ToString(), locator);
        }

        public void SetQuantityAsString(IWebElement element, string item, string locator)
        {
            KendoDropdown kendoDropdown = new KendoDropdown(element);

            kendoDropdown.SelectItem(locator, item);
            WaitUtils.WaitForLoaderToDisapear(driver);
        }

        public void SetCoupon(string text)
        {
            cmnElement.SetText(CouponInput, text);
            ClickApplyCouponButton();
        }

        public void ClickDeleteItemButtonByProductName(ProductsEnum product)
        {
            string productName = HelperUtils.GetDescription(product);
            string newLocator = locatorForDeleteItemBttnByProductName.Replace("foo", productName);
            cmnElement.ClickElement(driver.FindElement(By.XPath(newLocator)));
        }

        public void ClickBrowseProductsButton()
        {
            cmnElement.ClickElement(BrowseProductsButton);
        }

        public void ClickApplyCouponButton()
        {
            cmnElement.ClickElement(ApplyCouponButton);
        }

        public void ClickContinueButton() {
            cmnElement.ClickElement(ContinueButton);
        }

        public void ClickContinueShoppingButton() {
            cmnElement.ClickElement(ContinueShoppingButton);
        }

        public void ClickAutomaticalyRenewalCheckbox(string productName) {
            string newLocator = locatorForAutoRenewalCheckboxByProductName.Replace("foo", productName);
            cmnElement.ClickElement(driver.FindElement(By.XPath(newLocator)));
            WaitUtils.WaitForLoaderToDisapear(driver);
        }

        public void ClickOnProductByProductName(string productName) {
            string newLocator = locatorForProductName.Replace("foo", productName);
            cmnElement.ClickElement(driver.FindElement(By.XPath(newLocator)));
        }

        public void OpenDropdown(IWebElement element)
        {
            KendoDropdown dropdown = new KendoDropdown(element);
            dropdown.Expand();
            WaitUtils.WaitForLoaderToDisapear(driver);
        }

        public void CloseDropdown(IWebElement element)
        {
            KendoDropdown dropdown = new KendoDropdown(element);
            if (QuantityList.Count > 0)
            {
                dropdown.Collapse();
                WaitUtils.WaitForPageToLoad(driver);
            }
        }

        public void OpenLicenseQuantityDropdown()
        {
            OpenDropdown(LicenseQuantity);
        }

        public void OpenYearQuantityDropdown()
        {
            OpenDropdown(MaintenanceAndSupportQuantity);
        }

        public void CloseLicenseQuantityDropdown()
        {
            CloseDropdown(LicenseQuantity);
        }

        public void CloseYearQuantityDropdown()
        {
            CloseDropdown(MaintenanceAndSupportQuantity);
        }
    }
}
