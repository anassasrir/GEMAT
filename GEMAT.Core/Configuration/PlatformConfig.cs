using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Configuration
{
    public class PlatformConfig
    {
        public string PlatformName { get; set; }
        public string AutomationName { get; set; }
        public string DeviceName { get; set; }
        public string PlatformVersion { get; set; }
        public string UdId { get; set; }
        public bool NoReset { get; set; }
        public bool FullReset { get; set; }
        public int NewCommandTimeout { get; set; }
        public string AppiumServerUrl { get; set; } = "http://localhost:4723";
    }
}
