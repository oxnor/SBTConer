using GameLogic.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Engine
    {
        GameBoard Board;
        Dictionary<TypeShape, IShape> ShapeSet = new Dictionary<TypeShape, IShape>();

        public Engine()
        {
            IShape shape;

            shape = new Circle();
            ShapeSet.Add(shape.typeShape, shape);

            shape = new Square();
            ShapeSet.Add(shape.typeShape, shape);

            shape = new Triangle();
            ShapeSet.Add(shape.typeShape, shape);
        }

        public GameBoard GetNextPosition(GameBoard curBoard)
        {
            IShape curShape = ShapeSet[curBoard.CurrentShape];
            Step step0 = new Step(curBoard, StepResult.Ok, 0);
            List<Step> steps = curShape.GetNextSteps(step0);
            Step bestStep = null;
            foreach(Step step in steps)
            {
                if (bestStep == null)
                {
                    bestStep = step;
                }
                else
                {
                    if (step.Score > bestStep.Score)
                        bestStep = step;
                }
            }

            return bestStep?.Board;
        }
    }
}
