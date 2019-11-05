using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Shapes
{
    public class Triangle : ShapeBasic
    {
        public override TypeShape typeShape => TypeShape.Triangle;

        public override int MinRelationCount => 2;

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
                               };

        protected override StepOffset[] Offsets => offsets;
    }
}
