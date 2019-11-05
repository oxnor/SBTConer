namespace task
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newBoard = new System.Windows.Forms.ToolStripMenuItem();
            this.addShape = new System.Windows.Forms.ToolStripMenuItem();
            this.addCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.addTriangle = new System.Windows.Forms.ToolStripMenuItem();
            this.addSquare = new System.Windows.Forms.ToolStripMenuItem();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.nexStep = new System.Windows.Forms.Button();
            this.txtBoard = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.board = new task.BoardControl();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1856, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItem
            // 
            this.menuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBoard,
            this.addShape,
            this.exit});
            this.menuItem.Name = "menuItem";
            this.menuItem.Size = new System.Drawing.Size(63, 24);
            this.menuItem.Text = "Меню";
            // 
            // newBoard
            // 
            this.newBoard.Name = "newBoard";
            this.newBoard.Size = new System.Drawing.Size(203, 26);
            this.newBoard.Text = "Новая доска";
            this.newBoard.Click += new System.EventHandler(this.NewBoard_Click);
            // 
            // addShape
            // 
            this.addShape.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCircle,
            this.addTriangle,
            this.addSquare});
            this.addShape.Name = "addShape";
            this.addShape.Size = new System.Drawing.Size(203, 26);
            this.addShape.Text = "Добавить фигуру";
            // 
            // addCircle
            // 
            this.addCircle.Name = "addCircle";
            this.addCircle.Size = new System.Drawing.Size(172, 26);
            this.addCircle.Text = "Круг";
            this.addCircle.Click += new System.EventHandler(this.AddCircle_Click);
            // 
            // addTriangle
            // 
            this.addTriangle.Name = "addTriangle";
            this.addTriangle.Size = new System.Drawing.Size(172, 26);
            this.addTriangle.Text = "Треугольник";
            this.addTriangle.Click += new System.EventHandler(this.AddTriangle_Click);
            // 
            // addSquare
            // 
            this.addSquare.Name = "addSquare";
            this.addSquare.Size = new System.Drawing.Size(172, 26);
            this.addSquare.Text = "Квадрат";
            this.addSquare.Click += new System.EventHandler(this.AddSquare_Click);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(203, 26);
            this.exit.Text = "Выход";
            this.exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // nexStep
            // 
            this.nexStep.Location = new System.Drawing.Point(398, 15);
            this.nexStep.Margin = new System.Windows.Forms.Padding(4);
            this.nexStep.Name = "nexStep";
            this.nexStep.Size = new System.Drawing.Size(1456, 29);
            this.nexStep.TabIndex = 1;
            this.nexStep.Text = "Следующий шаг";
            this.nexStep.UseVisualStyleBackColor = true;
            this.nexStep.Click += new System.EventHandler(this.NexStep_Click);
            // 
            // txtBoard
            // 
            this.txtBoard.Location = new System.Drawing.Point(56, 18);
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.Size = new System.Drawing.Size(214, 22);
            this.txtBoard.TabIndex = 3;
            this.txtBoard.Text = "AQABAAEB";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(276, 15);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(115, 29);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Применить";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(5, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(45, 27);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "R";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // board
            // 
            this.board.Dock = System.Windows.Forms.DockStyle.Fill;
            this.board.Location = new System.Drawing.Point(0, 28);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(1856, 818);
            this.board.TabIndex = 7;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnRefresh);
            this.pnlBottom.Controls.Add(this.nexStep);
            this.pnlBottom.Controls.Add(this.txtBoard);
            this.pnlBottom.Controls.Add(this.btnApply);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 846);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1856, 56);
            this.pnlBottom.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1856, 902);
            this.Controls.Add(this.board);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlBottom);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem;
        private System.Windows.Forms.ToolStripMenuItem newBoard;
        private System.Windows.Forms.ToolStripMenuItem addShape;
        private System.Windows.Forms.ToolStripMenuItem addCircle;
        private System.Windows.Forms.ToolStripMenuItem addTriangle;
        private System.Windows.Forms.ToolStripMenuItem addSquare;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.Button nexStep;
        private System.Windows.Forms.TextBox txtBoard;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRefresh;
        private BoardControl board;
        private System.Windows.Forms.Panel pnlBottom;
    }
}

