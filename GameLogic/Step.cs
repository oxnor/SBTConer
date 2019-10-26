using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    class Step
    {
        GameBoard Board { get; set; }
        ShapeLocation Location { get; set; }
        StepResult Result { get; set; }
    }
}
