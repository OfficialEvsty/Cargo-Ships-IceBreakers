

using System.Text;

namespace ModelSMP.TilesLogicHandler
{
    class Field
    {
        public List<string> Map;
        public static Field? m_field;
        private Field()
        {
            int lengthInTiles = GeneralSettings.Settings.FieldLength / GeneralSettings.Settings.TileSquare;
            int widthInTiles = GeneralSettings.Settings.FieldWidth / GeneralSettings.Settings.TileSquare;
            Map = new List<string>();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < lengthInTiles; i++)
                builder.Append(' ');
            for (int i = 0; i < widthInTiles; i++)
            {
                Map.Add(builder.ToString());
            }
            Console.WriteLine($"Map created. Length: {lengthInTiles}, Width: {widthInTiles}, Square: {lengthInTiles * widthInTiles}.");
        }

        private Field(List<string> s)
        {
            Map = s;
        }

        public static void CreateField(List<string> map = null)
        {
            if (m_field != null)
                return;
            if(map == null)
                m_field = new Field();
            else
                m_field = new Field(map);
        }
        public static List<string>? PaintNavigationMap()
        {
            if(m_field == null)
                return null;
            List<string> map = m_field.Map;

            return map;
        }
        public static List<Tile> GetWalkableTiles(List<string> map, Tile currentTile, Tile targetTile)
        {
            var possibleTiles = new List<Tile>()
            {
                new Tile() { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile() { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile() { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile, Cost = currentTile.Cost + 1 },
                new Tile() { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile, Cost = currentTile.Cost + 1 },
            };

            possibleTiles.ForEach(tile => tile.SetDistance(targetTile.X, targetTile.Y));
            return possibleTiles
                .Where(tile => tile.X >= 0 && tile.X < map.First().Length)
                .Where(tile => tile.Y >= 0 && tile.Y < map.Count)
                .Where(tile => map[tile.Y][tile.X] == 'B' || map[tile.Y][tile.X] == ' ').ToList();
        }

        public static List<Tile>? BuildPath(List<string> map, Tile currentTile, Tile targetTile)
        {
            List<Tile> activeTiles = new List<Tile>(); 
            List<Tile> visitedTiles = new List<Tile>();
            currentTile.SetDistance(targetTile.X, targetTile.Y);
            activeTiles.Add(currentTile);
            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(tile => tile.CostDistance).First();
                if(checkTile.X == targetTile.X && checkTile.Y == checkTile.Y)
                {
                    Console.WriteLine("We are in destination!");
                    var returnedListTiles = new List<Tile>();
                    while(checkTile != null)
                    {
                        returnedListTiles.Add(checkTile);
                        checkTile = checkTile.Parent;
                    }
                    returnedListTiles.Reverse();
                    return returnedListTiles;
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);
                var walkableTiles = GetWalkableTiles(map, checkTile, targetTile);
                foreach(var walkableTile in walkableTiles)
                {
                    if (visitedTiles.Any(tile => tile.X == walkableTile.X && tile.Y == walkableTile.Y))
                        continue;

                    if(activeTiles.Any(tile => tile.X == walkableTile.X && tile.Y == walkableTile.Y))
                    {
                        var existingTile = activeTiles.First(tile => tile.X == walkableTile.X && tile.Y == walkableTile.Y);
                        if (existingTile.CostDistance > checkTile.CostDistance)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(walkableTile);
                        }                        
                    }
                    else
                        activeTiles.Add(walkableTile);
                }
            }
            Console.WriteLine($"Path from {currentTile.X} : {currentTile.Y} to {targetTile.X} : {targetTile.Y} not found.");
            return null;
        }
    }
}
