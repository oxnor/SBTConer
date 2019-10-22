using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GameLogic;

namespace task
{
    class Board
    {
        private MainForm mainForm;
        private Graphics graphics;
        private Pen penBlack;
        private int cellSize;
        private int shapeSize;
        private int halfShapeSize;
        private int countCellWidth;
        private int countCellHeight;
        private int boardSizeWidth;
        private int boardSizeHeight;
        private const int defaultCountCellWidth = 25;
        private const int defaultCountCellHeight = 12;

        GameBoard gameBoard;
        

        public Board(MainForm _mainForm, int _countCellWidth, int _countCellHeight)
        {
            this.mainForm = _mainForm;
            this.countCellWidth = _countCellWidth==0 ? Board.defaultCountCellWidth : _countCellWidth;
            this.countCellHeight = _countCellHeight == 0 ? Board.defaultCountCellHeight : _countCellHeight;
            
            this.CalculateSize();
            
            this.InitializeGraphics();

            gameBoard = new GameBoard(countCellWidth, countCellHeight);
        }

        private void InitializeGraphics()
        {
            this.graphics = this.mainForm.BoardPanel.CreateGraphics();
            this.penBlack = new Pen(Color.Black);
            this.penBlack.Width = 1;
        }

        public void DrawBoard()
        {
            int i;
            int j;
            int currentY = 0;
            this.graphics = this.mainForm.BoardPanel.CreateGraphics();
            this.graphics.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, this.boardSizeWidth, this.boardSizeHeight));

            for (i = 0; i <= countCellHeight; i++)
            {
                this.graphics.DrawLine(penBlack, 0, currentY, this.boardSizeWidth, currentY);
                currentY += this.cellSize;
            }

            int currentX = 0;

            for (i = 0; i <= countCellWidth; i++)
            {
                this.graphics.DrawLine(penBlack, currentX, 0, currentX, this.boardSizeHeight);
                currentX += this.cellSize;
            }

            gameBoard.CellVisitor((cellX, cellY, curShape) =>
            {
                if (curShape != TypeShape.Empty)
                {
                    int x = 0, y = 0;
                    this.CellToCoords(cellX, cellY, ref x, ref y);

                    switch (curShape)
                    {
                        case TypeShape.Circle:
                            {
                                this.AddCircle(x, y);
                                break;
                            }
                        case TypeShape.Triangle:
                            {
                                this.AddTriangle(x, y);
                                break;
                            }
                        case TypeShape.Square:
                            {
                                this.AddSquare(x, y);
                                break;
                            }
                    }
                }
            });
        }

        public void AddCircle(int x, int y)
        {
            this.graphics.FillEllipse(Brushes.Chocolate, x - this.halfShapeSize, 
                y - this.halfShapeSize, this.shapeSize, this.shapeSize);
        }

        public void AddTriangle(int x, int y)
        {
            Point[] pointsOfTriangle = new Point[]
            {
                new Point(x-this.halfShapeSize,y+this.halfShapeSize),
                new Point(x,y-this.halfShapeSize),
                new Point(x+this.halfShapeSize,y+this.halfShapeSize),
            };
            
            this.graphics.FillPolygon(Brushes.Blue, pointsOfTriangle);
        }

        public void AddSquare(int x, int y)
        {
            this.graphics.FillRectangle(Brushes.Green, x - this.halfShapeSize,
                y - this.halfShapeSize, this.shapeSize, this.shapeSize);
        }

        private void CalculateSize()
        {
            int cellSizeWidth = this.mainForm.BoardPanel.Width / this.countCellWidth;
            int cellSizeHeight = this.mainForm.BoardPanel.Height / countCellHeight;
            this.cellSize = cellSizeWidth < cellSizeHeight ? cellSizeWidth : cellSizeHeight;
            this.shapeSize = this.cellSize / 2;
            this.halfShapeSize = this.shapeSize / 2;

            this.boardSizeWidth = this.countCellWidth * this.cellSize;
            this.boardSizeHeight = this.countCellHeight * this.cellSize;
        }

        private void CoordsToCell(int x, int y, ref int cellX, ref int cellY)
        {
            cellX = x / cellSize;
            cellY = y / cellSize;
        }

        private void CellToCoords(int cellX, int cellY, ref int x, ref int y)
        {
            x = cellX * cellSize + cellSize / 2;
            y = cellY * cellSize + cellSize / 2;
        }

        public void PutShape(TypeShape shape, int x, int y)
        {
            int cellX = 0;
            int cellY = 0;
            this.CoordsToCell(x, y, ref cellX, ref cellY);
            if (cellX >= countCellWidth || cellY >= countCellHeight) return;
            gameBoard.PutShape(shape, cellX, cellY);
            mainForm.Invalidate();
        }
    }
}
