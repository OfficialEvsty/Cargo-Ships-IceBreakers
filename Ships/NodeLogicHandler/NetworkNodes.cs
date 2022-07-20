using ModelSMP.CoordsSupportEntities;

namespace ModelSMP.NodeLogicHandler
{
    class NetworkNodes
    {
        private List<Node> m_nodesNetwork;
        public static NetworkNodes Network;
        public List<Node> Nodes { get { return m_nodesNetwork; } }
        private NetworkNodes()
        {
            m_nodesNetwork = new List<Node>();
        }

        public static void InitNetwork()
        {
            if(Network == null)
                Network = new NetworkNodes();
        }

        public Node AddNode(Point point, int nodeSize)
        {
            Node newNode = new Node(point, nodeSize);
            m_nodesNetwork.Add(newNode);
            return newNode;
        }
    }
}
