using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic
{
    public enum TypeShape : byte
    {
        Empty = 0,
        Circle,
        Triangle,
        Square
    }

    public enum StepResult : byte
    {
        Ok = 0,
        Die,
        Illegal,
    }

    public delegate void CellVisitorHandler(int x, int y, TypeShape shape);


}
