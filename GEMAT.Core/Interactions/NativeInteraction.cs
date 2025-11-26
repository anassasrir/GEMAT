using GEMAT.Core.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Interactions
{
    /// <summary>
    /// Stratégie d'interaction utilisant les éléments natifs Appium
    /// Utilisée pour les apps natives standard
    /// </summary>
    public class NativeInteraction : IInteractionStrategy
    {
        private readonly AppiumDriver _driver;

        public NativeInteraction(AppiumDriver driver)
        {
            _driver = driver;
        }

        public void Tap(Coordinate coordinate, int waitAfterMs = 500)
        {
            // Fallback sur coordonnées si pas d'élément trouvé
            var coordInteraction = new CoordinateInteraction(_driver);
            coordInteraction.Tap(coordinate, waitAfterMs);
        }

        public void TapElement(By locator, int waitAfterMs = 500)
        {
            var element = _driver.FindElement(locator);
            element.Click();
            Thread.Sleep(waitAfterMs);
        }

        public void Swipe(Coordinate from, Coordinate to, int durationMs = 500)
        {
            var coordInteraction = new CoordinateInteraction(_driver);
            coordInteraction.Swipe(from, to, durationMs);
        }

        public void LongPress(Coordinate coordinate, int durationMs = 1000)
        {
            var coordInteraction = new CoordinateInteraction(_driver);
            coordInteraction.LongPress(coordinate, durationMs);
        }

        public void WaitForAnimation(int milliseconds = 2000)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
