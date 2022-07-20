

using ModelSMP.CoordsSupportEntities;

namespace ModelSMP.TilesLogicHandler
{
    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Square { get { return GeneralSettings.Settings.TileSquare; } }
        public int Cost { get; init; }
        public int Distance { get; private set; }
        public int CostDistance { get { return Cost + Distance; } }
        public Tile? Parent { get; init; }

        public static Tile? GetTileCoords(Point point)
        {
            if (point.X > 0 && point.Y > 0)
                return new Tile() { X = (int)point.X / GeneralSettings.Settings.TileSquare, Y = (int)point.Y / GeneralSettings.Settings.TileSquare };
            return null;
        }

        public void SetDistance(int targetX, int targetY)
        {
            Distance = Math.Abs(targetX - X) * Square + Math.Abs(targetY - Y) * Square;
        }

    }
}
