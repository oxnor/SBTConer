using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;
using NLog;

namespace task
{
    public partial class BoardControl : UserControl
    {
        private Pen penBlack;
        private Pen penRelation;
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
        Engine gameEngine = new Engine();

        static Logger _logger = LogManager.GetCurrentClassLogger();

        public BoardControl()
        {
            InitializeComponent();

            Init(defaultCountCellWidth, defaultCountCellHeight);
        }

        public void Init(int _countCellWidth, int _countCellHeight)
        {
            this.countCellWidth = _countCellWidth == 0 ? defaultCountCellWidth : _countCellWidth;
            this.countCellHeight = _countCellHeight == 0 ? defaultCountCellHeight : _countCellHeight;

            this.CalculateSize();

            this.InitializeGraphics();

            gameBoard = new GameBoard(countCellWidth, countCellHeight);
            this.Invalidate();
        }

        private void InitializeGraphics()
        {
            this.penBlack = new Pen(Color.Black);
            this.penBlack.Width = 1;

            this.penRelation = new Pen(Color.Lime);
            this.penRelation.Width = 1;
            this.penRelation.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        }



        public void DrawBoard(Graphics graphics)
        {
            _logger.Trace("DrawBoard");
            int i;
            int j;
            int currentY = 0;

            graphics.FillRectangle(SystemBrushes.Control, new Rectangle(0, 0, this.boardSizeWidth, this.boardSizeHeight));

            for (i = 0; i <= countCellHeight; i++)
            {
                graphics.DrawLine(penBlack, 0, currentY, this.boardSizeWidth, currentY);
                currentY += this.cellSize;
            }

            int currentX = 0;

            for (i = 0; i <= countCellWidth; i++)
            {
                graphics.DrawLine(penBlack, currentX, 0, currentX, this.boardSizeHeight);
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
                                this.AddCircle(graphics, x, y);
                                break;
                            }
                        case TypeShape.Triangle:
                            {
                                this.AddTriangle(graphics, x, y);
                                break;
                            }
                        case TypeShape.Square:
                            {
                                this.AddSquare(graphics, x, y);
                                break;
                            }
                    }

                    int nx = 0, ny = 0;
                    foreach (ShapeLocation neighbourLocation in gameBoard.GetNeighbours(new ShapeLocation(cellX, cellY)))
                    {
                        this.CellToCoords(neighbourLocation.X, neighbourLocation.Y, ref nx, ref ny);
                        graphics.DrawLine(penRelation, x, y, nx, ny);
                    }
                }
            });

            gameBoard.CellVisitor((cellX, cellY, curShape) =>
            {
                if (curShape != TypeShape.Empty)
                {
                    int x = 0, y = 0;
                    this.CellToCoords(cellX, cellY, ref x, ref y);

                    int nx = 0, ny = 0;
                    foreach (ShapeLocation neighbourLocation in gameBoard.GetNeighbours(new ShapeLocation(cellX, cellY)))
                    {
                        this.CellToCoords(neighbourLocation.X, neighbourLocation.Y, ref nx, ref ny);
                        graphics.DrawLine(penRelation, x, y, nx, ny);
                    }
                }
            });
        }

        public void AddCircle(Graphics graphics, int x, int y)
        {
            graphics.FillEllipse(Brushes.Chocolate, x - this.halfShapeSize,
                y - this.halfShapeSize, this.shapeSize, this.shapeSize);
        }

        public void AddTriangle(Graphics graphics, int x, int y)
        {
            Point[] pointsOfTriangle = new Point[]
            {
                new Point(x-this.halfShapeSize,y+this.halfShapeSize),
                new Point(x,y-this.halfShapeSize),
                new Point(x+this.halfShapeSize,y+this.halfShapeSize),
            };

            graphics.FillPolygon(Brushes.Blue, pointsOfTriangle);
        }

        public void AddSquare(Graphics graphics, int x, int y)
        {
            graphics.FillRectangle(Brushes.Green, x - this.halfShapeSize,
                y - this.halfShapeSize, this.shapeSize, this.shapeSize);
        }

        private void CalculateSize()
        {
            int cellSizeWidth = this.Width / this.countCellWidth;
            int cellSizeHeight = this.Height / countCellHeight;
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
            this.Invalidate();
        }

        public string Serialize()
        {
            return Convert.ToBase64String(gameBoard.Serialize());
            this.Invalidate();
        }

        public void Deserialize(string strBoard)
        {
            gameBoard.Deserialize(Convert.FromBase64String(strBoard));
            this.Invalidate();
        }

        public void MakeStep()
        {
            gameBoard = gameEngine.GetNextPosition(gameBoard);
            this.Invalidate();
        }

        private void BoardControl_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);
        }
    }
}
