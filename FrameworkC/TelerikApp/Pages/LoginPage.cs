using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkC.TelerikApp.Pages
{
    class LoginPage
    {
        private IWebDriver driver;

        private IWebElement username => driver.FindElement(By.Id(""));
        private IWebElement password;
        private IWebElement loginButton;

    }
}
