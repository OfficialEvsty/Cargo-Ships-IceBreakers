

using ModelSMP.Cargos;
using ModelSMP.NodeLogicHandler;
using ModelSMP.Ships;

namespace ModelSMP.Fraghts
{
    public interface IFraghtStrategy
    {
        void CharterShip();
    }
    sealed class Fraght
    {
        private Dictionary<Cargo, int> m_requiredCargo;
        private Ship? m_fraghter;

        public Ship Fraghter { get => m_fraghter; private set { m_fraghter = value; } }
        public Dictionary<Cargo, int> RequiredCargo { get { return m_requiredCargo; } }
        public IFraghtStrategy FraghtStrategy;
        public Node FromNode;
        public Node ToNode;
        
        public Fraght(IFraghtStrategy strategy, Dictionary<Cargo, int> reqCargo, Node From, Node To)
        {
            FraghtStrategy = strategy;
            m_requiredCargo = reqCargo;
            FromNode = From;
            ToNode = To;
        }

        public void Contract(Ship fraghter)
        {
            Fraghter = fraghter;
            FraghtMarket.Fraghts.Remove(this);
        }
    }

    class Rent : IFraghtStrategy
    {
        void IFraghtStrategy.CharterShip()
        {
            
        }
    }

    class Voyage : IFraghtStrategy
    {
        void IFraghtStrategy.CharterShip()
        {
            
        }
    }
}
