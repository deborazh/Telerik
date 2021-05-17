using AventStack.ExtentReports;
using FrameworkC.CommonLibs.Enums;
using FrameworkC.CommonLibs.Implementation;
using FrameworkC.CommonLibs.Utils;
using FrameworkC.TelerikApp.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkC.TelerikTests.tests
{
    class ContactInfoPageTests : BaseTests
    {
        public YourOrderPage yourOrderPage;
        public ProductsPage productsPage;
        public ContactInfoPage contactInfoPage;
        public OrderSummaryPage orderSummaryPage;

        [Test]
        public void CheckAutoRenewNoteIsDisplayedOnContactInfoPageWhenTheUserCheckIt() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check Auto renewal note is displayed on Contact Info page when the user check it");
            extentReportUtils.addTestLog(Status.Info, "CheckAutoRenewNoteIsDisplayedOnContactInfoPageWhenTheUserCheckIt");
            //CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            productsPage.ClickAcceptAllCookies();

            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_ULTIMATE);

            yourOrderPage.ClickAcceptAllCookies();

            // Get product name
            string productName = HelperUtils.GetDescription(ProductsEnum.DEVCRAFT_COMPLETE);

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Check Auto Renewal Checkbox
            yourOrderPage.ClickAutomaticalyRenewalCheckbox(productName);

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            // Assert Auto Renewal Message Box is displayed
            Assert.IsTrue(contactInfoPage.IsAutoRenewalMessageDisplayed());

            // Click 'Your Account' link
            contactInfoPage.ClickYourAccountLink();

            // Accept cookies
            contactInfoPage.ClickAcceptAllCookies();

            // Check current url
            string url = CmdDriver.Driver.Url.Trim();
            Assert.IsTrue(url.Contains("https://www.telerik.com/login/v2/telerik"));
        }

        [Test]
        public void CheckAutoRenewNoteIsNotDisplayedWhenTheUserHasNotCheckIt() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check Auto renewal note is NOT displayed on Contact Info page when the user has not check it");
            extentReportUtils.addTestLog(Status.Info, "CheckAutoRenewNoteIsNotDisplayedWhenTheUserHasNotCheckIt");
            //CmdDriver.NavigateTo("https://store.progress.com/configure-purchase?skuId=6127");

            productsPage.ClickAcceptAllCookies();

            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_ULTIMATE);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            // Assert Auto Renewal Message Box is displayed
            Assert.IsFalse(contactInfoPage.IsAutoRenewalMessageDisplayed());
        }

        [Test]
        public void CheckBillingInformationWithState()
        {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);
            orderSummaryPage = new OrderSummaryPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check Billing information with state");
            extentReportUtils.addTestLog(Status.Info, "CheckBillingInformationWithState");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            ContactInfo contactInfo = new ContactInfo();

            // Fill Billing Information
            contactInfoPage.FillBillingInformation(contactInfo);

            // Assert Vat field is not displayed
            Assert.IsFalse(contactInfoPage.IsVatIdFieldDisplayed());

            // Navigate user to next page
            contactInfoPage.ClickContinueButton();

            // Assert at Order summary page
            Assert.AreEqual("Order Summary", orderSummaryPage.GetHeaderText());
        }

        [Test]
        public void CheckBillingInformationWithVat() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);
            orderSummaryPage = new OrderSummaryPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check Billing information with state");
            extentReportUtils.addTestLog(Status.Info, "CheckBillingInformationWithState");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Address.Country = "Bulgaria";
            contactInfo.Address.City = "Sofia";
            contactInfo.Address.ZipCode = "1000";
            contactInfo.VatId = "BG160026043";

            // Fill Billing Information
            contactInfoPage.FillBillingInformation(contactInfo);
            contactInfoPage.ClickContinueButton();

            // Assert at Order summary page
            Assert.AreEqual("Order Summary", orderSummaryPage.GetHeaderText());
        }

        [Test]
        public void CheckErrorMessageForVatId() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);
            orderSummaryPage = new OrderSummaryPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check Billing information with state");
            extentReportUtils.addTestLog(Status.Info, "CheckBillingInformationWithState");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Address.Country = "Bulgaria";
            contactInfo.Address.City = "Sofia";
            contactInfo.Address.ZipCode = "1000";
            contactInfo.VatId = "BG1609999999996fgh3";

            // Fill Billing Information
            contactInfoPage.FillBillingInformation(contactInfo);

            // Assert error message for Vat id
            Assert.AreEqual("Invalid VAT ID", contactInfoPage.GetErrorMessageForVatId());
        }

        [Test]
        public void CheckVatIdIsNotRequiredField() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);
            orderSummaryPage = new OrderSummaryPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check Vat id is not a required field");
            extentReportUtils.addTestLog(Status.Info, "CheckVatIdIsNotRequiredField");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Address.Country = "Bulgaria";
            contactInfo.Address.City = "Sofia";
            contactInfo.Address.ZipCode = "1000";
            contactInfo.VatId = "";

            // Fill Billing Information
            contactInfoPage.FillBillingInformation(contactInfo);

            // Assert error message for vat id is NOT displayed
            Assert.IsFalse(contactInfoPage.IsVatIdErrorMessageDisplayed());

            // Navigate user to next page
            contactInfoPage.ClickContinueButton();

            // Assert at Order summary page
            Assert.AreEqual("Order Summary", orderSummaryPage.GetHeaderText());
        }

        [Test]
        public void CheckValidationForEmail() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);
            orderSummaryPage = new OrderSummaryPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check validation for email");
            extentReportUtils.addTestLog(Status.Info, "CheckValidationForEmail");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            contactInfoPage.ClickAcceptAllCookies();

            ContactInfo contactInfo = new ContactInfo();
            contactInfo.Email = "32543543512@";

            // Fill Billing Information
            contactInfoPage.SetFirstName(contactInfo.FirstName);
            contactInfoPage.SetLastName(contactInfo.LastName);
            contactInfoPage.SetCompany(contactInfo.CompanyName);
            contactInfoPage.SetPhone(contactInfo.PhoneNumber);
            contactInfoPage.SetAddress(contactInfo.Address.Street);
            contactInfoPage.SetCountry(contactInfo.Address.Country);
            contactInfoPage.SetCity(contactInfo.Address.City);
            contactInfoPage.SetZipCode(contactInfo.Address.ZipCode);

            contactInfoPage.ClickContinueButton();

            // Assert email error message about required field
            Assert.AreEqual("Email is required", contactInfoPage.GetErrorMessageForEmail());

            // Set email with more than max lenght (150)
            string tooLongEmail = StringUtils.CreateString(151);
            contactInfoPage.SetEmail(tooLongEmail);

            // Assert error message about lenght
            Assert.AreEqual("Invalid email", contactInfoPage.GetErrorMessageForEmail());
        }

        [Test]
        public void CheckValidationForCompany() {
            productsPage = new ProductsPage(CmdDriver.Driver);
            yourOrderPage = new YourOrderPage(CmdDriver.Driver);
            contactInfoPage = new ContactInfoPage(CmdDriver.Driver);
            orderSummaryPage = new OrderSummaryPage(CmdDriver.Driver);

            extentReportUtils.createATestCase("Check validation for company");
            extentReportUtils.addTestLog(Status.Info, "CheckValidationForCompany");

            productsPage.ClickAcceptAllCookies();

            // Click on Buy Now button per Product name
            productsPage.ClickBuyNowButtonByProductName(ProductsEnum.DEVCRAFT_UI);

            //yourOrderPage.ClickAcceptAllCookies();

            // Assert there is at least one product at the page
            Assert.AreEqual(1, yourOrderPage.GetProductsCountFromPage());

            // Navigate user to the next page - Contact Info Page
            yourOrderPage.ClickContinueButton();

            contactInfoPage.ClickAcceptAllCookies();

            ContactInfo contactInfo = new ContactInfo();

            // Fill Billing Information
            contactInfoPage.SetFirstName(contactInfo.FirstName);
            contactInfoPage.SetLastName(contactInfo.LastName);
            contactInfoPage.SetEmail(contactInfo.Email);
            contactInfoPage.SetPhone(contactInfo.PhoneNumber);
            contactInfoPage.SetAddress(contactInfo.Address.Street);
            contactInfoPage.SetCountry(contactInfo.Address.Country);
            contactInfoPage.SetCity(contactInfo.Address.City);
            contactInfoPage.SetZipCode(contactInfo.Address.ZipCode);
            contactInfoPage.SetState("New Jersey");

            // Click 'Continue' button
            contactInfoPage.ClickContinueButton();

            // Assert email error message about required field
            Assert.AreEqual("Company is required", contactInfoPage.GetErrorMessageForCompany());

            // Set email with more than max lenght (60)
            string tooLongCompany = StringUtils.CreateString(61);
            contactInfoPage.SetCompany(tooLongCompany);

            // Click 'Continue' button
            contactInfoPage.ClickContinueButton();

            // Assert email error message about required field
            Assert.AreEqual("Invalid company", contactInfoPage.GetErrorMessageForCompany());
        }
    }
}
