using ModelSMP.CoordsSupportEntities;
using ModelSMP.Ships;


namespace ModelSMP.Cargos
{
    class Cargo
    {
        private Point? m_crds;

        public Point Coords { get { return m_crds; } }

        public void ConnectToShip(Ship ship)
        {

        }

        public Cargo(NodeLogicHandler.Node nodeToSpawn)
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
        public CargoContainer(NodeLogicHandler.Node node) : base(node) { }
        public CargoContainer(Point point) : base(point) { }
    }

    class CargoTanker : Cargo
    {
        public CargoTanker(NodeLogicHandler.Node node) : base(node) { }
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
