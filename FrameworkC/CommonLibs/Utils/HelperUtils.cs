using Bogus;
using FrameworkC.CommonLibs.Implementation;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FrameworkC.CommonLibs.Utils
{
    public static class HelperUtils
    {
        public static int GetRandomValue(int maxNumber) {
            return GetRandomValue(1, maxNumber);
        }

        public static int GetRandomValue(int min, int max) {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public static string GetDescription(this Enum e)
        {
            var attribute =
                e.GetType()
                    .GetTypeInfo()
                    .GetMember(e.ToString())
                    .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault()
                    as DescriptionAttribute;

            return attribute?.Description ?? e.ToString();
        }

        public static void ScrollToElement(IWebElement toElement, IWebDriver driver)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(toElement);
            actions.Perform();
            toElement.Click();
        }

        public static double CalculateExpectedDiscountFromPrice(double price, int discount)
        {
            double discountValue = (Double)discount / 100;
            double total = price * discountValue;
            return (double)Math.Round(total, 2);
        }

        public static double CalculateExpectedPriceWithDiscount(double price, int discount, int quantity)
        {
            double discountValue = CalculateExpectedDiscountFromPrice(price, discount);
            double total = price - discountValue;
            return (double)Math.Round(total, 2);
        }

        //public static Faker<ContactInfo> GenerateUserInfo() {
        //public static Faker<User> GenerateUserInfo() {

        ////var testUsers = new Faker<ContactInfo>()
        //var testUsers = new Faker<User>()
        ////Optional: Call for objects that have complex initialization
        ////.CustomInstantiator(f => new User(userIds++, f.Random.Replace("###-##-####")))

        ////Basic rules using built-in generators
        //.RuleFor(u => u.FirstName, f => f.Name.FirstName())
        //.RuleFor(u => u.LastName, f => f.Name.LastName())
        //.RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
        //.RuleFor(u => u.PhoneNumber, (f, u) => f.Phone.PhoneNumber())
        //.RuleFor(u => u.CompanyName, (f, u) => f.Company.CompanyName());

        //    return testUsers;
        //}     
    }
}
