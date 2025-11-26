using System;
using System.Collections.Generic;
using System.Text;

namespace GEMAT.Core.Models
{
    /// <summary>
    /// Mapping complet de l'écran d'une application
    /// Peut être chargé depuis un fichier JSON
    /// </summary>
    public class ScreenMapping
    {
        public string ScreenName { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public Dictionary<string, Zone> Zones { get; set; } = new Dictionary<string, Zone>();
        public Dictionary<string, Coordinate> Points { get; set; } = new Dictionary<string, Coordinate>();
        public GridMapping Grid { get; set; }

        public void RecalculateForResolution(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            foreach (var zone in Zones.Values)
            {
                zone.CalculateAbsolute(width, height);
            }

            if (Grid != null)
            {
                Grid.CalculateCells(width, height);
            }
        }
    }

    public class GridMapping
    {
        public Zone GridZone { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<GridCell> Cells { get; set; }

        public void CalculateCells(int screenWidth, int screenHeight)
        {
            if (GridZone == null) return;

            GridZone.CalculateAbsolute(screenWidth, screenHeight);

            Cells = new List<GridCell>();
            var cellWidth = GridZone.Width / Columns;
            var cellHeight = GridZone.Height / Rows;

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Cells.Add(new GridCell
                    {
                        Row = row,
                        Column = col,
                        Center = new Coordinate(
                            GridZone.X + (col * cellWidth) + (cellWidth / 2),
                            GridZone.Y + (row * cellHeight) + (cellHeight / 2)
                        )
                    });
                }
            }
        }

        public GridCell GetCell(int row, int column)
        {
            return Cells?.Find(c => c.Row == row && c.Column == column);
        }
    }

    public class GridCell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Coordinate Center { get; set; }
    }
}
