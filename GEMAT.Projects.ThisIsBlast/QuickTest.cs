using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

[TestFixture]
public class QuickConnectionTest
{
    [Test]
    public void VerifyAppiumConnection()
    {
        TimeSpan commandTimeout = TimeSpan.FromSeconds(60);
        var options = new AppiumOptions();

        options.PlatformName = "Android";
        options.AutomationName = AutomationName.AndroidUIAutomator2;
        options.DeviceName = "Android Emulator";
        options.App = "C:\\Users\\asrir\\Downloads\\161.apk";

        options.AddAdditionalAppiumOption("appPackage", "com.kiragan.blastjam");
        options.AddAdditionalAppiumOption("appActivity", "com.google.firebase.MessagingUnityPlayerActivity");
        options.AddAdditionalAppiumOption("installOptions", new string[] { "--no-verify" });
        options.AddAdditionalAppiumOption("disableIdlingResource", true);

        var driver = new AndroidDriver(new Uri("http://localhost:4723"), options);

        System.Threading.Thread.Sleep(20000);

        Console.WriteLine("✓ App launched successfully!");
        Console.WriteLine($"Window size: {driver.Manage().Window.Size}");

        driver.Quit();
    }
}