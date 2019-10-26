using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using GameLogic;
using GameLogic.Shapes;


namespace GameLogicTests
{
    [TestClass]
    public class CircleTests
    {
        [TestMethod]
        public void TestGetNextStepsCenter()
        {
            GameBoard board = new GameBoard(3, 3);
            board.PutShape(TypeShape.Circle, 1, 1);
            Step step = new Step(board, new ShapeLocation(1, 1), StepResult.Ok);
            IShape Circle = new Circle();
            List<Step> Steps = Circle.GetNextSteps(step);
            Assert.AreEqual(8, Steps.Count);
        }
    }
}
