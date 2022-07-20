using ModelSMP.NodeLogicHandler;
using ModelSMP.PatternObserver;

namespace ModelSMP.EdgesLogicHandler
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
        public GeneralNode? FirstNode { get; init; }
        public GeneralNode? LastNode { get; init; }        

        public List<EdgeSection>? EdgeSections { get; private set; }

        protected Edge(GeneralNode n1, GeneralNode n2)
        {
            FirstNode = n1;
            LastNode = n2;
            EventObservable.NotifyObservers(this);
        }
        

        public void ShowRoutes()
        {
            int i = 0;
            if (EdgeSections == null)
                return;
            foreach(EdgeSection edgeSection in EdgeSections)
            {
                Console.WriteLine($"{++i}-ый путь в {this} протяженностью {edgeSection.Distance}. Общий путь {this.Distance}");
            }
        }
    }

    class Pipeline : Edge
    {
        public Pipeline(Node n1, Node n2) : base(n1, n2)
        { 
            FirstNode = n1;
            LastNode = n2;
        }
    }

    class Railway : Edge
    {
        public Railway(Node n1, Node n2) : base (n1, n2)
        {
            FirstNode = n1;
            LastNode = n2;
        }
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
