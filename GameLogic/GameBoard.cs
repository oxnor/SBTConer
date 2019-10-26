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

        public void PutShape(TypeShape shape, int x, int y)
        {
            if (x >= countCellWidth || y >= countCellHeight) return;
            fields[y, x] = shape;
        }

    }
}
