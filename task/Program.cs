using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace task
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameBoard board = new GameBoard(3, 3);
            board.PutShape(TypeShape.Circle, 0, 0);
            Step step = new Step(board, new ShapeLocation(0, 0), StepResult.Ok);
            IShape Circle = new GameLogic.Shapes.Circle();
            List<Step> Steps = Circle.GetNextSteps(step);
            //Assert.AreEqual(8, Steps.Count);

            Application.Run(new MainForm());
        }
    }
}
