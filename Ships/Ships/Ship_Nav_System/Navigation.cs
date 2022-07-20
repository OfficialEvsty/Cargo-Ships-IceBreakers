using ModelSMP.NodeLogicHandler;
using ModelSMP.TilesLogicHandler;

namespace ModelSMP.Navigation_System
{
    class Navigation 
    {
        private List<List<Tile>> m_availableRoutesList = new List<List<Tile>>();
        private List<string>? m_map;
        private float f_distanceTraveledOnCurrentTile;
        public List<List<Tile>> AvailableEdgesList { get { return m_availableRoutesList; } }
        public List<Tile> ChosenRoute { get; private set; }
        public Tile CurrentTile { get; private set; }
        public event Action OnEndRoute;

        public float DistanceTraveledOnCurrentTile
        {
            get { return f_distanceTraveledOnCurrentTile; }
            private set
            {
                f_distanceTraveledOnCurrentTile = value;
                ControlCurrentTileTraveledDistance();               
            }
        }
        
        public Node? CurrentNode { get; private set; }
        public Node? FromNode { get; set; }
        public Node? ToNode { get; set; }

        public Navigation()
        {
            PaintMap();
        }

        private void SetCurrentNode()
        {
            if (CurrentTile == null)
                throw new Exception("CurrentTile is not exist. Nullreference.");
            foreach(Node node in NetworkNodes.Network.Nodes)
            {
                if (IsTilesEqual(Tile.GetTileCoords(node.GetCoords), CurrentTile))
                    CurrentNode = node;
            }
        }

        public void ObserveMoving(float distanceTraveled)
        {
            DistanceTraveledOnCurrentTile += distanceTraveled;
        }
        
        private void ControlCurrentTileTraveledDistance()
        {
            if (CurrentTile == null)
                return;
            while(CurrentTile != null && f_distanceTraveledOnCurrentTile > CurrentTile.Square)
            {
                f_distanceTraveledOnCurrentTile -= CurrentTile.Square;
                SwitchCurrentTile();
            }         
        }

        private void SwitchCurrentTile()
        {
            if (GetNextTile() == null && CurrentTile != null)
            {
                SetCurrentNode();
                OnEndRoute?.Invoke();                
                CurrentTile = null;
                f_distanceTraveledOnCurrentTile = 0;
                Console.WriteLine("Путь пройден");
                return;
            }                
            CurrentTile = GetNextTile();
        }
        private Tile? GetNextTile()
        {
            foreach(Tile tile in ChosenRoute)
            {
                if (CurrentTile == tile)
                    if (ChosenRoute.IndexOf(tile) + 1 < ChosenRoute.Count)
                        return ChosenRoute[ChosenRoute.IndexOf(tile) + 1];
            }
            return null;
        }
        private void PaintMap()
        {            
            m_map = Field.PaintNavigationMap();
        }
        public void SetToNode(Node destinationNode)
        {
            ToNode = destinationNode;
        }
        public void InstallRoute()
        {
            if (FromNode == null || ToNode == null)
                return;
            var route = GetRouteFromList(FromNode, ToNode);
            if (route == null)
            {
                BuildNewRoute();
                route = GetRouteFromList(FromNode, ToNode);
            }
            if (route == null)
                return;
            ChosenRoute = route;
            CurrentTile = route.First();
            Console.WriteLine($"Маршрут был выбран {ChosenRoute}");
        }
        private static bool IsRoutesEqual(List<Tile> roate1, List<Tile> roate2)
        {
            if (roate1.Count != roate2.Count)
                return false;
            for (int i = 0; i < roate1.Count; i++)
            {
                if (roate1[i].X != roate2[i].X && roate1[i].Y != roate2[i].Y)
                    break;
                if (i == roate1.Count - 1)
                    return true;
            }

            for (int i = roate1.Count; i > 0; i--)
            {
                if (roate1[i].X != roate2[i].X && roate1[i].Y != roate2[i].Y)
                    break;
                if (i == 1)
                    return true;
            }
            return false;
        }
        private static bool IsTilesEqual(Tile t1, Tile t2)
        {
            if (t1 == null || t2 == null)
                return false;
            return t1.X == t2.X && t1.Y == t2.Y;
        }
        private bool IsContainInRoutesList(List<Tile> newRoute)
        {
            foreach (var route in m_availableRoutesList)
                if (IsRoutesEqual(route, newRoute))
                    return true;
            return false;
        }
        public List<Tile>? GetRouteFromList(Node from, Node to)
        {
            Tile tileFrom = Tile.GetTileCoords(from.GetCoords);
            Tile tileTo = Tile.GetTileCoords(to.GetCoords);
            if(tileFrom == null || tileTo == null)
                return null;
            
            foreach(var tileList in m_availableRoutesList)
            {
                
                if (tileList.First().X == tileFrom.X && tileList.First().Y == tileFrom.Y)
                {
                    if (tileList.Last().X == tileTo.X && tileList.Last().Y == tileTo.Y)
                        return tileList;
                }
                else if (tileList.First().X == tileTo.X && tileList.First().Y == tileTo.Y)
                    {
                        var reverseTilesList = tileList;
                        reverseTilesList.Reverse();
                        if (reverseTilesList.First().X == tileFrom.X && reverseTilesList.First().Y == tileFrom.Y)
                            return reverseTilesList;
                    }
            }
            return null;
        }
        private bool BuildNewRoute()
        {
            if (FromNode == null || ToNode == null)
                return false;
            Tile fromNode = Tile.GetTileCoords(FromNode.GetCoords);
            Tile toNode = Tile.GetTileCoords(ToNode.GetCoords);

            if (fromNode == null || toNode == null)
                return false;
            if (m_map == null)
                return false;
            var newRoute = Field.BuildPath(m_map, fromNode, toNode);
            if (newRoute == null)
                return false;
            AddRoute(newRoute);
            return true;
        }

        private void AddRoute(List<Tile>? newRoute)
        {
            if (newRoute == null)
                return;
            if (!IsContainInRoutesList(newRoute))
                m_availableRoutesList.Add(newRoute);
        }

        public bool CheckRouteValid(List<Tile> route)
        {
            return false;
        }
    }
}