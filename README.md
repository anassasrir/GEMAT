# GEMAT - Getting Started Guide and Overview

Welcome to the **GEMAT** (Global Environment for Mobile Automation Testing) project, **our** automation testing framework focused on using **Appium** with C#/.NET for Android and iOS mobile applications. **We** designed it to be particularly suited for Unity games using a coordinate-based interaction mode.

---

## 🚀 Quick Start

To begin working with GEMAT, follow these steps:

1.  **Environment Setup:** Install all external dependencies (Node.js, Appium, Android/iOS SDKs) by following the detailed guide:

    - 👉 **[Environment Setup (SETUP.md)](docs/SETUP.md)**

2.  **Project Configuration:** Create the .NET structure and add the necessary NuGet packages:

    - 👉 **[Development Guide (DEVELOPMENT.md)](docs/DEVELOPMENT.md)**

3.  **Run Tests:** Once the project is configured and the Appium server is running, **we** can execute the tests.

```bash
# Run the smoke tests
dotnet test --filter "Category=Smoke"
```
