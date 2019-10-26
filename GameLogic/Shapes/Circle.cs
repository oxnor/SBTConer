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
            List<Step> Steps = new List<Step>();
            GameBoard NewBoard = curStep.Board.Clone();
            int LocX = curStep.Location.X;
            int LocY = curStep.Location.Y;
            int NewLocX;
            int NewLocY;
            foreach (StepOffset offset in Offsets)
            {
                NewLocX = LocX + offset.X;
                NewLocY = LocY + offset.Y;
                if (NewBoard.PutShape(TypeShape.Circle, NewLocX, NewLocY) == StepResult.Ok)
                {
                    Steps.Add(new Step(NewBoard, new ShapeLocation((byte)NewLocX, (byte)NewLocY), StepResult.Ok));
                    NewBoard = curStep.Board.Clone(); 
                }
            }

            return Steps;
        }
    }
}
