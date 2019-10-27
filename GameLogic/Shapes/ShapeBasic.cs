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

        public abstract TypeShape typeShape { get; }

        public abstract TypeShape MinRelationCount { get; }

        public abstract List<Step> GetNextSteps(Step curStep);

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
                        neighbors.Add(new ShapeLocation((byte)NewLocX, (byte)NewLocY));
                }
                catch { };
            }

            return neighbors;
        }

        public abstract bool IsMustDie();
    }
}
