

using ModelSMP.NodeLogicHandler;

namespace ModelSMP.EdgesLogicHandler
{
    class EdgeSection
    {
        public GeneralNode? FirstNode;
        public GeneralNode? LastNode;
        public float Distance
        {
            get
            {
                if(FirstNode != null && LastNode != null)
                    return FirstNode.GetCoords.GetDistance(LastNode.GetCoords);
                return 0;
            }
        }
        public EdgeSection(GeneralNode n1, GeneralNode n2)
        {
            FirstNode = n1;
            LastNode = n2;
        }
    }
}
