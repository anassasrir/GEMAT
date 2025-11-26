using GEMAT.Core.Configuration;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Driver
{
    /// <summary>
    /// Gestionnaire centralisé du driver avec support ThreadStatic
    /// pour l'exécution parallèle
    /// </summary>
    public class DriverManager
    {
        [ThreadStatic]
        private static AppiumDriver _driver;

        private static TestConfig _testConfig;

        public static void Initialize(
            PlatformConfig platformConfig,
            AppConfig appConfig,
            TestConfig testConfig)
        {
            _testConfig = testConfig;
            _driver = DriverFactory.CreateDriver(platformConfig, appConfig);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(testConfig.ImplicitWait);
        }

        public static AppiumDriver GetDriver()
        {
            if (_driver == null)
                throw new InvalidOperationException("Driver not initialized. Call Initialize() first.");
            return _driver;
        }

        public static void QuitDriver()
        {
            _driver?.Quit();
            _driver = null;
        }

        public static TestConfig GetTestConfig() => _testConfig;
    }
}
