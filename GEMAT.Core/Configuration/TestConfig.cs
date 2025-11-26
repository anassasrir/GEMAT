using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Configuration
{
    public class TestConfig
    {
        public int ImplicitWait { get; set; } = 10;
        public int ExplicitWait { get; set; } = 30;
        public bool ScreenshotOnFailure { get; set; } = true;
        public bool VideoRecording { get; set; } = false;
        public int RetryCount { get; set; } = 1;
        public string ScreenshotPath { get; set; } = "Screenshots";
        public string ReportPath { get; set; } = "Reports";
    }
}
