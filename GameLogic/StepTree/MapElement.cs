using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.StepTree
{
    public class MapElement
    {
        public MapElement(ShapeLocation location, int deepSearch = -1, Step bestStep = null)
        {
            Location = location;
            DeepSearch = -1;
            BestStep = bestStep;
        }

        public ShapeLocation Location {get;}
        public int DeepSearch { set; get; }
        public Step BestStep { set; get; }

        public MapElement Clone()
        {
            return new MapElement(Location, DeepSearch, BestStep);
        }
    }
}
