using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameBoard
    {
        private TypeShape[,] fields;
        private int countCellWidth;
        private int countCellHeight;

        public GameBoard(int _countCellWidth, int _countCellHeight)
        {
            countCellWidth = _countCellWidth;
            countCellHeight = _countCellHeight;

            fields = new TypeShape[countCellHeight, countCellWidth];
            this.CellVisitor((x, y, shp) => fields[y, x] = TypeShape.Empty);
        }

        public void CellVisitor(CellVisitorHandler cellVisitorHandler)
        {
            for (int i = 0; i < countCellHeight; i++)
                for (int j = 0; j < countCellWidth; j++)
                    cellVisitorHandler(j, i, fields[i, j]);
        }

        public StepResult PutShape(TypeShape shape, int x, int y, bool checkEmpty = true)
        {
            if (x >= countCellWidth || y >= countCellHeight || x < 0 || y<0) return StepResult.Illegal;
            if (checkEmpty && fields[y, x] != TypeShape.Empty) return StepResult.Illegal;

            fields[y, x] = shape;
            return StepResult.Ok;
        }

        public TypeShape GetShape(int x, int y)
        {
            if (x >= countCellWidth || y >= countCellHeight || x < 0 || y < 0)
                new Exception("Координаты за пределом поля");

            return fields[y, x];
        }
        public GameBoard Clone()
        {
            GameBoard NewBoard = new GameBoard(countCellWidth, countCellHeight);
            for (int i = 0; i < countCellHeight; i++)
                for (int j = 0; j < countCellWidth; j++)
                    NewBoard.fields[i, j] = fields[i, j];

            return NewBoard;
        }
    }
}
