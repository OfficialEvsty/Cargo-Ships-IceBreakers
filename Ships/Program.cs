using ModelSMP.Edges;
using ModelSMP.Ships;
using ModelSMP.Nodes;
using ModelSMP.TimerSMP;
using ModelSMP.Fraghts;
using ModelSMP.Cargos;

class Program
{
    public static void Main()
    {
            
        TimerData myTimer = new TimerData();
        Node node1 = new Node(new ModelSMP.CoordsSupportEntities.Point(100, -201), 5);
        Node node2 = new Node(new ModelSMP.CoordsSupportEntities.Point(-100, 1101), 8);
        IFraghtStrategy voyage = new Voyage();
        Dictionary<Cargo, int> requiredCargo = new Dictionary<Cargo, int>();
        requiredCargo.Add(new CargoContainer(node2), 5);
        FraghtMarket.Fraghts.Add(new Fraght(voyage, requiredCargo, node2, node1));
        Ship myGreatShip = new Ship(node2);
        Ship MYSHIP = new Ship(node1);
        EdgeFactory edgeFactory = new EdgeFactory();
        edgeFactory.CreateSeaRoute(node1, node2);
        //myGreatShip.StartEngine();
        //myGreatShip.MoveOnEdge();
        myTimer.TimerOff();
        
        var count = 0;
        while (true)
        {
            count++;
        }
    }
}