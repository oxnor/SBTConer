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
            if (x >= countCellWidth || y >= countCellHeight || x < 0 || y < 0) return StepResult.Illegal;
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

        public StepResult RemoveShape(int x, int y)
        {
            return PutShape(TypeShape.Empty, x, y, checkEmpty:false);
        }

        public GameBoard Clone()
        {
            GameBoard NewBoard = new GameBoard(countCellWidth, countCellHeight);
            for (int i = 0; i < countCellHeight; i++)
                for (int j = 0; j < countCellWidth; j++)
                    NewBoard.fields[i, j] = fields[i, j];

            return NewBoard;
        }

        public byte[] Serialize()
        {
            List<byte[]> shapeList = new List<byte[]>();
            CellVisitor((x, y, shp) =>
            {
                if (fields[y, x] != TypeShape.Empty)
                    shapeList.Add(new byte[3] { (byte)x, (byte)y, (byte)fields[y, x] });
            });

            byte[] binBoard = new byte[shapeList.Count * 3];
            for (int i = 0; i< shapeList.Count; i++)
            {
                binBoard[i * 3] = shapeList[i][0];
                binBoard[i * 3 + 1] = shapeList[i][1];
                binBoard[i * 3 + 2] = shapeList[i][2];
            }

            return binBoard;
        }

        public void Deserialize(byte[] binBoard)
        {
            if (binBoard.Length % 3 != 0)
                throw new Exception("Неверный формат доски");

            int shapeCount = binBoard.Length % 3;
            for (int i = 0; i < shapeCount; i++)
                fields[binBoard[i * 3 + 1], binBoard[i * 3]] = (TypeShape)binBoard[i * 3 + 2];
        }
    }
}
