using GEMAT.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Interactions
{
    public interface IInteractionStrategy
    {
        void Tap(Coordinate coordinate, int waitAfterMs = 500);
        void Swipe(Coordinate from, Coordinate to, int durationMs = 500);
        void LongPress(Coordinate coordinate, int durationMs = 1000);
        void WaitForAnimation(int milliseconds = 2000);
    }
}
