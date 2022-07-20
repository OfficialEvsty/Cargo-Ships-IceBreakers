
using ModelSMP.CoordsSupportEntities;
using ModelSMP.Cargos;
using ModelSMP.Ships;

namespace ModelSMP.NodeLogicHandler
{
    sealed class Node : GeneralNode
    {
        private LoadingSection m_loadingSection;        
        private List<Ship> m_ships = new List<Ship> ();
        private Dictionary<Cargo, decimal> m_priceCargo = new Dictionary<Cargo, decimal>();
        private int i_maxNodeSize;

        public LoadingSection LoadingSection { get { return m_loadingSection; } }
        public List<Ship> Ships { get { return m_ships; } }
           
        public Dictionary<Cargo, decimal> GetPriceCargo { get { return m_priceCargo; } }                
        public int MaxNodeSize { get { return i_maxNodeSize; } }
        

        

        public Node(Point pointToSetNode, int maxSize)
        {
            m_loadingSection = new LoadingSection(this, maxSize / 3);
            m_point = pointToSetNode;
            i_maxNodeSize = maxSize;
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

        public void ShipLeaveNode(Ship leavedShip)
        {
            for(int i = 0; i < m_ships.Count; i++)
            {
                if(m_ships[i] == leavedShip)
                {
                    Console.WriteLine($"Корабль {m_ships[i]} покинул порт {this}");
                    m_ships.RemoveAt(i);                    
                }
            }
        }

        
    }
}
