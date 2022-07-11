using ModelSMP.TimerSMP;
using ModelSMP.Navigation_System;
using ModelSMP.Ship_Cargo_Compartment;
using ModelSMP.Ship_Engine;
using ModelSMP.Ships.Behavior;
using ModelSMP.Nodes;
using ModelSMP.Fraghts;

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

        public Ship(Node nodeToSpawnShip)
        {
            TimerData.PropertyChanged += Update;
            m_navigationModule = new Navigation();
            m_boardCargo = new BoardCargo();
            m_engine = new Engine();
            m_shipBehavior = new ShipBehavior(this, m_engine, m_navigationModule, m_boardCargo);
            nodeToSpawnShip.ShipTryEnterInNode(this);
        }


        public void StartEngine()
        {
            m_engine.IsStartEngine = true;
            Console.WriteLine("The Engine is start.");
        }

        public void StopEngine()
        {

            m_engine.IsStartEngine = false;
            Console.WriteLine("The Engine is stop.");
        }



        public void Update()
        {
            if(m_navigationModule != null && m_engine != null)
            {
                m_navigationModule.DistanceTraveledOnEdge += m_engine.Running() * GeneralSettings.Settings.TimerTickInSeconds * GeneralSettings.Settings.MultipleTimer / 3600;
                Console.WriteLine(m_navigationModule.DistanceTraveledOnEdge);
            }
            
        }

    }
}
