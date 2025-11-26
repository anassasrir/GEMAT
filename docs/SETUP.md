# GEMAT Environment Setup

This guide details the installation and configuration of the environment necessary for **us** to execute Appium tests.

## PREREQUISITES

### 1. Install Node.js and Appium

Node.js is necessary for **us** to manage Appium 2.x and its drivers.

```bash
# Install Node.js (if not already installed)
# Download from [https://nodejs.org/](https://nodejs.org/)

# Install Appium 2.x globally
npm install -g appium

# Check installation
appium --version
```

### 2. Install Appium Drivers

We use the standard drivers for Android and iOS.

```bash
# Android Driver (UiAutomator2)
appium driver install uiautomator2

# iOS Driver (XCUITest) - macOS only
appium driver install xcuitest

# Verify installed drivers
appium driver list --installed
```

### 3. Android Configuration

#### 3.1 Install Android Studio and SDK

1. Download Android Studio : https://developer.android.com/studio
2. Install Android SDK and Platform Tools via the SDK Manager in Android Studio.
3. Configure Environment Variables:

##### Windows (PowerShell):

```powerShell
$env:ANDROID_HOME = "C:\Users\YourName\AppData\Local\Android\Sdk"
$env:PATH += ";$env:ANDROID_HOME\platform-tools;$env:ANDROID_HOME\tools;$env:ANDROID_HOME\emulator"
```

##### macOS/Linux:

```bash
export ANDROID_HOME=$HOME/Library/Android/sdk
export PATH=$PATH:$ANDROID_HOME/platform-tools:$ANDROID_HOME/tools:$ANDROID_HOME/emulator
```

#### 3.2 Create and Start an Android Emulator

Emulators (AVDs) can be managed via the Android Studio GUI or command line.

```bash
# List available AVDs
emulator -list-avds

# Create an emulator via Android Studio (Tools > Device Manager > Create Device)
# Recommendation: Pixel 6, Android 13 (API 33)

# Start the emulator from the command line
emulator -avd Pixel_6_API_33
```

#### 3.3 Verify ADB Connection

Verify that the emulator is recognized by the Android Debug Bridge (ADB).

```bash
adb devices
# Expected output (example):
# List of devices attached
# emulator-5554   device
```

### 4. iOS Configuration (macOS only)

iOS configuration requires macOS and Xcode.

#### 4.1 Install Xcode and Command Line Tools

```bash
# Download Xcode from Mac App Store
# Install Command Line Tools
xcode-select --install
```

#### 4.2 Install iOS Dependencies

```bash
# Carthage (for WebDriverAgent)
brew install carthage

# ios-deploy
npm install -g ios-deploy

# Authorize iOS testing
npm install -g authorize-ios
```

#### 4.3 Configure WebDriverAgent

WebDriverAgent is the iOS testing agent we use with Appium.

```bash
cd ~/.appium/node_modules/appium-xcuitest-driver/node_modules/appium-webdriveragent
./Scripts/bootstrap.sh
```

### 5. Obtain the APK/IPA for the game

To perform installation testing, the application binary (APK for Android, IPA for iOS) or a reference to an already installed app is needed. Below are the recommended methods for obtaining or specifying the application under test for both platforms.

The required method depends on whether you are testing the **installation process** itself or attaching to an **already installed app** for functional testing.

| Platform    | Option 1: From the Store (Extraction)                                                                                                       | Option 2: Already Installed App                                                                                               |
| :---------- | :------------------------------------------------------------------------------------------------------------------------------------------ | :---------------------------------------------------------------------------------------------------------------------------- |
| **Android** | Use **`adb pull`** or an **APK Extractor** application to save the `.apk` file. This is the **actual binary** for installation.             | Leave **`AppPath`** empty in `appsettings.json` and utilize **`appPackage`** and **`appActivity`** to target the running app. |
| **iOS**     | Use an extraction tool like **iMazing** to extract the `.ipa` file from a device or backup. This is the **actual binary** for installation. | Leave **`AppPath`** empty in `appsettings.json` and utilize the **`bundleId`** to target the running app.                     |

---

#### 📝 Configuration Notes

- **Option 1 (Extraction):** This is essential when your test scenario includes the step of **installing the application** onto a fresh device or simulator/emulator. The extracted file (`.apk` or `.ipa`) is specified in the test configuration (e.g., using an **`AppPath`** variable).
- **Option 2 (Already Installed):** This is typically used for general **functional testing** where the installation step is skipped. Instead of a file path, you provide platform-specific identifiers:
  - **Android:** Use the application's unique **Package Name** (e.g., `com.example.app`) and its **Main Activity** (e.g., `.MainActivity`).
  - **iOS:** Use the application's unique **Bundle ID** (e.g., `com.example.app.ios`).

---

## 🌐 START APPIUM SERVER

The Appium server must be running before we can execute any test.

### Method 1: Command Line (Recommended)

```bash
# Start Appium with detailed logs
appium --log-level debug

# Or on a specific port
appium --port 4723

# Expected output (example):
# [Appium] Welcome to Appium v2.x.x
# [Appium] Appium REST http interface listener started on 0.0.0.0:4723
```

### Method 2: Appium Desktop (GUI)

We can use the Appium Inspector tool to start the server locally and inspect elements: https://github.com/appium/appium-inspector/releases
