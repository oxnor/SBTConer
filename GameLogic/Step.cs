using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Step
    {
        public Step(GameBoard board, ShapeLocation location, StepResult result)
        {
            Board = board;
            Location = location;
            Result = result;
        }

        public GameBoard Board { get; set; }
        public ShapeLocation Location { get; set; }
        public StepResult Result { get; set; }
    }
}
