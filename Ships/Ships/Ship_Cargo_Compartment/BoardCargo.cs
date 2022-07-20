using ModelSMP.Cargos;
using ModelSMP.Fraghts;
using ModelSMP.NodeLogicHandler;

namespace ModelSMP.Ship_Cargo_Compartment
{
    interface ICarryCargo
    {
        void Loading(Dictionary<Cargo, int> cargoToLoad);
        void Unloading();
    }

    interface IContractCargo
    {
        void ContractCargo();

        void ClearContracts();
    }

    class BoardCargo : ICarryCargo, IContractCargo
    {
        private Dictionary<Cargo, int> m_loadedCargo = new Dictionary<Cargo, int>();

        public Dictionary<Cargo, int> LoadedCargo { get { return m_loadedCargo; } }


        public bool IsCargoContracted { get; private set; }
        public float DeadWeight;
        public bool IsLoaded { get { return m_loadedCargo.Count > 0; } }
        public void Loading(Dictionary<Cargo, int> cargoToLoad)
        {
            foreach(Cargo key in cargoToLoad.Keys)
            {
                if (m_loadedCargo.ContainsKey(key))
                    m_loadedCargo[key] += cargoToLoad[key];
                else
                    m_loadedCargo.Add(key, cargoToLoad[key]);
            }
        }

        public void Unloading()
        {
            m_loadedCargo.Clear();
            
        }

        public void ContractCargo()
        {
            IsCargoContracted = true;

        }

        public void ClearContracts()
        {
            IsCargoContracted = false;
        }
    }
}
