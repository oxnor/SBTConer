using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Shapes
{
    public class Square : ShapeBasic
    {
        public override TypeShape typeShape => TypeShape.Square;

        public override int MinRelationCount => 3;

        StepOffset[] offsets = { new StepOffset(-1, -1)
                               , new StepOffset(0, -1)
                               , new StepOffset(1, -1)
                               , new StepOffset(-1, 0)
                               , new StepOffset(1, 0)
                               , new StepOffset(-1, 1)
                               , new StepOffset(0, 1)
                               , new StepOffset(1, 1)
                               , new StepOffset(-2, 0)
                               , new StepOffset(2, 0)
                               , new StepOffset(0, -2)
                               , new StepOffset(0, 2)
                               , new StepOffset(-2, -2)
                               , new StepOffset(2, -2)
                               , new StepOffset(2, 2)
                               , new StepOffset(-2, 2)
                               };

        protected override StepOffset[] Offsets => offsets;
    }
}
