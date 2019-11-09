using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Shapes
{
    abstract public class ShapeBasic : IShape
    {
        StepOffset[] neighborOffsets = { new StepOffset(-1, -1)
                               , new StepOffset(0, -1)
                               , new StepOffset(1, -1)
                               , new StepOffset(-1, 0)
                               , new StepOffset(1, 0)
                               , new StepOffset(-1, 1)
                               , new StepOffset(0, 1)
                               , new StepOffset(1, 1)
                               };

        protected abstract StepOffset[] Offsets
        {
            get;
        }

        public abstract TypeShape typeShape { get; }

        public abstract int MinRelationCount { get; }

        public virtual List<Step> GetNextSteps(Step curStep)
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
                        Steps.Add(new Step(newBoard, newLocation, StepResult.Die, double.MinValue));
                    }
                    else
                    {
                        Steps.Add(new Step(newBoard, newLocation, StepResult.Ok, newBoard.Score(newLocation)));
                    }

                    newBoard = curStep.Board.Clone();
                }
            }

            return Steps;
        }

        public virtual bool IsMustDie(GameBoard board, ShapeLocation curLocation)
        {
            return board.GetNeighbours(curLocation).Count < MinRelationCount;
        }
    }
}
