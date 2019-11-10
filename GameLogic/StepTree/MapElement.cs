using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.StepTree
{
    public class MapElement
    {
        public MapElement(ShapeLocation location, int deep = 0, Step bestStep = null)
        {
            Location = location;
            Deep = deep;
            BestStep = bestStep;
        }

        public ShapeLocation Location {get;}
        public int Deep { set; get; }
        public Step BestStep { set; get; }

        public MapElement Clone(ShapeLocation location = null)
        {
            return new MapElement(location??Location, Deep, BestStep);
        }
    }
}
