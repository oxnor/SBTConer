using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class ShapeLocation
    {
        public ShapeLocation(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public ShapeLocation(int x, int y)
        {
            X = (byte)x;
            Y = (byte)y;
        }

        public byte X { get; }
        public byte Y { get; }

        public override bool Equals(object obj)
        {
            ShapeLocation l = (ShapeLocation)obj;
            return l.X == this.X && l.Y == this.Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
