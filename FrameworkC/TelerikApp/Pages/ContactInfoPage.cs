using FrameworkC.CommonLibs.Implementation;
using FrameworkC.CommonLibs.Utils;
using OpenQA.Selenium;

namespace FrameworkC.TelerikApp.Pages
{
    public class ContactInfoPage : BasePage
    {

        private IWebElement AutoRenewalMessageBox => driver.FindElement(By.CssSelector("div.auto-renewal-alert-panel"));
        private IWebElement YourAccountLink => driver.FindElement(By.CssSelector("a.u-fs13:nth-child(1)"));
        private IWebElement ContactInfoHeader => driver.FindElement(By.XPath("//h3[contains(@class, 'title')]/span[normalize-space(text()) = 'Contact Info']"));
        private IWebElement FirstNameField => driver.FindElement(By.CssSelector("#biFirstName"));
        private IWebElement LastNameField => driver.FindElement(By.CssSelector("#biLastName"));
        private IWebElement EmailField => driver.FindElement(By.CssSelector("#biEmail"));
        private IWebElement CompanyField => driver.FindElement(By.CssSelector("#biCompany"));
        private IWebElement PhoneField => driver.FindElement(By.CssSelector("#biPhone"));
        private IWebElement AddressField => driver.FindElement(By.CssSelector("#biAddress"));
        private IWebElement CityField => driver.FindElement(By.CssSelector("#biCity"));
        private IWebElement ZipCodeField => driver.FindElement(By.CssSelector("#biZipCode"));
        private IWebElement CountryDropdown => driver.FindElement(By.CssSelector("#biCountry span>span"));
        private IWebElement StateDropdown => driver.FindElement(By.CssSelector("#biState span>span"));
        private IWebElement ContinueButton => driver.FindElement(By.CssSelector("button.e2e-continue"));
        private IWebElement VatIdField => driver.FindElement(By.CssSelector("#biVatId"));
        private IWebElement ErrorMessageForVatId => driver.FindElement(By.XPath("//input[@id='biVatId']/parent::div//span"));
        private IWebElement ErrorMessageForEmail => driver.FindElement(By.XPath("//input[@id='biEmail']/parent::div//span"));
        private IWebElement ErrorMessageForCompany => driver.FindElement(By.XPath("//input[@id='biCompany']/parent::div//span"));
        private By AutoRenewalMessageBoxLocator => By.CssSelector("div.auto-renewal-alert-panel");

        private string BIPageDropdownValuesLocator = "//kendo-list//ul//li[normalize-space()='foo']";
        private By VatIdLocator => By.CssSelector("#biVatId");
        private By VatIdErrorMessageLocator => By.XPath("//input[@id='biVatId']/parent::div//span");
        private By VatIdLoader => By.CssSelector("div.row.loader-container:not(.is-loading)");

        

        public ContactInfoPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public bool IsAutoRenewalMessageDisplayed() {
            return cmnElement.isElementDisplayedByLocator(driver, AutoRenewalMessageBoxLocator);
        }

        public bool IsVatIdFieldDisplayed() {
            return cmnElement.isElementDisplayedByLocator(driver, VatIdLocator);
        }

        public bool IsVatIdErrorMessageDisplayed() {
            return cmnElement.isElementDisplayedByLocator(driver, VatIdErrorMessageLocator);
        }


        public string GetErrorMessageForVatId() {
            return cmnElement.GetText(ErrorMessageForVatId);
        }

        public string GetErrorMessageForEmail() {
            return cmnElement.GetText(ErrorMessageForEmail);
        }

        public string GetErrorMessageForCompany() {
            return cmnElement.GetText(ErrorMessageForCompany);
        }

        public void ClickYourAccountLink() {
            cmnElement.ClickElement(YourAccountLink);
        }

        public void ClickContinueButton() {
            cmnElement.ClickElement(ContinueButton);
            WaitUtils.WaitForPageToLoad(driver);
        }

        public void SetFirstName(string firstName) {
            cmnElement.ClearTextAndSetText(FirstNameField, firstName);
        }

        public void SetLastName(string lastName) {
            cmnElement.ClearTextAndSetText(LastNameField, lastName);
        }

        public void SetEmail(string email) {
            cmnElement.ClearTextAndSetText(EmailField, email);
        }

        public void SetCompany(string companyName) {
            cmnElement.ClearTextAndSetText(CompanyField, companyName);
        }

        public void SetPhone(string phoneNumber) {
            cmnElement.ClearTextAndSetText(PhoneField, phoneNumber);
        }

        public void SetAddress(string address) {
            cmnElement.ClearTextAndSetText(AddressField, address);
        }

        public void SetCountry(string country) {
            cmnElement.ClickElement(CountryDropdown);
            SetCountryAsString(CountryDropdown, country, BIPageDropdownValuesLocator);
        }

        public void SetCity(string city) {
            cmnElement.ClearTextAndSetText(CityField, city);
        }

        public void SetZipCode(string zipCode) {
            cmnElement.ClearTextAndSetText(ZipCodeField, zipCode);
        }

        public void SetVatId(string vatId) {
            cmnElement.ClearTextAndSetText(VatIdField, vatId);
            CheckVatId();
        }

        public void SetCountryAsString(IWebElement element, string item, string locator)
        {
            KendoDropdown kendoDropdown = new KendoDropdown(element);

            kendoDropdown.SelectCountryItem(locator, item);
        }

        public void SetState(string state)
        {
            cmnElement.ClickElement(StateDropdown);
            SetCountryAsString(StateDropdown, state, BIPageDropdownValuesLocator);
        }

        public void FillBillingInformation(ContactInfo contactInfo)
        {
            SetFirstName(contactInfo.FirstName);
            SetLastName(contactInfo.LastName);
            SetEmail(contactInfo.Email);
            SetCompany(contactInfo.CompanyName);
            SetPhone(contactInfo.PhoneNumber);
            SetAddress(contactInfo.Address.Street);
            SetCountry(contactInfo.Address.Country);
            SetCity(contactInfo.Address.City);
            SetZipCode(contactInfo.Address.ZipCode);

            if (contactInfo.Address.Country == "United States") {
                SetState(contactInfo.Address.State);
            } else if (contactInfo.Address.Country == "Bulgaria") {
                SetVatId(contactInfo.VatId);
            }
        }

        public void CheckVatId() {
            WaitUtils.WaitForElementPresentBy(driver, VatIdLoader);
        }
    }
}
