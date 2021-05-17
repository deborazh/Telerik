using AventStack.ExtentReports;
using FrameworkC.CommonLibs.Implementation;
using FrameworkC.CommonLibs.Utils;
using FrameworkC.TelerikApp.Pages;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.IO;

namespace FrameworkC.TelerikTests.tests
{
    class BaseTests
    {
        public CommonDriver CmdDriver;
        private string url;

        private IConfigurationRoot configuration;
        string currentProjectDirectory;
        string reportFileName;

        public ExtentReportUtils extentReportUtils;
        ScreenshotUtils screenshot;

        [OneTimeSetUp]
        public void PreSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            currentProjectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            reportFileName = currentProjectDirectory + "/reports/telerikTestReport.html";
            extentReportUtils = new ExtentReportUtils(reportFileName);
            configuration = new ConfigurationBuilder().AddJsonFile(currentProjectDirectory + "/config/appSettings.json").Build();
        }

        [SetUp]
        public void Setup()
        {
            extentReportUtils.createATestCase("Setup");
            string browserType = configuration["browserType"];
            url = configuration["baseUrl"];

            extentReportUtils.addTestLog(Status.Info, "Browser Type - " + browserType);
            extentReportUtils.addTestLog(Status.Info, "Base Url - " + url);
           
            CmdDriver = new CommonDriver(browserType);
            CmdDriver.NavigateTo(url);

            screenshot = new ScreenshotUtils(CmdDriver.Driver);
        }

        [TearDown]
        public void TearDown()
        {
            string currentExecutionTime = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'-'mm'-'ss");
            string screenshotFileName = $"{currentProjectDirectory}/screenshots/test-{currentExecutionTime}.jpeg";

            if(TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                extentReportUtils.addTestLog(Status.Fail, "One or more step failed");
                screenshot.CaptureAndSaveScreenshot(screenshotFileName);
                extentReportUtils.addScreenshot(screenshotFileName);
            }
            CmdDriver.CloseAllBrowser();
        }

        [OneTimeTearDown]
        public void PostCleanUp()
        {
            extentReportUtils.flushReport();
        }
    }
}
