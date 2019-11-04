using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Step
    {
        public Step(GameBoard board, StepResult result, double score)
        {
            Board = board;
            Result = result;
            Score = score;
        }

        public double Score { get; }
        public GameBoard Board { get; }
        public StepResult Result { get; }
    }
}
