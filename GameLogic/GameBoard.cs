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
        static private byte[] EmptyByteArray = new byte[0];
        static private TypeShape[] EmptyShapeArray = new TypeShape[0];

        // Фигуры в порядке ходов, первая - чей сейчас ход
        TypeShape[] Shapes = EmptyShapeArray;

        public GameBoard(int _countCellWidth, int _countCellHeight, byte[] binBoard = null)
        {
            if (binBoard == null) binBoard = EmptyByteArray;

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
                    cellVisitorHandler(x, y, this[y, x]);
        }

        public StepResult PutShape(TypeShape shape, int x, int y, bool checkEmpty = true)
        {
            if (x >= countCellWidth || y >= countCellHeight || x < 0 || y < 0) return StepResult.Illegal;
            if (checkEmpty && fields[y, x] != EmptyCell && shape!= TypeShape.Empty) return StepResult.Illegal;

            if (shape == TypeShape.Empty)
                return RemoveShape(x, y);

            TypeShape[] newShapes = new TypeShape[Shapes.Length + 1];
            byte newIndex = (byte) (newShapes.Length - 1);
            Shapes.CopyTo(newShapes, 0);
            newShapes[newIndex] = (TypeShape)shape;
            fields[y, x] = newIndex;
            Shapes = newShapes;

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
            byte shapeIndex = fields[y, x];
            if (shapeIndex == EmptyCell) return StepResult.Ok;

            TypeShape[] newShapes = new TypeShape[Shapes.Length - 1];
            int i, ni = 0;
            for (i = 0; i < Shapes.Length; i++)
                if (i != shapeIndex)
                    newShapes[ni++] = Shapes[i];

            Shapes = newShapes;
            fields[y, x] = EmptyCell;

            return StepResult.Ok;
        }

        public StepResult MoveShape(int x0, int y0, int x1, int y1)
        {
            if ((x0 >= countCellWidth || y0 >= countCellHeight || x0 < 0 || y0 < 0)
                || (x1 >= countCellWidth || y1 >= countCellHeight || x1 < 0 || y1 < 0)
               )
                return StepResult.Illegal;

            if (fields[y1, x1] != EmptyCell) return StepResult.Illegal;

            fields[y1, x1] = fields[y0, x0];
            fields[y0, x0] = EmptyCell;

            return StepResult.Ok;
        }

        public GameBoard Clone()
        {
            GameBoard newBoard = new GameBoard(countCellWidth, countCellHeight);
            int x, y;
            for (y = 0; y < countCellHeight; y++)
                for (x = 0; x < countCellWidth; x++)
                    newBoard.fields[y, x] = fields[y, x];

            newBoard.Shapes = new TypeShape[this.Shapes.Length];
            this.Shapes.CopyTo(newBoard.Shapes, 0);

            return newBoard;
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
            for (byte y = 0; y < countCellHeight; y++)
                for (byte x = 0; x < countCellWidth; x++)
                    fields[y, x] = EmptyCell;

            if (binBoard.Length == 0) return;

            if (binBoard.Length % 3 != 0)
                throw new Exception("Неверный формат доски");

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
