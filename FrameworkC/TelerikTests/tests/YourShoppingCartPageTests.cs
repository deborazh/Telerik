using System;
using System.Collections.Generic;
using System.Text;
using AventStack.ExtentReports;
using EnumsNET;
using FrameworkC.CommonLibs.Enums;
using FrameworkC.CommonLibs.Implementation;
using FrameworkC.CommonLibs.Utils;
using FrameworkC.TelerikApp.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FrameworkC.TelerikTests.tests
{
   [TestFixture]
    class YourShoppingCartPageTests : BaseTests
    {
        public YourOrderPage yourOrderPage;
        public ProductsPage productsPage;

        //[Test]
        public void VerifySavingsLabelsAreNotDisplayedWhenQuantitiesAreWithDefaultValues() {
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Verify savings labels are not displayed when quantities are with default values");
            extentReportUtils.addTestLog(Status.Info, "VerifySavingsLabelsAreNotDisplayedWhenQuantityAreWithDefaultValues");
            CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            yourOrderPage.ClickAcceptAllCookies();

            // Assert default License quantity value
            Assert.AreEqual(1, yourOrderPage.GetSelectedLicenceQuantityValue());

            // Assert default Maintenance & Support quantity value
            Assert.AreEqual(HelperUtils.GetDescription(YearQuantity.ONE_YEAR), yourOrderPage.GetSelectedMaintenanceAndSupportQuantityValue());

            // Assert Savings Label is NOT displayed under Unit Price
            Assert.IsFalse(yourOrderPage.IsSavingsLabelForUnitPriceDisplayed());

            // Assert Savings Label is NOT displayed under Yearly Price
            Assert.IsFalse(yourOrderPage.IsSavingsLabelForYearlyPriceDisplayed());
        }

        //[Test]
        public void CheckTotalAndSubtotalValuesWhenThereAreDiscountsForBothLicensesAndYearlyDropDowns()
        {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check total and subtotal values when there are discounts for both licenses and yearly dropdowns");
            extentReportUtils.addTestLog(Status.Info, "CheckTotalAndSubtotalValuesWhenThereAreDiscountsForBothLicensesAndYearlyDropDowns");
            //CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            productsPage.ClickAcceptAllCookies();

            // Choose product and click on 'Buy Now' button for it
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_ULTIMATE);

            yourOrderPage.ClickAcceptAllCookies();

            Product product = new Product(ProductType.LICENSE_AND_YEARLY_DISCOUNT);

            // Set min values to have discounts for both licenses and yearly
            product.MinLicenseQty = 2;
            product.MinYearlyQty = 2;

            // Set Unit Price and M & S subscription price
            product.InitialUnitPrice = yourOrderPage.GetUnitPrice();
            product.MNsSubscriptionPrice = yourOrderPage.GetMnSSubscriptionPrice();

            // Set license quantity
            int randomLicenseQuantity = yourOrderPage.GetRandomLicenseQuantityWithMinValue(product.MinLicenseQty);
            product.LicenseQuantity = randomLicenseQuantity;

            // Set license discount
            product.LicenseDiscount = yourOrderPage.GetQuantityDiscounts(product.LicenseQuantity);

            // Calculate discount
            yourOrderPage.SetLicenseQuantity(product.LicenseQuantity);

            // Set Yearly quantity
            int randomYearQuantity = yourOrderPage.GetRandomYearQuantityWithMinValue(product.MinYearlyQty);
            product.YearlyQuantity = randomYearQuantity;
            product.YearlyDiscount = yourOrderPage.GetYearQuantityDiscounts(randomYearQuantity);
            yourOrderPage.SetMaintenanceAndSupportQuantity(product.YearlyQuantity);

            // Calculations
            product.CalculateValues();

            // Assert actual and expected unit price
            double actualUnitPrice = yourOrderPage.GetUnitPrice();
            Assert.AreEqual(product.UnitPrice, actualUnitPrice);

            // Check discount for licenses
            double actualDiscountForLicenses = yourOrderPage.GetSavingsLabelForUnitPrice();
            Assert.AreEqual(product.ExpectedDiscountForLicense, actualDiscountForLicenses);

            // Assert actual and expected yearly price
            double actualYearlyPrice = yourOrderPage.GetYearlyQuantityValue();
            Assert.AreEqual(product.ExpectedYearlyPrice, actualYearlyPrice);

            // Check discount
            double actualDiscountForYear = yourOrderPage.GetSavingsLabelForYearlyPrice();
            Assert.AreEqual(product.DiscountForYear, actualDiscountForYear);

            // Assert subtotal value
            double actualSubtotal = yourOrderPage.GetSubtotalValue();
            Assert.AreEqual(product.ExpectedYearlyPrice, actualSubtotal);

            // Assert renewal price
            double actualRenewalPrice = yourOrderPage.GetMnSSubscriptionPrice();
            Assert.AreEqual(product.ExpectedRenewalPrice, actualRenewalPrice);

            // Assert total license price
            double actualTotalLicensesPrice = yourOrderPage.GetTotalLicensesPrice();
            Assert.AreEqual(product.ExpectedTotalLicensesPrice, actualTotalLicensesPrice);

            // Assert total Maintenance price
            double actualMaintenancePrice = yourOrderPage.GetTotalMaintenancePrice();
            Assert.AreEqual(product.ExpectedMaintenancePrice, actualMaintenancePrice);

            // Assert total discounts
            double actualTotalDiscounts = yourOrderPage.GetTotalDiscounts();
            Assert.AreEqual(product.ExpectedTotalDiscounts, actualTotalDiscounts);

            // Assert Total value
            double actualTotalValue = yourOrderPage.GetTotalValue();
            Assert.AreEqual(product.ExpectedTotalValue, actualTotalValue);

            // Assert Subtotal and Total value
            Assert.AreEqual(product.ExpectedYearlyPrice, product.ExpectedTotalValue);
        }

        //[Test]
        public void CheckTotalAndSubtotalValuesWhenThereIsADiscountOnlyForLicenses()
        {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check total and subtotal values when there is a discount only for licenses");
            extentReportUtils.addTestLog(Status.Info, "CheckTotalAndSubtotalValuesWhenThereIsADiscountOnlyForLicenses");
            ////CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            productsPage.ClickAcceptAllCookies();

            // Choose product and click on 'Buy now' button for it
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_ULTIMATE);

            yourOrderPage.ClickAcceptAllCookies();

            Product product = new Product(ProductType.ONLY_LICENCE_DISCOUNT);

            // Set min values to have discounts for both licenses and yearly
            product.MinLicenseQty = 2;
            product.MinYearlyQty = 2;

            // Set Unit Price and M & S subscription price
            product.InitialUnitPrice = yourOrderPage.GetUnitPrice();
            product.MNsSubscriptionPrice = yourOrderPage.GetMnSSubscriptionPrice();

            // Set license quantity
            int randomLicenseQuantity = yourOrderPage.GetRandomLicenseQuantityWithMinValue(product.MinLicenseQty);
            product.LicenseQuantity = randomLicenseQuantity;

            // Set license discount
            product.LicenseDiscount = yourOrderPage.GetQuantityDiscounts(product.LicenseQuantity);

            // Set License quantity
            yourOrderPage.SetLicenseQuantity(product.LicenseQuantity);

            // Set yearly quantity to be 1 (we don't want to have a discount)
            product.YearlyQuantity = 1;
            product.YearlyDiscount = yourOrderPage.GetYearQuantityDiscounts(product.YearlyQuantity);

            // Calculations
            product.CalculateValues();

            // Assert actual and expected unit price
            double actualUnitPrice = yourOrderPage.GetUnitPrice();
            Assert.AreEqual(product.UnitPrice, actualUnitPrice);

            // Check discount for licenses
            double actualDiscountForLicenses = yourOrderPage.GetSavingsLabelForUnitPrice();
            Assert.AreEqual(product.ExpectedDiscountForLicense, actualDiscountForLicenses);

            // Check discount
            Assert.IsFalse(yourOrderPage.IsSavingsLabelForYearlyPriceDisplayed());

            // Assert subtotal value
            double actualSubtotal = yourOrderPage.GetSubtotalValue();
            Assert.AreEqual(product.ExpectedYearlyPrice, actualSubtotal);

            // Assert renewal price
            double actualRenewalPrice = yourOrderPage.GetMnSSubscriptionPrice();
            Assert.AreEqual(product.ExpectedRenewalPrice, actualRenewalPrice);

            // Assert total license price
            double actualTotalLicensesPrice = yourOrderPage.GetTotalLicensesPrice();
            Assert.AreEqual(product.ExpectedTotalLicensesPrice, actualTotalLicensesPrice);

            // Assert total discounts
            double actualTotalDiscounts = yourOrderPage.GetTotalDiscounts();
            Assert.AreEqual(product.ExpectedTotalDiscounts, actualTotalDiscounts);

            // Assert Total value
            double actualTotalValue = yourOrderPage.GetTotalValue();
            Assert.AreEqual(product.ExpectedTotalValue, actualTotalValue);

            // Assert Subtotal and Total value
            Assert.AreEqual(product.ExpectedYearlyPrice, product.ExpectedTotalValue);
        }

        //[Test]
        public void CheckTotalAndSubtotalValuesWhenThereIsADiscountOnlyForYearly()
        {
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check total and subtotal values when there are discounts for both licenses and yearly dropdowns");
            extentReportUtils.addTestLog(Status.Info, "CheckTotalAndSubtotalValuesWhenThereAreDiscountsForBothLicensesAndYearlyDropDowns");
            CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            yourOrderPage.ClickAcceptAllCookies();

            Product product = new Product(ProductType.ONLY_YEARLY_DISCOUNT);

            product.LicenseQuantity = 1;
            product.MinYearlyQty = 2;

            // Set Unit Price and M & S subscription price
            product.InitialUnitPrice = yourOrderPage.GetUnitPrice();
            product.MNsSubscriptionPrice = yourOrderPage.GetMnSSubscriptionPrice();

            // Assert Savings Label is NOT displayed under Unit Price
            Assert.IsFalse(yourOrderPage.IsSavingsLabelForUnitPriceDisplayed());

            // Set Yearly quantity
            int randomYearQuantity = yourOrderPage.GetRandomYearQuantityWithMinValue(product.MinYearlyQty);
            product.YearlyQuantity = randomYearQuantity;
            product.YearlyDiscount = yourOrderPage.GetYearQuantityDiscounts(randomYearQuantity);
            yourOrderPage.SetMaintenanceAndSupportQuantity(product.YearlyQuantity);

            // Calculations
            product.CalculateValues();

            // Assert actual and expected unit price
            double actualUnitPrice = yourOrderPage.GetUnitPrice();
            Assert.AreEqual(product.UnitPrice, actualUnitPrice);

            // Assert actual and expected yearly price
            double actualYearlyPrice = yourOrderPage.GetYearlyQuantityValue();
            Assert.AreEqual(product.ExpectedYearlyPrice, actualYearlyPrice);

            // Check discount
            double actualDiscountForYear = yourOrderPage.GetSavingsLabelForYearlyPrice();
            Assert.AreEqual(product.DiscountForYear, actualDiscountForYear);

            // Assert subtotal value
            double actualSubtotal = yourOrderPage.GetSubtotalValue();
            Assert.AreEqual(product.ExpectedYearlyPrice, actualSubtotal);

            // Assert renewal price
            double actualRenewalPrice = yourOrderPage.GetMnSSubscriptionPrice();
            Assert.AreEqual(product.ExpectedRenewalPrice, actualRenewalPrice);

            // Assert total Maintenance price
            double actualMaintenancePrice = yourOrderPage.GetTotalMaintenancePrice();
            Assert.AreEqual(product.ExpectedMaintenancePrice, actualMaintenancePrice);

            // Assert total discounts
            double actualTotalDiscounts = yourOrderPage.GetTotalDiscounts();
            Assert.AreEqual(product.ExpectedTotalDiscounts, actualTotalDiscounts);

            // Assert Total value
            double actualTotalValue = yourOrderPage.GetTotalValue();
            Assert.AreEqual(product.ExpectedTotalValue, actualTotalValue);

            // Assert Subtotal and Total value
            Assert.AreEqual(product.ExpectedYearlyPrice, product.ExpectedTotalValue);
        }

        //[Test]
        public void VerifyUserIsNavigatedBackToProductPageWhenRemovingExistingProductFromCard() {
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            productsPage = new ProductsPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Verify the user is navigated back to Product page when removing the existing product from the card");
            extentReportUtils.addTestLog(Status.Info, "VerifyUserIsNavigatedBackToProductPageWhenRemovingExistingProductFromCard");
            //CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            productsPage.ClickAcceptAllCookies();

            // Choose product and click on 'Buy Now' button for it
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_COMPLETE);

            yourOrderPage.ClickAcceptAllCookies();

            // Click Delete item link by product name
            yourOrderPage.ClickDeleteItemButtonByProductName(ProductsEnum.DEVCRAFT_COMPLETE);

            // Assert Emtry Card message text
            Assert.AreEqual("Your shopping cart is empty!", yourOrderPage.GetEmptyCardMessageText());

            // Click "Browse Products" button
            yourOrderPage.ClickBrowseProductsButton();

            // Assert Header on Product page
            Assert.AreEqual("Pricing", productsPage.GetHeaderText());
        }

        //[Test]
        public void CheckErrorMessageForInvalidCoupon() {
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check error message for an invalid coupon");
            extentReportUtils.addTestLog(Status.Info, "CheckErrorMessageForInvalidCoupon");
            CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            yourOrderPage.ClickAcceptAllCookies();

            // Set coupon
            yourOrderPage.SetCoupon("TestDiscount20");

            // Assert error message
            Assert.AreEqual("Coupon code is not valid.", yourOrderPage.GetCouponErrorMessageText());
        }

        //[Test]
        public void CheckLicenseQuantityWhenUserAddTheSameProductTwiceToCard() {
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            productsPage = new ProductsPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check license quantity when user add the same product twice to card");
            extentReportUtils.addTestLog(Status.Info, "CheckLicenseQuantityWhenUserAddTheSameProductTwiceToCard");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_COMPLETE);

            yourOrderPage.ClickAcceptAllCookies();

            string productName = HelperUtils.GetDescription(ProductsEnum.DEVCRAFT_COMPLETE);

            Assert.IsTrue(yourOrderPage.IsProductDisplayedByProductName(productName));

            // Assert only one product is displayed on the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Assert default License quantity value
            Assert.AreEqual(1, yourOrderPage.GetSelectedLicenceQuantityValue());

            Product product = new Product(ProductType.LICENSE_AND_YEARLY_DISCOUNT);
            int qty = 1;
            product.LicenseQuantity = qty;
            product.YearlyQuantity = qty;

            // Set Unit Price and M & S subscription price
            product.InitialUnitPrice = yourOrderPage.GetUnitPrice();
            product.MNsSubscriptionPrice = yourOrderPage.GetMnSSubscriptionPrice();

            // Set license discount
            product.LicenseDiscount = yourOrderPage.GetQuantityDiscounts(product.LicenseQuantity);

            // Calculate discount
            yourOrderPage.SetLicenseQuantity(product.LicenseQuantity);

            product.YearlyDiscount =(yourOrderPage.GetYearQuantityDiscounts(qty));
            yourOrderPage.SetMaintenanceAndSupportQuantity(product.YearlyQuantity);

            // Calculations
            product.CalculateValues();

            // Assert actual and expected unit price
            double actualUnitPrice = yourOrderPage.GetUnitPrice();
            Assert.AreEqual(product.UnitPrice, actualUnitPrice);

            // Assert subtotal value
            double actualSubtotal = yourOrderPage.GetSubtotalValue();
            Assert.AreEqual(product.ExpectedYearlyPrice, actualSubtotal);

            // Assert renewal price
            double actualRenewalPrice = yourOrderPage.GetMnSSubscriptionPrice();
            Assert.AreEqual(product.ExpectedRenewalPrice, actualRenewalPrice);

            // Assert Total value
            double actualTotalValue = yourOrderPage.GetTotalValue();
            Assert.AreEqual(product.ExpectedTotalValue, actualTotalValue);

            // Assert Subtotal and Total value
            Assert.AreEqual(product.ExpectedYearlyPrice, product.ExpectedTotalValue);

            // Click 'Continue Shopping' button
            yourOrderPage.ClickContinueShoppingButton();

            // Click on 'Buy now" button for same product
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_COMPLETE);

            // Assert only one product is displayed on the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            int expectedLicenseQty2 = 2;

            // Assert license Qty is incresed
            Assert.AreEqual(expectedLicenseQty2, yourOrderPage.GetSelectedLicenceQuantityValue());

            product.LicenseQuantity = expectedLicenseQty2;

            // Calculate discount
            yourOrderPage.SetLicenseQuantity(product.LicenseQuantity);

            product.CalculateValues();

            // Assert Subtotal and Total value
            Assert.AreEqual(product.ExpectedYearlyPrice, product.ExpectedTotalValue);
        }

        [Test]
        public void VerifyProductDetailsAreDisplayedWhenUserClicksOnProductName()
        {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Verify Product details are displayed when a user clicks on product name");
            extentReportUtils.addTestLog(Status.Info, "VerifyProductDetailsAreDisplayedWhenUserClicksOnProductName");
            //CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            productsPage.ClickAcceptAllCookies();

            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            string productName = HelperUtils.GetDescription(ProductsEnum.DEVCRAFT_UI);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert productDetails are not displayed
            Assert.IsFalse(yourOrderPage.IsProductDetailsDisplayedByProductName(productName));

            // Click on product name
            yourOrderPage.ClickOnProductByProductName(productName);

            // Assert product details are displayed
            Assert.IsTrue(yourOrderPage.IsProductDetailsDisplayedByProductName(productName));

            // Click again on product name
            yourOrderPage.ClickOnProductByProductName(productName);

            // Assert productDetails are not displayed
            Assert.IsFalse(yourOrderPage.IsProductDetailsDisplayedByProductName(productName));
        }


        private void retreiveAllProductInformation(Product product)
        {
            // Set Unit Price and M & S subscription price
            product.InitialUnitPrice = yourOrderPage.GetUnitPrice();
            product.MNsSubscriptionPrice = yourOrderPage.GetMnSSubscriptionPrice();

            // Set license quantity
            int randomLicenseQuantity = yourOrderPage.GetRandomLicenseQuantityWithMinValue(product.MinLicenseQty);
            product.LicenseQuantity = randomLicenseQuantity;

            // Set license discount
            product.LicenseDiscount = yourOrderPage.GetQuantityDiscounts(product.LicenseQuantity);

            yourOrderPage.SetLicenseQuantity(product.LicenseQuantity);

            // Set Yearly quantity
            //int randomYearQuantity = yourOrderPage.GetRandomYearQuantityWithMinValue(2);
            int randomYearQuantity = yourOrderPage.GetRandomYearQuantityWithMinValue(product.MinYearlyQty);
            //product.SetYearlyQuantity(yourOrderPage.GetRandomYearQuantityWithMinValue(2));
            product.YearlyQuantity = randomYearQuantity;
            //int yearDiscount = yourOrderPage.GetYearQuantityDiscounts(randomYearQuantity);
            product.YearlyDiscount = yourOrderPage.GetYearQuantityDiscounts(randomYearQuantity);
            yourOrderPage.SetMaintenanceAndSupportQuantity(product.YearlyQuantity);
        }
    }
}
