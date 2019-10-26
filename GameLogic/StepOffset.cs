using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    class StepOffset
    {
        public StepOffset(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { set; get; }
        public int Y { set; get; }
    }
}
