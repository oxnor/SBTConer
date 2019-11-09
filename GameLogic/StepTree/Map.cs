using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.StepTree
{
    public class Map
    {
        public Map(MapElement[] nodesArray)
        {
            nodes = nodesArray.Select(n => n.Clone()).ToArray();
        }

        private MapElement[] nodes;

        public MapElement FindNodeFor(ShapeLocation shapeLocation)
        {
            return nodes.First(n => n.Location.Equals(shapeLocation));
        }

        public Map MoveShape(ShapeLocation curLocation, ShapeLocation newLocation)
        {
            MapElement curElement = FindNodeFor(curLocation);
            MapElement newElement = curElement.Clone(newLocation);
            MapElement[] newNodesArray = nodes.Select(n => n != curElement? n : newElement).ToArray();
            return new Map(newNodesArray);
        }
    }
}
