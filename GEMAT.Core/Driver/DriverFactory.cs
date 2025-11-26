using GEMAT.Core.Configuration;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Driver
{
    public class DriverFactory
    {
        public static AppiumDriver CreateDriver(
            PlatformConfig platformConfig,
            AppConfig appConfig)
        {
            var appiumUri = new Uri(platformConfig.AppiumServerUrl);
            var options = BuildOptions(platformConfig, appConfig);

            AppiumDriver driver = platformConfig.PlatformName.ToLower() == "android"
                ? new AndroidDriver(appiumUri, options)
                : new IOSDriver(appiumUri, options);

            return driver;
        }

        private static AppiumOptions BuildOptions(
            PlatformConfig platformConfig,
            AppConfig appConfig)
        {
            var options = new AppiumOptions();

            // Capabilities communes
            options.AddAdditionalAppiumOption("platformName", platformConfig.PlatformName);
            options.AddAdditionalAppiumOption("automationName", platformConfig.AutomationName);
            options.AddAdditionalAppiumOption("deviceName", platformConfig.DeviceName);
            options.AddAdditionalAppiumOption("noReset", platformConfig.NoReset);
            options.AddAdditionalAppiumOption("fullReset", platformConfig.FullReset);
            options.AddAdditionalAppiumOption("newCommandTimeout", platformConfig.NewCommandTimeout);

            if (!string.IsNullOrEmpty(appConfig.AppPath))
            {
                options.AddAdditionalAppiumOption("app", appConfig.AppPath);
            }

            // Capabilities spécifiques Android
            if (platformConfig.PlatformName.ToLower() == "android")
            {
                if (!string.IsNullOrEmpty(appConfig.AppPackage))
                    options.AddAdditionalAppiumOption("appPackage", appConfig.AppPackage);
                if (!string.IsNullOrEmpty(appConfig.AppActivity))
                    options.AddAdditionalAppiumOption("appActivity", appConfig.AppActivity);
            }
            // Capabilities spécifiques iOS
            else
            {
                if (!string.IsNullOrEmpty(platformConfig.PlatformVersion))
                    options.AddAdditionalAppiumOption("platformVersion", platformConfig.PlatformVersion);
                if (!string.IsNullOrEmpty(appConfig.BundleId))
                    options.AddAdditionalAppiumOption("bundleId", appConfig.BundleId);
                if (!string.IsNullOrEmpty(platformConfig.UdId))
                    options.AddAdditionalAppiumOption("udid", platformConfig.UdId);
            }

            // Capabilities custom
            if (appConfig.CustomCapabilities != null)
            {
                foreach (var capability in appConfig.CustomCapabilities)
                {
                    options.AddAdditionalAppiumOption(capability.Key, capability.Value);
                }
            }

            return options;
        }
    }
}
