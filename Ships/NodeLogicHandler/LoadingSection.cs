

using ModelSMP.Cargos;
using ModelSMP.Fraghts;
using ModelSMP.PatternObserver.Observer;
using ModelSMP.Ships;
using ModelSMP.Ships.Behavior.Ship_States;

namespace ModelSMP.NodeLogicHandler
{
    sealed class LoadingSection : IEventObserver<ShipOnLoadingState>
    {
        private Dictionary<Cargo, int> m_weightCargo = new Dictionary<Cargo, int>();
        private Dictionary<Cargo, int> m_requiredCargo = new Dictionary<Cargo, int>();
        private Dictionary<Cargo, int> m_contractedCargo = new Dictionary<Cargo, int>();
        private List<Ship> m_shipsOnLoading = new List<Ship>();
        private Node m_node;
        private int i_maxLoadingSize;

        public Dictionary<Cargo, int> AvailableWeightCargo { get { return m_weightCargo; } }
        

        public event EventHandler<Dictionary<Cargo, int>>? AddCargoInNode;

        public LoadingSection(Node ownNode, int loadingSize)
        {
            m_node = ownNode;
            i_maxLoadingSize = loadingSize;
            AddCargoInNode += OnAddCargo;
            ShipWaitingForLoadingState.CheckNodeForLoading += ShipTryGetPlaceForLoading;
            PatternObserver.EventObservable.AddEventObserver(this);
        }

        private void ShipTryGetPlaceForLoading()
        {
            if (m_shipsOnLoading.Count > i_maxLoadingSize)
                Console.WriteLine($"Места для загрузки в {this} заполнены");
            for(int i = 0; i < m_node.Ships.Count; i++)
            {
                if (m_node.Ships[i].Behavior.State is ShipWaitingForLoadingState)
                {
                    Fraght fraght = m_node.Ships[i].Fraght;
                    if (!m_node.Ships[i].Behavior.BoardCargo.IsCargoContracted && fraght != null)
                    {
                        if (!ContractAvailableCargo(fraght))
                        {
                            return;
                        }
                    }
                    m_shipsOnLoading.Add(m_node.Ships[i]);
                    m_node.Ships[i].Behavior.GoNextState();
                    //Console.WriteLine($"{m_node.Ships[i]} успешно встал на загрузку в {this}.");
                }
            }
        }

        public bool ContractAvailableCargo(Fraght fraght)
        {
            Dictionary<Cargo, int> requiredCargo = fraght.RequiredCargo;
            bool flagToReturn = false;
            foreach (Cargo key in requiredCargo.Keys)
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

            foreach (Cargo key in requiredCargo.Keys)
            {
                if (m_contractedCargo.ContainsKey(key))
                    m_contractedCargo[key] += requiredCargo[key];
                else m_contractedCargo.Add(key, requiredCargo[key]);
                fraght.Fraghter.Behavior.BoardCargo.ContractCargo();
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
            AddCargoInNode?.Invoke(this, m_requiredCargo);
        }

        /// <summary>
        /// Временный метод
        /// </summary>
        /// <param name="requiredCargo"></param>
        private void OnAddCargo(object sender, Dictionary<Cargo, int> requiredCargo)
        {
            if (sender == this)
            {
                foreach (var key in requiredCargo.Keys)
                {
                    if (m_weightCargo.ContainsKey(key))
                        m_weightCargo.Add(key, requiredCargo[key]);
                    else m_weightCargo.Add(key, requiredCargo[key]);
                    if (m_requiredCargo.ContainsKey(key))
                        m_requiredCargo[key] = 0;
                }
                Console.WriteLine("Cargo successfully added in Available cargo");

            }
        }

        private void SubtractContractedCargo(Dictionary<Cargo, int> subtractedCargo)
        {
            foreach(Cargo key in subtractedCargo.Keys)
            {
                m_contractedCargo[key] -= subtractedCargo[key];
            }
        }

        public void LoadCargoOnShip()
        {
            Dictionary<Cargo, int> requiredCargo;
            for (int i = 0; i < m_shipsOnLoading.Count; i++)
            {
                if (m_shipsOnLoading[i].Fraght == null || m_shipsOnLoading[i].Behavior.State is not ShipOnLoadingState)
                    continue;

                requiredCargo = m_shipsOnLoading[i].Fraght.RequiredCargo;
                SubtractContractedCargo(requiredCargo);
                m_shipsOnLoading[i].Behavior.BoardCargo.Loading(requiredCargo);
                ((ShipOnLoadingState)m_shipsOnLoading[i].Behavior.State).ReadyLoad -= LoadCargoOnShip;                
                m_shipsOnLoading[i].Behavior.GoNextState();
                m_shipsOnLoading.Remove(m_shipsOnLoading[i]);
            }
        }

        public void Update(ShipOnLoadingState ev)
        {
            ev.ReadyLoad += LoadCargoOnShip;
        }
    }
}
