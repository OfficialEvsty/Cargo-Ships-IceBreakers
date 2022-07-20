using ModelSMP.PatternObserver;
using ModelSMP.PatternObserver.Observer;


namespace ModelSMP.EdgesLogicHandler
{
    internal class EdgeSystem : IEventObserver<Edge>
    {
        private List<Edge> globalEdges;
        public List<Edge> GlobalSystem { get { return globalEdges; } }
        public EdgeSystem()
        {
            globalEdges = new List<Edge>();
            EventObservable.AddEventObserver(this);
        }

        public void Update(Edge ev)
        {
            globalEdges.Add(ev);
            Console.WriteLine("Добавил");
        }
    }
}
