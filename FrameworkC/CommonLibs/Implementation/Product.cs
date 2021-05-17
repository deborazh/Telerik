using FrameworkC.CommonLibs.Enums;
using FrameworkC.CommonLibs.Utils;
using System;

namespace FrameworkC.CommonLibs.Implementation
{
    public class Product
    {

        private ProductType productType;
        private int minLicenseQty;
        private int minYearlyQty;
        private string productName;
        private double mNsSubscriptionPrice;
        private double initialUnitPrice;
        private int licenseQuantity;
        private int licenseDiscount;
        private int yearlyQuantity;
        private int yearlyDiscount;
        private double unitPrice;
        private double expectedDiscountForLicense;
        private double discountForYear;
        private double expectedYearlyPrice;
        private double subtotalValue;
        private double renewalDiscount;
        private double expectedRenewalPrice;
        private double expectedTotalLicensesPrice;
        private double expectedMaintenancePrice;
        private double expectedTotalDiscounts;
        private double expectedTotalValue;

        public string ProductName { get => productName; set => productName = value; }
        public double MNsSubscriptionPrice { get => mNsSubscriptionPrice; set => mNsSubscriptionPrice = value; }
        public int LicenseQuantity { get => licenseQuantity; set => licenseQuantity = value; }
        public int LicenseDiscount { get => licenseDiscount; set => licenseDiscount = value; }
        public double InitialUnitPrice { get => initialUnitPrice; set => initialUnitPrice = value; }
        public double ExpectedDiscountForLicense { get => expectedDiscountForLicense; set => expectedDiscountForLicense = value; }
        public double UnitPrice { get => unitPrice; set => unitPrice = value; }
        public int YearlyQuantity { get => yearlyQuantity; set => yearlyQuantity = value; }
        public int YearlyDiscount { get => yearlyDiscount; set => yearlyDiscount = value; }
        public int MinLicenseQty { get => minLicenseQty; set => minLicenseQty = value; }
        public int MinYearlyQty { get => minYearlyQty; set => minYearlyQty = value; }
        public double DiscountForYear { get => discountForYear; set => discountForYear = value; }
        public double ExpectedYearlyPrice { get => expectedYearlyPrice; set => expectedYearlyPrice = value; }
        public double SubtotalValue { get => subtotalValue; set => subtotalValue = value; }
        public double ExpectedRenewalPrice { get => expectedRenewalPrice; set => expectedRenewalPrice = value; }
        public double ExpectedTotalLicensesPrice { get => expectedTotalLicensesPrice; set => expectedTotalLicensesPrice = value; }
        public double ExpectedMaintenancePrice { get => expectedMaintenancePrice; set => expectedMaintenancePrice = value; }
        public double ExpectedTotalDiscounts { get => expectedTotalDiscounts; set => expectedTotalDiscounts = value; }
        public double ExpectedTotalValue { get => expectedTotalValue; set => expectedTotalValue = value; }

        public Product(ProductType productType)
        {
            this.productType = productType;

            this.minLicenseQty = 1;
            this.minYearlyQty = 1;

            this.licenseDiscount = 0;
            this.yearlyDiscount = 0;
            this.expectedYearlyPrice = 0;
            this.expectedMaintenancePrice = 0;
        }

        public void CalculateValues() {
            checkProductType();
            switch (productType)
            {
                case ProductType.ONLY_LICENCE_DISCOUNT:
                    CalculateValuesForProductOnlyWithLicenseDiscount();
                    break;
                case ProductType.ONLY_YEARLY_DISCOUNT:
                    CalculateValuesForProductOnlyWithYearlyDiscount();
                    break;
                case ProductType.LICENSE_AND_YEARLY_DISCOUNT:
                    CalculateValuesForBothLicenseAndYearlyDiscounts();
                    break;
                default:
                    Console.WriteLine("Invalid Product type");
                    break;
            }
        }
        private void checkProductType()
        {
            if (this.licenseQuantity == 1 && this.yearlyQuantity>1)
            {
                this.productType = ProductType.ONLY_YEARLY_DISCOUNT;

            } else if (this.licenseQuantity > 1 && this.yearlyQuantity == 1)
            {
                this.productType = ProductType.ONLY_LICENCE_DISCOUNT;
            } else if(this.licenseQuantity > 1 && this.yearlyQuantity > 1)
            {
                this.productType = ProductType.LICENSE_AND_YEARLY_DISCOUNT;
            }
        }

