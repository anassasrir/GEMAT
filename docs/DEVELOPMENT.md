# GEMAT Development Guide

This guide covers setting up the .NET project and the commands for running **our** tests.

## 🛠️ CONFIGURE THE GEMAT PROJECT

These commands create the .NET solution structure and add the necessary dependencies **we** will need.

### 1. Create Project Structure

**We** use a structure with a Core (class library) and a Test project (NUnit).

```bash
# Create the solution
dotnet new sln -n GEMAT

# Create the Core project
dotnet new classlib -n GEMAT.Core
dotnet sln add GEMAT.Core/GEMAT.Core.csproj

# Create the Test project (for your game)
dotnet new nunit -n GEMAT.Projects.Game
dotnet sln add GEMAT.Projects.Game/GEMAT.Projects.Game.csproj

# Reference Core from Game
cd GEMAT.Projects.Game
dotnet add reference ../GEMAT.Core/GEMAT.Core.csproj
cd .. # Return to root (Solution)
```

---

### 2. Add NuGet Packages

We need to add the required packages for Appium, Selenium, and JSON configuration.

```bash
# ---- In GEMAT.Core ----
cd GEMAT.Core
dotnet add package Appium.WebDriver --version 5.0.0-rc.1
dotnet add package Selenium.WebDriver --version 4.15.0
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json

# ---- In GEMAT.Projects.Game ----
cd ../GEMAT.Projects.Game
dotnet add package NUnit --version 3.14.0
dotnet add package NUnit3TestAdapter --version 4.5.0
dotnet add package Microsoft.NET.Test.Sdk --version 17.8.0
dotnet add package Newtonsoft.Json --version 13.0.3
cd .. # Return to root (Solution)
```

### 3. Configure appsettings.json

We must create and modify the file GEMAT.Projects.Game/appsettings.json with the target application settings.

#### For Android (with APK)

```json
{
  "AppConfig": {
    "AppPath": "C:/Path/To/Game.apk",
    "AppPackage": "com.scopely.blast",
    "AppActivity": "com.unity3d.player.UnityPlayerActivity",
    "InteractionMode": "Coordinate"
  }
}
```

#### For iOS

```json
{
  "AppConfig": {
    "AppPath": "/Path/To/Game.ipa",
    "BundleId": "com.scopely.blast",
    "InteractionMode": "Coordinate"
  }
}
```

## 🚀 RUNNING TESTS

We must ensure the Appium server is running and an emulator/simulator is started before executing tests.

### 1. Quick Connection Verification

We can create a QuickTest.cs file to verify that Appium and the driver are functioning correctly.

```C#
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android; // Or .iOS for iOS
using System;

[TestFixture]
public class QuickConnectionTest
{
    [Test]
    public void VerifyAppiumConnection()
    {
        var options = new AppiumOptions();
        options.AddAdditionalAppiumOption("platformName", "Android");
        options.AddAdditionalAppiumOption("automationName", "UiAutomator2");
        options.AddAdditionalAppiumOption("deviceName", "Android Emulator");
        options.AddAdditionalAppiumOption("appPackage", "com.scopely.blast");
        options.AddAdditionalAppiumOption("appActivity", "com.unity3d.player.UnityPlayerActivity");

        var driver = new AndroidDriver(new Uri("http://localhost:4723"), options);

        System.Threading.Thread.Sleep(10000); // Wait for app loading

        Console.WriteLine("✓ App launched successfully!");
        Console.WriteLine($"Window size: {driver.Manage().Window.Size}");

        driver.Quit();
    }
}
```

Execute the check:

```bash
dotnet test --filter "FullyQualifiedName~QuickConnectionTest"
```

### 2. Test Execution Commands

```bash
# 1. Build the project
dotnet build

# 2. Run all tests in the active test project
dotnet test

# Run only tests marked with the "Smoke" Category
dotnet test --filter "Category=Smoke"

# Run a specific test
dotnet test --filter "FullyQualifiedName~Test_01_AppLaunches"

# Run on iOS (passing the parameter via NUnit)
dotnet test --filter "Category=Smoke" -- TestRunParameters.Parameter(name=\"Platform\",value=\"iOS\")
```

## ➡️ NEXT STEPS

1. Calibrate Coordinates: We should use Appium Inspector to find the exact X, Y positions for touch actions (essential for Unity games).
2. Adjust Timeouts: We may need to modify Timeouts in configurations to suit the speed of our device/emulator.
3. Create Your Own Tests: We can develop new tests by inheriting from BasePage or a common initialization class.
4. Add Image Recognition: We can implement image recognition (computer vision) verification mechanisms to detect game states.
