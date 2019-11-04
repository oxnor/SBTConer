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
                               };

        public override List<Step> GetNextSteps(Step curStep)
        {
            int LocX = curStep.Board.CurrentShapeLocation.X;
            int LocY = curStep.Board.CurrentShapeLocation.Y;
            int NewLocX;
            int NewLocY;
            List<Step> Steps = new List<Step>();
            GameBoard newBoard = curStep.Board.Clone();
            ShapeLocation newLocation;
            foreach (StepOffset offset in Offsets)
            {
                NewLocX = LocX + offset.X;
                NewLocY = LocY + offset.Y;
                if (newBoard.MoveShape(NewLocX, NewLocY) == StepResult.Ok)
                {
                    newLocation = new ShapeLocation(NewLocX, NewLocY);
                    if (IsMustDie(newBoard, newLocation))
                    {
                        newBoard.RemoveShape(NewLocX, NewLocY);
                        Steps.Add(new Step(newBoard, StepResult.Die, double.MinValue));
                    }
                    else
                    {
                        Steps.Add(new Step(newBoard, StepResult.Ok, newBoard.Score(newLocation)));
                    }

                    newBoard = curStep.Board.Clone();
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
