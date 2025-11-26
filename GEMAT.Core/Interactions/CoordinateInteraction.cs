using GEMAT.Core.Models;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Interactions
{
    /// <summary>
    /// Stratégie d'interaction basée sur les coordonnées
    /// Utilisée pour les jeux Unity, moteurs custom, etc.
    /// </summary>
    public class CoordinateInteraction : IInteractionStrategy
    {
        private readonly AppiumDriver _driver;

        public CoordinateInteraction(AppiumDriver driver)
        {
            _driver = driver;
        }

        public void Tap(Coordinate coordinate, int waitAfterMs = 500)
        {
            var actions = new Actions(_driver);
            actions.MoveToLocation(coordinate.X, coordinate.Y)
                   .Click()
                   .Perform();
            Thread.Sleep(waitAfterMs);
        }

        public void Swipe(Coordinate from, Coordinate to, int durationMs = 500)
        {
            var actions = new Actions(_driver);
            actions.MoveToLocation(from.X, from.Y)
                   .ClickAndHold()
                   .MoveToLocation(to.X, to.Y)
                   .Release()
                   .Perform();
            Thread.Sleep(500);
        }

        public void LongPress(Coordinate coordinate, int durationMs = 1000)
        {
            var actions = new Actions(_driver);
            actions.MoveToLocation(coordinate.X, coordinate.Y)
                   .ClickAndHold()
                   .Pause(System.TimeSpan.FromMilliseconds(durationMs))
                   .Release()
                   .Perform();
            Thread.Sleep(500);
        }

        public void WaitForAnimation(int milliseconds = 2000)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
