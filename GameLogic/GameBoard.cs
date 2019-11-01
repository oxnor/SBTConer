using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameBoard
    {
        // Двумерное игровое поле. Элемент - индекс в массиве Shapes
        private byte[,] fields; 
        private int countCellWidth;
        private int countCellHeight;

        private const byte EmptyCell = 255; // Признак пустой клетки

        // Фигуры в порядке ходов, первая - чей сейчас ход
        TypeShape[] Shapes;

        public GameBoard(byte[] binBoard, int _countCellWidth, int _countCellHeight)
        {
            countCellWidth = _countCellWidth;
            countCellHeight = _countCellHeight;

            fields = new byte[countCellHeight, countCellWidth];

            this.Deserialize(binBoard);
        }

        public TypeShape this[int y, int x]
        {
            get
            {
                return GetShape(x, y);
            }
        }

        public void CellVisitor(CellVisitorHandler cellVisitorHandler)
        {
            for (int y = 0; y < countCellHeight; y++)
                for (int x = 0; x < countCellWidth; x++)
                    cellVisitorHandler(x, y, Shapes[fields[y, x]]);
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

            return fields[y, x] != EmptyCell ? Shapes[fields[y, x]] : TypeShape.Empty;
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
            byte[] binBoard = new byte[Shapes.Length * 3];
            byte shapeIndex;

            for (byte y = 0; y < countCellHeight; y++)
                for (byte x = 0; x < countCellWidth; x++)
                {
                    shapeIndex = fields[y, x];
                    if (shapeIndex != EmptyCell)
                    {
                        binBoard[shapeIndex * 3] = x;
                        binBoard[shapeIndex * 3 + 1] = y;
                        binBoard[shapeIndex * 3 + 2] = (byte)Shapes[shapeIndex];
                    }
                }

            return binBoard;
        }

        public void Deserialize(byte[] binBoard)
        {
            if (binBoard.Length % 3 != 0)
                throw new Exception("Неверный формат доски");

            for (byte y = 0; y < countCellHeight; y++)
                for (byte x = 0; x < countCellWidth; x++)
                    fields[y, x] = EmptyCell;

            int shapeCount = binBoard.Length / 3;
            Shapes = new TypeShape[shapeCount];
            for (byte i = 0; i < shapeCount; i++)
            {
                Shapes[i] = (TypeShape)binBoard[i * 3 + 2];
                fields[binBoard[i * 3 + 1], binBoard[i * 3]] = i;
            }
        }
    }
}
