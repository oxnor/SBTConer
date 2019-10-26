using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    class ShapeLocation
    {
        public ShapeLocation(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public byte X { set; get; }
        public byte Y { set; get; }
    }
}
