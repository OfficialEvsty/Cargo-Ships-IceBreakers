using ModelSMP.Nodes;
using ModelSMP.PatternObserver;

namespace ModelSMP.Edges
{
    interface IEdgeFactory
    {
        Pipeline CreatePipeline(Node n1, Node n2);

        Railway CreateRailway(Node n1, Node n2);

        SeaRoute CreateSeaRoute(Node n1, Node n2);
    }

    class Edge
    {

        public float Distance 
        { 
            get
            {
                if(FirstNode != null && LastNode != null)
                    return FirstNode.GetCoords.GetDistance(LastNode.GetCoords);
                return 0;
            } 
        }
        public Node FirstNode { get; init; }
        public Node LastNode { get; init; }
        public Edge(Node n1, Node n2)
        {
            FirstNode = n1;
            LastNode = n2;
        }
    }

    class Pipeline : Edge
    {
        public Pipeline(Node n1, Node n2) : base(n1, n2) { }
    }

    class Railway : Edge
    {
        public Railway(Node n1, Node n2) : base(n1, n2) { }
    }

    class SeaRoute : Edge
    {
        
        public SeaRoute(Node n1, Node n2) : base(n1, n2) 
        {
            EventObservable.NotifyObservers(this);
        }
        public int IceLevel { get; private set; }
    }

    class EdgeFactory : IEdgeFactory
    {
        public Pipeline CreatePipeline(Node n1, Node n2)
        {
            return new Pipeline(n1, n2);
        }

        public Railway CreateRailway(Node n1, Node n2)
        {
            return new Railway(n1, n2);
        }

        public SeaRoute CreateSeaRoute(Node n1, Node n2)
        {
            SeaRoute newSeaRoute = new SeaRoute(n1, n2);

            return newSeaRoute;
        }
    }
}
