# GEMAT Troubleshooting Guide

This document lists common errors during Appium configuration and test execution, along with their corresponding solutions. **We** use this as a quick reference guide.

## 🔴 Connection and Device Issues

### Problem: "Could not find a connected Android device"

Appium cannot see the emulator or physical device.

| Possible Cause                     | Solution                                                                                                        | Verification Command                          |
| :--------------------------------- | :-------------------------------------------------------------------------------------------------------------- | :-------------------------------------------- |
| The emulator is not running.       | **Start the emulator** (e.g., `emulator -avd Pixel_6_API_33`).                                                  | `adb devices`                                 |
| ADB is stuck.                      | **Restart the ADB server.**                                                                                     | `adb kill-server` then `adb start-server`     |
| Environment variables are not set. | **Verify** that `ANDROID_HOME` and the `$PATH` are correctly configured (`platform-tools` must be in the path). | `echo $ANDROID_HOME` or `echo %ANDROID_HOME%` |

### Problem: "Could not start a new session"

The C# client sent the request, but Appium failed to initialize the session.

1.  **Check the Appium Server:** Ensure the Appium server is **running** in a separate terminal window (`appium --log-level debug`).
2.  **Check the Port:** The C# client must point to the same port Appium is listening on (default: `http://localhost:4723`).
3.  **Check Appium Logs:** **We** must read the **Appium console log** for the exact error that prevented the session from starting (e.g., missing Capability).

## 📱 Application Issues

### Problem: "App never started" or "Original error: Activity not started"

The application does not launch or Appium cannot find the entry point.

1.  **Verify `appPackage` and `appActivity` (Android):** These values are very case-sensitive and must be exact.

    - **How to check the correct values:**

      ```bash
      # On the emulator or device, open the application
      adb shell dumpsys window | findstr mCurrentFocus
      # or on macOS/Linux:
      adb shell dumpsys window | grep mCurrentFocus

      # The result will show the current {package}/{activity} pair.
      ```

2.  **Check APK/IPA Installation:** If **we** are using the `AppPath` field, ensure the path is correct and that the Appium Server has access permissions to the file.

## 📐 Unity Game Specific Issues

### Problem: "Cannot find elements"

Appium cannot locate standard elements (buttons, text) in our game.

- **Cause:** This is **normal** for games built with engines like **Unity** or **Unreal**. These engines render the user interface as a single graphic surface, making elements inaccessible to standard Appium localization tools (XPath, ID, Accessibility ID).
- **Solution:**
  - GEMAT uses the **`"InteractionMode": "Coordinate"`** mode (defined in `appsettings.json`).
  - **We** must use **Appium Inspector** to get the precise **X, Y coordinates** of buttons and elements to interact with them via **tap (touch)** actions. This is the standard approach for game automation.
