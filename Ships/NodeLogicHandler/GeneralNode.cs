

using ModelSMP.CoordsSupportEntities;
using ModelSMP.TilesLogicHandler;

namespace ModelSMP.NodeLogicHandler
{
    abstract class GeneralNode
    {
        protected Point m_point;
        protected Tile m_tileCrds;
        public Point GetCoords { get { return m_point; } }
        
    }
}
