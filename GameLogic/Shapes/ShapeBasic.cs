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

        public virtual List<ShapeLocation> GetNeighbours(GameBoard board, ShapeLocation curLocation)
        {
            List<ShapeLocation> neighbors = new List<ShapeLocation>();

            int NewLocX;
            int NewLocY;
            TypeShape neighborShape;
            foreach (StepOffset offset in neighborOffsets)
            {
                NewLocX = curLocation.X + offset.X;
                NewLocY = curLocation.Y + offset.Y;
                try
                {
                    neighborShape = board.GetShape(NewLocX, NewLocY);
                    if (neighborShape != TypeShape.Empty)
                        neighbors.Add(new ShapeLocation(NewLocX, NewLocY));
                }
                catch { };
            }

            return neighbors;
        }

        public virtual bool IsMustDie(GameBoard board, ShapeLocation curLocation)
        {
            return GetNeighbours(board, curLocation).Count < MinRelationCount;
        }
    }
}
