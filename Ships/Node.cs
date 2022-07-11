
using ModelSMP.CoordsSupportEntities;
using ModelSMP.Cargos;
using ModelSMP.Ships;
using ModelSMP.Ships.Behavior.Ship_States;

namespace ModelSMP.Nodes
{
    class Node
    {
        private Point m_coords;
        private List<Ship> m_ships = new List<Ship> ();
        private Dictionary<Cargo, decimal> m_priceCargo = new Dictionary<Cargo, decimal>();
        private Dictionary<Cargo, int> m_weightCargo = new Dictionary<Cargo, int>();
        private Dictionary<Cargo, int> m_requiredCargo = new Dictionary<Cargo, int>();
        private Dictionary<Cargo, int> m_contractedCargo = new Dictionary<Cargo, int>();
        private List<Ship> m_shipsOnLoading = new List<Ship>();
        private int i_maxNodeSize;


        public List<Ship> Ships { get { return m_ships; } }
        public Dictionary<Cargo, int> AvailableWeightCargo { get { return m_weightCargo; } }     
        public Dictionary<Cargo, decimal> GetPriceCargo { get { return m_priceCargo; } }        
        public Point GetCoords { get { return m_coords; } }
        public int MaxNodeSize { get { return i_maxNodeSize; } }
        public int MaxLoadingSize { get { return MaxNodeSize / 3; } }

        public Node(Point pointToSetNode, int maxSize)
        {
            m_coords = pointToSetNode;
            i_maxNodeSize = maxSize;
            ShipWaitingForLoadingState.CheckNodeForLoading += ShipTryGetPlaceForLoading;
        }

        public bool ShipTryEnterInNode(Ship enteredShip)
        {
            if(m_ships.Count < MaxNodeSize)
            {
                m_ships.Add(enteredShip);
                enteredShip.Behavior.SetStartNode(this);
                enteredShip.Behavior.GoNextState();
                Console.WriteLine($"{enteredShip} вошёл в порт {this}");
                return true;
            }
            Console.WriteLine($"{enteredShip} не может зайти в порт {this}. Причина: Порт заполнен");
            return false;
        }

        private void ShipTryGetPlaceForLoading()
        {
            if (m_shipsOnLoading.Count > MaxLoadingSize)
                Console.WriteLine($"Места для загрузки в {this} заполнены");
            foreach(Ship ship in Ships)
            {
                if(ship.Behavior.State is ShipWaitingForLoadingState && ship.Behavior.BoardCargo.IsCargoContracted)
                {
                    m_shipsOnLoading.Add(ship);
                    ship.Behavior.GoNextState();
                    Console.WriteLine($"{ship} успешно встал на загрузку в {this}.");
                }
            }
        }

        public bool ContractAvailableCargo(Dictionary<Cargo, int> requiredCargo)
        {
            bool flagToReturn = false;
            foreach(Cargo key in requiredCargo.Keys)
            {
                bool IsKeyContains = AvailableWeightCargo.ContainsKey(key);
                if (IsKeyContains)
                {
                    bool IsValueContains = AvailableWeightCargo[key] >= requiredCargo[key];
                    if (!IsValueContains)
                    {
                        flagToReturn = true;
                        Console.WriteLine($"В порту {this} недостаточно {key.GetType}");
                        AddRequiredCargo(key, requiredCargo[key] - AvailableWeightCargo[key]);
                    }
                }
                else
                {
                    flagToReturn = true;
                    Console.WriteLine($"В порту {this} нету {key.GetType}");
                    AddRequiredCargo(key, requiredCargo[key]);
                }                
            }

            if (flagToReturn)
                return false;

            foreach(Cargo key in requiredCargo.Keys)
            {
                m_contractedCargo[key] += requiredCargo[key];
            }
            Console.WriteLine($"Груз успешно закантроктован в порту {this}");
            return true;
        }

        private void AddRequiredCargo(Cargo cargoType, int count)
        {
            if (m_requiredCargo.ContainsKey(cargoType))
                m_requiredCargo[cargoType] += count;
            else m_requiredCargo.Add(cargoType, count);
            Console.WriteLine($"{count} {cargoType} was added in required cargo in {this}");
        }

        /// <summary>
        /// Временный метод
        /// </summary>
        /// <param name="requiredCargo"></param>
        private void AddCargo(Dictionary<Cargo, int> requiredCargo)
        {
            foreach(var key in requiredCargo.Keys)
            {
                if (m_weightCargo.ContainsKey(key))
                    m_weightCargo.Add(key, requiredCargo[key]);
                else m_weightCargo.Add(key, requiredCargo[key]);
                if(m_requiredCargo.ContainsKey(key))
                    m_requiredCargo[key] = 0;
            }
            Console.WriteLine("Cargo successfully added in Available cargo");
        }

        public void LoadCargoOnShip()
        {

        }
    }
}
