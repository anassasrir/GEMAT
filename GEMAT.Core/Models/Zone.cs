using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Models
{
    public class Zone
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // Coordonnées relatives (pourcentage de l'écran)
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double RelativeWidth { get; set; }
        public double RelativeHeight { get; set; }

        public Coordinate Center => new Coordinate(
            X + Width / 2,
            Y + Height / 2
        );

        public Coordinate TopLeft => new Coordinate(X, Y);
        public Coordinate TopRight => new Coordinate(X + Width, Y);
        public Coordinate BottomLeft => new Coordinate(X, Y + Height);
        public Coordinate BottomRight => new Coordinate(X + Width, Y + Height);

        /// <summary>
        /// Calcule les coordonnées absolues à partir des coordonnées relatives
        /// </summary>
        public void CalculateAbsolute(int screenWidth, int screenHeight)
        {
            X = (int)(screenWidth * RelativeX);
            Y = (int)(screenHeight * RelativeY);
            Width = (int)(screenWidth * RelativeWidth);
            Height = (int)(screenHeight * RelativeHeight);
        }
    }
}
