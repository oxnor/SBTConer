using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    interface IShape
    {
       // IFigure(Ga)
        TypeShape typeShape { get; }
        List<Step> GetNextSteps(Step curStep);
    }
}
