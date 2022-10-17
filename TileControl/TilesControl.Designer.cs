namespace TilesControl
{
    partial class TilesControl
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
            this.addTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.viewPanel = new System.Windows.Forms.Panel();
            this.editTextBox = new TextBoxPadding();
            this.toolboxPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.viewTextLabel = new System.Windows.Forms.Label();
            this.imagePictureBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.viewPanel.SuspendLayout();
            this.toolboxPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTileToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // addTileToolStripMenuItem
            // 
            this.addTileToolStripMenuItem.Name = "addTileToolStripMenuItem";
            this.addTileToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addTileToolStripMenuItem.Text = "Add Tile";
            this.addTileToolStripMenuItem.Click += new System.EventHandler(this.addTileToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // viewPanel
            // 
            this.viewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewPanel.BackColor = System.Drawing.Color.YellowGreen;
            this.viewPanel.Controls.Add(this.editTextBox);            
            this.viewPanel.Controls.Add(this.toolboxPanel);
            this.viewPanel.Controls.Add(this.imagePictureBox);
            this.viewPanel.Location = new System.Drawing.Point(79, 0);
            this.viewPanel.Name = "viewPanel";
            this.viewPanel.Size = new System.Drawing.Size(300, 312);
            this.viewPanel.TabIndex = 2;
            // 
            // editTextBox
            // 
            this.editTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.editTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.editTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editTextBox.Location = new System.Drawing.Point(0, 38);
            this.editTextBox.Name = "editTextBox";
            this.editTextBox.Size = new System.Drawing.Size(300, 74);
            this.editTextBox.TabIndex = 5;
            this.editTextBox.Visible = false;
            // 
            // toolbaxPanel
            // 
            this.toolboxPanel.BackColor = System.Drawing.SystemColors.Control;
            this.toolboxPanel.Controls.Add(this.cancelButton);
            this.toolboxPanel.Controls.Add(this.saveButton);
            this.toolboxPanel.Controls.Add(this.editButton);
            this.toolboxPanel.Controls.Add(this.closeButton);
            this.toolboxPanel.Controls.Add(this.viewTextLabel);
            this.toolboxPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolboxPanel.Location = new System.Drawing.Point(0, 0);
            this.toolboxPanel.Name = "toolbaxPanel";
            this.toolboxPanel.Size = new System.Drawing.Size(300, 38);
            this.toolboxPanel.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.Control;
            this.cancelButton.FlatAppearance.BorderSize = 0;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(135, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(52, 32);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Location = new System.Drawing.Point(77, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(52, 32);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.SystemColors.Control;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Image = global::TilesControl.Properties.Resources.icons8_edit_24_1_;
            this.editButton.Location = new System.Drawing.Point(39, 3);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(32, 32);
            this.editButton.TabIndex = 3;
            this.toolTip1.SetToolTip(this.editButton, "Edit");
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Image = global::TilesControl.Properties.Resources.close4;
            this.closeButton.Location = new System.Drawing.Point(265, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(32, 32);
            this.closeButton.TabIndex = 0;
            this.toolTip1.SetToolTip(this.closeButton, "Close");
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // viewTexeLabel
            // 
            this.viewTextLabel.AutoSize = true;
            this.viewTextLabel.Location = new System.Drawing.Point(3, 13);
            this.viewTextLabel.Name = "viewTexeLabel";
            this.viewTextLabel.Size = new System.Drawing.Size(30, 13);
            this.viewTextLabel.TabIndex = 2;
            this.viewTextLabel.Text = "View";
            // 
            // imagePictureBox
            // 
            this.imagePictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.imagePictureBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.imagePictureBox.Location = new System.Drawing.Point(0, 112);
            this.imagePictureBox.Name = "imagePictureBox";
            this.imagePictureBox.Size = new System.Drawing.Size(300, 200);
            this.imagePictureBox.TabIndex = 6;
            this.imagePictureBox.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            // 
            // TilesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.viewPanel);
            this.Controls.Add(this.label1);
            this.Name = "TilesControl";
            this.Size = new System.Drawing.Size(379, 312);
            this.contextMenuStrip1.ResumeLayout(false);
            this.viewPanel.ResumeLayout(false);
            this.viewPanel.PerformLayout();
            this.toolboxPanel.ResumeLayout(false);
            this.toolboxPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addTileToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel viewPanel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label viewTextLabel;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Panel toolboxPanel;
        private TextBoxPadding editTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.PictureBox imagePictureBox;
    }
}
