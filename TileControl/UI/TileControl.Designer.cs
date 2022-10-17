namespace TilesControl
{
    partial class TileControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.smallSquareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigSquareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigRectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.levelUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallSquareToolStripMenuItem,
            this.bigSquareToolStripMenuItem,
            this.smallRectangleToolStripMenuItem,
            this.bigRectangleToolStripMenuItem,
            this.customToolStripMenuItem,
            this.toolStripSeparator1,
            this.editTextToolStripMenuItem,
            this.editLinkToolStripMenuItem,
            this.toolStripSeparator2,
            this.levelUpToolStripMenuItem,
            this.levelDownToolStripMenuItem,
            this.toolStripSeparator3,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 264);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // smallSquareToolStripMenuItem
            // 
            this.smallSquareToolStripMenuItem.Name = "smallSquareToolStripMenuItem";
            this.smallSquareToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.smallSquareToolStripMenuItem.Text = "Small Square";
            this.smallSquareToolStripMenuItem.Click += new System.EventHandler(this.smallSquareToolStripMenuItem_Click);
            // 
            // bigSquareToolStripMenuItem
            // 
            this.bigSquareToolStripMenuItem.Name = "bigSquareToolStripMenuItem";
            this.bigSquareToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bigSquareToolStripMenuItem.Text = "Big Square";
            this.bigSquareToolStripMenuItem.Click += new System.EventHandler(this.bigSquareToolStripMenuItem_Click);
            // 
            // smallRectangleToolStripMenuItem
            // 
            this.smallRectangleToolStripMenuItem.Name = "smallRectangleToolStripMenuItem";
            this.smallRectangleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.smallRectangleToolStripMenuItem.Text = "Small Rectangle";
            this.smallRectangleToolStripMenuItem.Click += new System.EventHandler(this.smallRectangleToolStripMenuItem_Click);
            // 
            // bigRectangleToolStripMenuItem
            // 
            this.bigRectangleToolStripMenuItem.Name = "bigRectangleToolStripMenuItem";
            this.bigRectangleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bigRectangleToolStripMenuItem.Text = "Big Rectangle";
            this.bigRectangleToolStripMenuItem.Click += new System.EventHandler(this.bigRectangleToolStripMenuItem_Click);
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.customToolStripMenuItem.Text = "Custom";
            this.customToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // editTextToolStripMenuItem
            // 
            this.editTextToolStripMenuItem.Name = "editTextToolStripMenuItem";
            this.editTextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editTextToolStripMenuItem.Text = "Edit text";
            this.editTextToolStripMenuItem.Click += new System.EventHandler(this.editTextToolStripMenuItem_Click);
            // 
            // editLinkToolStripMenuItem
            // 
            this.editLinkToolStripMenuItem.Name = "editLinkToolStripMenuItem";
            this.editLinkToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editLinkToolStripMenuItem.Text = "Edit link";
            this.editLinkToolStripMenuItem.Click += new System.EventHandler(this.editLinkToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // levelUpToolStripMenuItem
            // 
            this.levelUpToolStripMenuItem.Name = "levelUpToolStripMenuItem";
            this.levelUpToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.levelUpToolStripMenuItem.Text = "Level up";
            this.levelUpToolStripMenuItem.Click += new System.EventHandler(this.levelUpToolStripMenuItem_Click);
            // 
            // levelDownToolStripMenuItem
            // 
            this.levelDownToolStripMenuItem.Name = "levelDownToolStripMenuItem";
            this.levelDownToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.levelDownToolStripMenuItem.Text = "Level down";
            this.levelDownToolStripMenuItem.Click += new System.EventHandler(this.levelDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);

            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // TileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Name = "TileControl";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.Controls.Add(pictureBox1);
            this.Controls.Add(pictureBox2);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem smallSquareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigSquareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallRectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigRectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem editTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem levelUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.PictureBox pictureBox2;
    }
}
