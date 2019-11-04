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
        private ShapeLocation currentShapeLocation;

        private const byte EmptyCell = 255; // Признак пустой клетки
        static private byte[] EmptyByteArray = new byte[0];
        static private TypeShape[] EmptyShapeArray = new TypeShape[0];

        // Фигуры в порядке ходов, первая - чей сейчас ход
        TypeShape[] Shapes = EmptyShapeArray;

        // При удалении текущей фигуры происходит неявная передача хода. 
        // При формировании позиции следующего хода не нужно делать сдвиг
        private bool IsNextStepped = false;

        public GameBoard(int _countCellWidth, int _countCellHeight, byte[] binBoard = null)
        {
            if (binBoard == null) binBoard = EmptyByteArray;

            countCellWidth = _countCellWidth;
            countCellHeight = _countCellHeight;

            fields = new byte[countCellHeight, countCellWidth];

            this.Deserialize(binBoard);
        }

        public TypeShape CurrentShape
        {
            get { return Shapes[0]; }
        }

        public ShapeLocation CurrentShapeLocation
        {
            get { return currentShapeLocation; }
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

            if (newIndex == 0)
                currentShapeLocation = new ShapeLocation(x, y);

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
            byte removedShapeIndex = fields[y, x];
            if (removedShapeIndex == EmptyCell) return StepResult.Ok;

            if (removedShapeIndex == 0)
                IsNextStepped = true;

            TypeShape[] newShapes = new TypeShape[Shapes.Length - 1];
            int i, ni = 0;
            for (i = 0; i < Shapes.Length; i++)
                if (i != removedShapeIndex)
                    newShapes[ni++] = Shapes[i];

            Shapes = newShapes;
            fields[y, x] = EmptyCell;

            for (y = 0; y < countCellHeight; y++)
                for (x = 0; x < countCellWidth; x++)
                {
                    ni = fields[x, y];
                    if (ni != EmptyCell)
                    {
                        if (ni > removedShapeIndex) fields[x, y]--;

                        // Если удалили текущую фигуру
                        if (removedShapeIndex == 0 && ni == 1)
                            currentShapeLocation = new ShapeLocation(x, y);
                    }
                }

            return StepResult.Ok;
        }

        public StepResult MoveShape(int x, int y)
        {
            if (x >= countCellWidth || y >= countCellHeight || x < 0 || y < 0)
                return StepResult.Illegal;

            if (fields[y, x] != EmptyCell) return StepResult.Illegal;

            fields[y, x] = (byte)(Shapes.Length - 1);
            fields[currentShapeLocation.X, currentShapeLocation.Y] = EmptyCell;
            currentShapeLocation = new ShapeLocation(x, y);

            TypeShape curShape = Shapes[0];

            int i;
            for (i = 0; i < Shapes.Length - 2; i++)
                Shapes[i] = Shapes[i + 1];

            Shapes[Shapes.Length - 1] = curShape;

            for (y = 0; y < countCellHeight; y++)
                for (x = 0; x < countCellWidth; x++)
                {
                    i = fields[x, y];
                    if (i != EmptyCell)
                    {
                        if (i < Shapes.Length - 1) fields[x, y]--;
                    }
                }

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
            newBoard.currentShapeLocation = new ShapeLocation(currentShapeLocation.X, currentShapeLocation.Y);

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

            currentShapeLocation = new ShapeLocation(binBoard[0], binBoard[1]);
        }

        public int Score(ShapeLocation currentLocation)
        {
            double dx = countCellWidth - 1 - currentShapeLocation.X;
            double dy = countCellHeight - 1 - currentShapeLocation.Y;
            double maxd = Math.Sqrt(countCellWidth * countCellWidth + countCellHeight + countCellHeight);
            return (int)(maxd - Math.Sqrt(dx * dx + dy * dy));
        }
    }
}
