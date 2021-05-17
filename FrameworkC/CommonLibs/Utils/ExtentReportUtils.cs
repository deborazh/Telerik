using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;

namespace FrameworkC.CommonLibs.Utils
{
    class ExtentReportUtils
    {
        private ExtentHtmlReporter extentHtmlReporter;

        private ExtentReports extentReports;

        private ExtentTest extentTest;

        public ExtentReportUtils(string htmlReportFileName)
        {
            extentHtmlReporter = new ExtentHtmlReporter(htmlReportFileName);
            extentReports = new ExtentReports();
            extentReports.AttachReporter(extentHtmlReporter);
        }

        public void createATestCase(string testCaseName)
        {
            extentTest = extentReports.CreateTest(testCaseName);
        }

        public void addTestLog(Status status, string comment)
        {
            extentTest.Log(status, comment);
        }

        public void addScreenshot(string screenshotFileName)
        {
            //screenshotFileName = screenshotFileName.Replace("/", "\\");
            Assert.That(File.Exists(screenshotFileName));
            extentTest.AddScreenCaptureFromPath(screenshotFileName);
        }

        public void flushReport()
        {
            extentReports.Flush();
        }

        internal void addTestLog(Status fail)
        {
            throw new NotImplementedException();
        }
    }
}
