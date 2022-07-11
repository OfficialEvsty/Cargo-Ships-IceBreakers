using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSMP.Fraghts
{
    static class FraghtMarket
    {
        private static List<Fraght> m_fraghts = new List<Fraght>();

        public static List<Fraght> Fraghts { get { return m_fraghts; } set { m_fraghts = value; } }

        
    }
}
