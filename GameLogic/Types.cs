using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public enum TypeShape
    {
        Empty,
        Circle,
        Triangle,
        Square
    }

    public delegate void CellVisitorHandler(int x, int y, TypeShape shape);
}
