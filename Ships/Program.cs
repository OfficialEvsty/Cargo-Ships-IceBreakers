
using ModelSMP.Ships;
using ModelSMP.TimerSMP;
using ModelSMP.Fraghts;
using ModelSMP.Cargos;
using ModelSMP.NodeLogicHandler;
using ModelSMP.TilesLogicHandler;

class Program
{
    public static void Main()
    {
        List<string> map = new List<string>() { "                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                   57                                                                                                 ",
"                                                    6666                                                                                              ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
"                                                                                                                                                      ",
        };
        Field.CreateField();    
        TimerData myTimer = new TimerData();
        NetworkNodes.InitNetwork();
        Node node1 = NetworkNodes.Network.AddNode(new ModelSMP.CoordsSupportEntities.Point(100, 201), 5);
        Node node2 = NetworkNodes.Network.AddNode(new ModelSMP.CoordsSupportEntities.Point(2001, 1200), 8);
        Dictionary<Cargo, int> requiredCargo = new Dictionary<Cargo, int>();
        requiredCargo.Add(new CargoContainer(node2), 5);
        IFraghtStrategy voyage = new Voyage();
        FraghtMarket.Fraghts.Add(new Fraght(voyage, requiredCargo, node2, node1));
        Ship myGreatShip = new Ship(node2);
        Ship MYSHIP = new Ship(node1);
        

        //myTimer.TimerOff();
        

        

        Console.ReadLine();
    }
}