        private void CalculateValuesForProductOnlyWithLicenseDiscount()
        {
            CalculateExpectedDiscountForLicense();
            CalculateExpectedUnitPrice();
            CalculateSubtotalValue();
            CalculateRenewalDiscount();
            CalculateExpectedRenewalPrice();
            CalculateTotalLicensesPrice();
            CalculateExpectedTotalDiscounts();
            CalculateExpectedTotalValue();
        }

        private void CalculateValuesForProductOnlyWithYearlyDiscount()
        {
            CalculateExpectedUnitPrice();
            CalculateDiscountForYearlyPrice();
            CalculateExpectedYearlyPrice();
            CalculateSubtotalValue();
            CalculateRenewalDiscount();
            CalculateExpectedRenewalPrice();
            CalculateTotalLicensesPrice();
            CalculateMaintenancePrice();
            CalculateExpectedTotalDiscounts();
            CalculateExpectedTotalValue();
        }

        private void CalculateValuesForBothLicenseAndYearlyDiscounts() 
        {
            CalculateExpectedDiscountForLicense();
            CalculateExpectedUnitPrice();
            CalculateDiscountForYearlyPrice();
            CalculateExpectedYearlyPrice();
            CalculateSubtotalValue(); 
            CalculateRenewalDiscount();
            CalculateExpectedRenewalPrice();
            CalculateTotalLicensesPrice();
            CalculateMaintenancePrice();
            CalculateExpectedTotalDiscounts();
            CalculateExpectedTotalValue();
        }

        public void CalculateExpectedDiscountForLicense() {
            this.expectedDiscountForLicense = HelperUtils.CalculateExpectedDiscountFromPrice(initialUnitPrice, licenseDiscount);
        }

        public void CalculateExpectedUnitPrice()
        {
            this.unitPrice = HelperUtils.CalculateExpectedPriceWithDiscount(initialUnitPrice, licenseDiscount, licenseQuantity);
        }

        public void CalculateDiscountForYearlyPrice()
        {
            double priceBeforeDiscounts = mNsSubscriptionPrice * licenseQuantity;
            double totalDiscount = HelperUtils.CalculateExpectedDiscountFromPrice(priceBeforeDiscounts, yearlyDiscount);
            this.discountForYear = (double)Math.Round(totalDiscount, 2);
        }

        public void CalculateExpectedYearlyPrice()
        {
            if (mNsSubscriptionPrice == 0)
            {
                this.expectedYearlyPrice = 0;
            }
            else
            {
                double priceBeforeDiscount = mNsSubscriptionPrice * licenseQuantity;
                double total = priceBeforeDiscount - discountForYear;
                this.expectedYearlyPrice = (double)Math.Round(total, 2);
            }
        }

        public void CalculateSubtotalValue() {
            double total = (unitPrice * licenseQuantity) + (expectedYearlyPrice * (yearlyQuantity - 1));
            this.subtotalValue = (double)Math.Round(total, 2);
        }

        public void CalculateRenewalDiscount() {
            this.renewalDiscount = HelperUtils.CalculateExpectedDiscountFromPrice(mNsSubscriptionPrice, licenseDiscount);
        }

        public void CalculateExpectedRenewalPrice() {
            double total = (mNsSubscriptionPrice - renewalDiscount) * licenseQuantity; ;
            this.expectedRenewalPrice = (double)Math.Round(total, 2);        
        }

        public void CalculateTotalLicensesPrice() {
            double total = initialUnitPrice * licenseQuantity;
            this.expectedTotalLicensesPrice = (double)Math.Round(total, 2);
        }

        public void CalculateMaintenancePrice() {
            double total = mNsSubscriptionPrice * (yearlyQuantity - 1) * licenseQuantity;
            this.expectedMaintenancePrice = (double)Math.Round(total, 2);
        }

        public void CalculateExpectedTotalDiscounts()
        {
            double total = (expectedDiscountForLicense * licenseQuantity) + (discountForYear * (yearlyQuantity - 1));
            this.expectedTotalDiscounts = (double)Math.Round(total, 2);
        }

        public void CalculateExpectedTotalValue() { 
            this.expectedTotalValue = expectedTotalLicensesPrice + expectedMaintenancePrice - expectedTotalDiscounts;
        }
        
    }
}
