using OpenQA.Selenium;
using System.IO;
using System;

namespace FrameworkC.CommonLibs.Utils
{
    class ScreenshotUtils
    {
        ITakesScreenshot camera;

        public ScreenshotUtils(IWebDriver driver)
        {
            camera = (ITakesScreenshot) driver;

        }

        public void CaptureAndSaveScreenshot(string fileName)
        {
            _ = fileName.Trim();

            if (File.Exists(fileName))
            {
                throw new Exception("File name already exist");
            }

            Screenshot screenshot = camera.GetScreenshot();
            screenshot.SaveAsFile(fileName);
        }
    }
}
