using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace FrameworkC.CommonLibs.Implementation
{
    class CommonDriver
    {
        public IWebDriver Driver { get => driver; private set => driver = value; }
        public int PageLoadTimeout { get => pageLoadTimeout; set => pageLoadTimeout = value; }
        public int ElementDetectionTimeout { private get => elementDetectionTimeout; set => elementDetectionTimeout = value; }

        private int pageLoadTimeout;
        private int elementDetectionTimeout;
        private IWebDriver driver;

        public CommonDriver(string browserType)
        {
            pageLoadTimeout = 60;
            elementDetectionTimeout = 15;

            browserType = browserType.Trim();

            if (browserType.Equals("chrome"))
            {
                Driver = new ChromeDriver();
            }
            else if (browserType.Equals("edge"))
            {
                Driver = new EdgeDriver();
            }
            else
            {
                throw new Exception("Invalid Browser Type " + browserType);
            }
        }

        public void NavigateTo(string url)
        {
            url = url.Trim();

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(elementDetectionTimeout);

            Driver.Url = url;
        }

        public void CloseBrowser()
        {
            if (Driver != null)
            {
                Driver.Close();
            }
        }

        public void CloseAllBrowser()
        {
            if (Driver != null)
            {
                Driver.Quit();
            }
        }
    }
}
