using GEMAT.Core.Driver;
using GEMAT.Core.Interactions;
using GEMAT.Core.Models;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Pages
{
    public abstract class BasePage
    {
        protected AppiumDriver Driver;
        protected IInteractionStrategy Interaction;
        protected ScreenMapping ScreenMapping;

        protected BasePage(AppiumDriver driver, IInteractionStrategy interaction)
        {
            Driver = driver;
            Interaction = interaction;
        }

        protected BasePage(AppiumDriver driver, IInteractionStrategy interaction, ScreenMapping screenMapping)
            : this(driver, interaction)
        {
            ScreenMapping = screenMapping;
            InitializeScreenMapping();
        }

        private void InitializeScreenMapping()
        {
            if (ScreenMapping != null)
            {
                var size = Driver.Manage().Window.Size;
                ScreenMapping.RecalculateForResolution(size.Width, size.Height);
            }
        }

        public abstract bool IsDisplayed();

        protected void TapZone(string zoneName, int waitAfterMs = 500)
        {
            if (ScreenMapping?.Zones.ContainsKey(zoneName) == true)
            {
                var zone = ScreenMapping.Zones[zoneName];
                Interaction.Tap(zone.Center, waitAfterMs);
            }
        }

        protected void TapPoint(string pointName, int waitAfterMs = 500)
        {
            if (ScreenMapping?.Points.ContainsKey(pointName) == true)
            {
                var point = ScreenMapping.Points[pointName];
                Interaction.Tap(point, waitAfterMs);
            }
        }

        protected void TakeScreenshot(string fileName)
        {
            var config = DriverManager.GetTestConfig();
            var path = Path.Combine(config.ScreenshotPath, $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            Directory.CreateDirectory(config.ScreenshotPath);

            var screenshot = Driver.GetScreenshot();
            screenshot.SaveAsFile(path);
        }
    }
}
