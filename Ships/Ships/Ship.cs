using ModelSMP.TimerSMP;
using ModelSMP.Navigation_System;
using ModelSMP.Ship_Cargo_Compartment;
using ModelSMP.Ship_Engine;
using ModelSMP.Ships.Behavior;
using ModelSMP.NodeLogicHandler;
using ModelSMP.Fraghts;
using ModelSMP.EdgesLogicHandler;

namespace ModelSMP.Ships
{
    class Ship
    {
        private Navigation m_navigationModule;
        private BoardCargo m_boardCargo;
        private Engine m_engine;
        private ShipBehavior m_shipBehavior;
        private Fraght? m_fraght;
        //list fraght
        public Fraght? Fraght
        {
            get
            {
                return m_fraght;
            }
            set
            {
                if (m_fraght != value)

                    m_fraght = value;
            }
        }
        public ShipBehavior Behavior { get { return m_shipBehavior; } }

        public Ship(Node nodeToSpawnShip, EdgeSystem? edgeSystem = null)
        {
            TimerData.PropertyChanged += Update;
            m_navigationModule = new Navigation();
            m_boardCargo = new BoardCargo();
            m_engine = new Engine();
            m_shipBehavior = new ShipBehavior(this, m_engine, m_navigationModule, m_boardCargo);
            nodeToSpawnShip.ShipTryEnterInNode(this);
        }

        public void Update()
        {
            if(m_navigationModule != null && m_engine != null)
            {
                m_navigationModule.ObserveMoving(m_engine.Running());
                Console.WriteLine(m_navigationModule.DistanceTraveledOnCurrentTile);
            } 
        }

    }
}
