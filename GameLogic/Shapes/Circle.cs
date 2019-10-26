using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Shapes
{
    class Circle : IShape
    {
        public TypeShape typeShape => TypeShape.Circle;
        StepOffset[] Offsets = { new StepOffset(-1, -1)
                               , new StepOffset(0, -1)
                               , new StepOffset(1, -1)
                               , new StepOffset(-1, 0)
                               , new StepOffset(1, 0)
                               , new StepOffset(-1, 1)
                               , new StepOffset(0, 1)
                               , new StepOffset(1, 1)
                               };

        public List<Step> GetNextSteps(Step curStep)
        {
            return new List<Step>();
        }
    }
}
