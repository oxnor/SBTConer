using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GameLogic;
using NLog;

namespace task
{
    public partial class MainForm : Form , IMainForm
    {
        static Logger _logger = LogManager.GetCurrentClassLogger();

        private TypeShape currentShapeForAdd;
       
        public MainForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            this.currentShapeForAdd = TypeShape.Empty;
            this.CreateNewBoard(0, 0);
            _logger.Info("Конструктор главной формы");
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewBoard_Click(object sender, EventArgs e)
        {
            NewBoardForm dialogForm = new NewBoardForm(this);
            dialogForm.ShowDialog();
        }

        public void CreateNewBoard(int width, int height)
        { 
            //this.boardPanel.CreateGraphics().Clear(this.boardPanel.BackColor);
            this.board.Init(width, height);
            this.EndListeningMouse();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            _logger.Trace("MainForm_Paint");
        }

        private void AddCircle_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(TypeShape.Circle);
        }

        private void AddTriangle_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(TypeShape.Triangle);
        }

        private void AddSquare_Click(object sender, EventArgs e)
        {
            this.StartListeningMouse(TypeShape.Square);
        }

        private void StartListeningMouse(TypeShape typeShapeForAdd)
        {
            this.currentShapeForAdd = typeShapeForAdd;
            this.board.MouseClick += new MouseEventHandler(BoardPanel_MouseClick);
        }

        private void EndListeningMouse()
        {
            this.currentShapeForAdd = TypeShape.Empty;
            this.board.MouseClick -= new MouseEventHandler(BoardPanel_MouseClick);
        }

        private void BoardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.board != null)
                board.PutShape(this.currentShapeForAdd, e.X, e.Y);
        }

        private void NexStep_Click(object sender, EventArgs e)
        {
            this.EndListeningMouse();
            board.MakeStep();
            board.Invalidate();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            board.Deserialize(txtBoard.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBoard.Text = board.Serialize();
        }

        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            _logger.Trace("boardPanel_Paint");
            if (this.board != null) this.board.DrawBoard(e.Graphics);
        }
    }


}
