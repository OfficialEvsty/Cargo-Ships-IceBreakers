using ModelSMP.CoordsSupportEntities;
using ModelSMP.Ships;


namespace ModelSMP.Cargos
{
    class Cargo
    {
        private Point? m_crds;
        private bool b_isContract;

        public bool IsContract { get { return b_isContract; } }
        public Point Coords { get { return m_crds; } }

        public void ConnectToShip(Ship ship)
        {

        }

        public Cargo(Nodes.Node nodeToSpawn)
        {
            m_crds = nodeToSpawn.GetCoords;
        }

        public Cargo(Point pointToSpawn)
        {
            m_crds = pointToSpawn;
        }
    }

    class CargoContainer : Cargo
    {
        public CargoContainer(Nodes.Node node) : base(node) { }
        public CargoContainer(Point point) : base(point) { }
    }

    class CargoTanker : Cargo
    {
        public CargoTanker(Nodes.Node node) : base(node) { }
        public CargoTanker(Point point) : base(point) { }
    }

    interface ICargoFactory
    {
        CargoContainer CreateContainer(Point p);
        CargoTanker CreateTanker(Point p);
    }

    class CargoFactory : ICargoFactory
    {
        public CargoContainer CreateContainer(Point pointToSpawn)
        {
            return new CargoContainer(pointToSpawn);
        }
        public CargoTanker CreateTanker(Point pointToSpawn)
        {
            return new CargoTanker(pointToSpawn);
        }
    }
}
