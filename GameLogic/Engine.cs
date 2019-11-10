using GameLogic.Shapes;
using GameLogic.StepTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Engine
    {
        Dictionary<TypeShape, IShape> ShapeSet = new Dictionary<TypeShape, IShape>();
        int MaxDeep = 1;

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
            Map map = new Map(curBoard.Locations().Select(l => new MapElement(l)).ToArray());
            return GetBestStep(curBoard, map);
        }

        public GameBoard GetNextPosition0(GameBoard curBoard)
        {
            IShape curShape = ShapeSet[curBoard.CurrentShape];
            Step step0 = new Step(curBoard, curBoard.CurrentShapeLocation, StepResult.Ok, 0);
            List<Step> steps = curShape.GetNextSteps(step0);
            Step bestStep = null;
            foreach (Step step in steps)
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

        public GameBoard GetBestStep(GameBoard curBoard, Map map)
        {
            IShape curShape = ShapeSet[curBoard.CurrentShape];
            Step step0 = new Step(curBoard, curBoard.CurrentShapeLocation, StepResult.Ok, 0);
            List<Step> steps = curShape.GetNextSteps(step0);
            Map newMap = null;
            MapElement curMapElement = map.FindNodeFor(curBoard.CurrentShapeLocation);
            MapElement newMapElement;
            foreach (Step step in steps)
            {
                if (curMapElement.Deep < MaxDeep)
                {
                    newMap = map.MoveShape(curBoard.CurrentShapeLocation, step.NewLocation);
                    newMapElement = newMap.FindNodeFor(step.NewLocation);
                    newMapElement.Deep = curMapElement.Deep + 1;

                    if (!step.Board.IsEmpty)
                        GetBestStep(step.Board, newMap);

                    if (newMapElement.BestStep != null)
                    {
                        if (curMapElement.BestStep == null)
                        {
                            curMapElement.BestStep = step;
                        }
                        else
                        {
                            if (newMapElement.BestStep.Score > curMapElement.BestStep.Score)
                                curMapElement.BestStep = step;
                        }
                    }
                }
                else
                {
                    if (curMapElement.BestStep == null)
                    {
                        curMapElement.BestStep = step;
                    }
                    else
                    {
                        if (step.Score > curMapElement.BestStep.Score)
                            curMapElement.BestStep = step;
                    }
                }
            }

            return curMapElement?.BestStep?.Board;
        }
    }
}
