using ModelSMP.Cargos;
using ModelSMP.Fraghts;
using ModelSMP.Nodes;

namespace ModelSMP.Ship_Cargo_Compartment
{
    interface ICarryCargo
    {
        bool Loading();
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
        public bool Loading()
        {
            throw new NotImplementedException();
        }

        public void Unloading()
        {
            throw new NotImplementedException();
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
