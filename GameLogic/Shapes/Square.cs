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

        StepOffset[] Offsets = { new StepOffset(-1, -1)
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

        public override List<Step> GetNextSteps(Step curStep)
        {
            int LocX = curStep.Location.X;
            int LocY = curStep.Location.Y;
            int NewLocX;
            int NewLocY;
            List<Step> Steps = new List<Step>();
            GameBoard TplBoard = curStep.Board.Clone();
            TplBoard.RemoveShape(LocX, LocY);
            GameBoard NewBoard = TplBoard.Clone();

            foreach (StepOffset offset in Offsets)
            {
                NewLocX = LocX + offset.X;
                NewLocY = LocY + offset.Y;
                if (NewBoard.PutShape(TypeShape.Circle, NewLocX, NewLocY) == StepResult.Ok)
                {
                    Steps.Add(new Step(NewBoard, new ShapeLocation((byte)NewLocX, (byte)NewLocY), StepResult.Ok));
                    NewBoard = TplBoard.Clone();
                }
            }

            return Steps;
        }

        public override bool IsMustDie(GameBoard board, ShapeLocation curLocation)
        {
            return GetNeighbours(board, curLocation).Count < MinRelationCount;
        }
    }
}
