using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Shapes
{
    public class Circle : ShapeBasic
    {
        public override TypeShape typeShape => TypeShape.Circle;

        public override int MinRelationCount => 1;

        StepOffset[] offsets = { new StepOffset(-1, -1)
                               , new StepOffset(0, -1)
                               , new StepOffset(1, -1)
                               , new StepOffset(-1, 0)
                               , new StepOffset(1, 0)
                               , new StepOffset(-1, 1)
                               , new StepOffset(0, 1)
                               , new StepOffset(1, 1)
                               };

        protected override StepOffset[] Offsets => offsets;
    }
}
