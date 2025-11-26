using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Configuration
{
    public class AppConfig
    {
        public string ProjectName { get; set; }
        public string AppPackage { get; set; }  // Android
        public string AppActivity { get; set; } // Android
        public string BundleId { get; set; }    // iOS
        public string AppPath { get; set; }
        public InteractionMode InteractionMode { get; set; }
        public Dictionary<string, string> CustomCapabilities { get; set; }
    }

    public enum InteractionMode
    {
        Native,        // Utilise les sélecteurs Appium standard
        Coordinate,    // Utilise les coordonnées
        Hybrid         // Mix des deux
    }
}
