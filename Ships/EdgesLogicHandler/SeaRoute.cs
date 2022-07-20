using ModelSMP.NodeLogicHandler;
using ModelSMP.PatternObserver;

namespace ModelSMP.EdgesLogicHandler
{
    class SeaRoute : Edge
    {
        public SeaRoute(GeneralNode n1, GeneralNode n2) : base(n1, n2)
        {
            FirstNode = n1;
            LastNode = n2;
            //ConstructEdgesComponent(n1, n2, GeneralNode.CreateNodesPath(n1, n2));
            EventObservable.NotifyObservers(this);
        }
    }
}
    