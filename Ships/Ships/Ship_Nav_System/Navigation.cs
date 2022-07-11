using ModelSMP.Edges;
using ModelSMP.Nodes;
using ModelSMP.PatternObserver;
using ModelSMP.PatternObserver.Observer;

namespace ModelSMP.Navigation_System
{
    class Navigation : IEventObserver<SeaRoute>
    {
        private List<Edge> m_availableEdgesList = new List<Edge>();

        public List<Edge> AvailableEdgesList { get { return m_availableEdgesList; } }
        public Edge? ChoosenEdge { get; set; }
        public float DistanceTraveledOnEdge;
        
        public Node? FromNode { get; set; }
        public Node? ToNode { get; set; }

        public Navigation()
        {
            EventObservable.AddEventObserver(this);
        }

        public void Update(SeaRoute seaRoute)
        {
            m_availableEdgesList.Add(seaRoute);
            Console.WriteLine("New Sea Route was added to Navigation System. Количество маршрутов: " + m_availableEdgesList.Count);
        }


    }
}


