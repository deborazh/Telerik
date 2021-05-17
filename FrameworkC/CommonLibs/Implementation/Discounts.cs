using FrameworkC.CommonLibs.Enums;
using FrameworkC.TelerikApp.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FrameworkC.CommonLibs.Implementation
{
    public class Discounts
    {

        private Dictionary<int, int> discounts;
        private DiscountType type;

        public Discounts(DiscountType type) {
            discounts = new Dictionary<int, int>();
            this.type = type;
        }

        public void ReadDiscountsFromList(ReadOnlyCollection<IWebElement> list, int quantityCount) {

            switch (type)
            {
                case DiscountType.LICENSE:
                    ReadDiscountsFromListForLicenseType(list, quantityCount);
                    break;
                case DiscountType.YEARLY:
                    ReadDiscountsFromListForYearlyType(list, quantityCount);
                    break;
                default:
                    Console.WriteLine("Invalid discount type");
                    break;
            }
        }

        public void ReadDiscountsFromListForLicenseType(ReadOnlyCollection<IWebElement> list, int quantityCount) {
            string discountValue;

            for (int i = 0; i < list.Count; i++)
            {
                ReadOnlyCollection<IWebElement> webElements = list[i].FindElements(By.CssSelector("span"));
                string range = webElements[0].Text;
                discountValue = webElements[1].Text;
                bool isLastElement = list.Count == i + 1;
                SetDiscountsForLicenseType(range, discountValue, quantityCount, isLastElement);
            }
        }

        public void ReadDiscountsFromListForYearlyType(ReadOnlyCollection<IWebElement> list, int quantityCount) {
            string discountValue;

            for (int i = 0; i < list.Count; i++)
            {
                ReadOnlyCollection<IWebElement> webElements = list[i].FindElements(By.CssSelector("span:not(.u-ln26)"));
                string range = webElements[0].Text;
                discountValue = webElements[1].Text;
                bool isLastElement = list.Count == i + 1;
                SetDiscountsForYearlyType(range, discountValue, quantityCount, isLastElement);
            }
        }

        public void SetDiscountsForLicenseType(string range, string discountValue, int count, bool isLastAvailableDiscount) {
            int min;
            int max;
            int discount;

            string removeTextFromDiscount = Regex.Replace(discountValue, @"[+%A-z ]+", "");
            discount = int.Parse(removeTextFromDiscount);

            string removeTextFromRange = Regex.Replace(range, @"[+A-z ]+", "");

            string[] rangeArray = removeTextFromRange.Split('-');
            min = int.Parse(rangeArray[0]);
            max = int.Parse(rangeArray[1]);

            for (int i = min; i <= max; i++)
            {
                discounts.Add(i, discount);
            }

            if (isLastAvailableDiscount)
            {
                SetRestOfDiscounts(discount, count);
            }
        }

        public void SetDiscountsForYearlyType(string period, string discountValue, int count, bool isLastAvailableDiscount) {
            int year;
            int discount;

            string removeTextFromRange = Regex.Replace(period, @"[+A-z ]+", "");

            if (discountValue.Contains("Included"))
            {
                discounts.Add(0, 0);
            }
            else {
                string removeTextFromDiscount = Regex.Replace(discountValue, @"[-+%A-z ]+", "");
                discount = int.Parse(removeTextFromDiscount);
                year = int.Parse(removeTextFromRange);

                discounts.Add(year, discount);

                if (isLastAvailableDiscount)
                {
                    SetRestOfDiscounts(discount, count);
                }
            }
        }

        //public void SetDiscounts1(string range, string discountValue, int count, bool isLastAvailableDiscount) {

        //    int min;
        //    int max;
        //    int discount;

            

        //    string removeTextFromRange = Regex.Replace(range, @"[+A-z ]+", "");

        //    if (discountValue.Contains("Included")) {
        //        discounts.Add(0, 0);
        //    }

        //    string removeTextFromDiscount = Regex.Replace(discountValue, @"[+%A-z ]+", "");
        //    discount = int.Parse(removeTextFromDiscount);

        //    if (!removeTextFromRange.Contains("-") && !discountValue.Contains("Included"))
        //    {
        //        min = int.Parse(removeTextFromRange);
        //        max = min;
        //    }
        //    else {
        //        string[] rangeArray = removeTextFromRange.Split('-');
        //        min = int.Parse(rangeArray[0]);
        //        max = int.Parse(rangeArray[1]);
        //    }

        //    //string removeTextFromDiscount = Regex.Replace(discountValue, @"[+%A-z ]+", "");
        //    //discount = int.Parse(removeTextFromDiscount);

        //    for (int i = min; i <= max; i++)
        //    {
        //        discounts.Add(i, discount);
        //    }

        //    if (isLastAvailableDiscount)
        //    {
        //        SetRestOfDiscounts(discount, count);
        //    }
        //}

        public int GetDiscountByKey(int number) {
            return discounts[number];
        }

        public void SetRestOfDiscounts(int discount, int count) {
            if (!discounts.ContainsKey(1)) {
                discounts.Add(1, 0);
            }
            
            if (discounts.Count != count)
            {
                for (int i = discounts.Count + 1; i <= count; i++)
                {
                    discounts.Add(i, discount);
                }
            }
        }

        public int GetDiscount(int value) {
           return discounts.ElementAt(value).Value;
        }
    }
}
