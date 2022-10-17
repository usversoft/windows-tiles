namespace TilesControl
{
    partial class SizeCellForm
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
            this.cellsSizeXCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cellsSizeYCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cellsXCountLabel = new System.Windows.Forms.Label();
            this.cellsYCountLabel = new System.Windows.Forms.Label();
            this.OKbutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cellsSizeXCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellsSizeYCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cellsSizeXCountNumericUpDown
            // 
            this.cellsSizeXCountNumericUpDown.Location = new System.Drawing.Point(81, 12);
            this.cellsSizeXCountNumericUpDown.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.cellsSizeXCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cellsSizeXCountNumericUpDown.Name = "cellsSizeXCountNumericUpDown";
            this.cellsSizeXCountNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cellsSizeXCountNumericUpDown.TabIndex = 0;
            this.cellsSizeXCountNumericUpDown.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // cellsSizeYCountNumericUpDown
            // 
            this.cellsSizeYCountNumericUpDown.Location = new System.Drawing.Point(81, 45);
            this.cellsSizeYCountNumericUpDown.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.cellsSizeYCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cellsSizeYCountNumericUpDown.Name = "cellsSizeYCountNumericUpDown";
            this.cellsSizeYCountNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cellsSizeYCountNumericUpDown.TabIndex = 1;
            this.cellsSizeYCountNumericUpDown.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // cellsXCountLabel
            // 
            this.cellsXCountLabel.AutoSize = true;
            this.cellsXCountLabel.Location = new System.Drawing.Point(12, 14);
            this.cellsXCountLabel.Name = "cellsXCountLabel";
            this.cellsXCountLabel.Size = new System.Drawing.Size(60, 13);
            this.cellsXCountLabel.TabIndex = 2;
            this.cellsXCountLabel.Text = "Cells size X";
            // 
            // cellsYCountLabel
            // 
            this.cellsYCountLabel.AutoSize = true;
            this.cellsYCountLabel.Location = new System.Drawing.Point(12, 47);
            this.cellsYCountLabel.Name = "cellsYCountLabel";
            this.cellsYCountLabel.Size = new System.Drawing.Size(60, 13);
            this.cellsYCountLabel.TabIndex = 3;
            this.cellsYCountLabel.Text = "Cells size Y";
            // 
            // OKbutton
            // 
            this.OKbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbutton.Location = new System.Drawing.Point(45, 70);
            this.OKbutton.Name = "OKbutton";
            this.OKbutton.Size = new System.Drawing.Size(75, 23);
            this.OKbutton.TabIndex = 4;
            this.OKbutton.Text = "OK";
            this.OKbutton.UseVisualStyleBackColor = true;
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbutton.Location = new System.Drawing.Point(126, 71);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 5;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            // 
            // SizeCellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 105);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.cellsYCountLabel);
            this.Controls.Add(this.cellsXCountLabel);
            this.Controls.Add(this.cellsSizeYCountNumericUpDown);
            this.Controls.Add(this.cellsSizeXCountNumericUpDown);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SizeCellForm";
            this.Text = "Tile size";
            ((System.ComponentModel.ISupportInitialize)(this.cellsSizeXCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellsSizeYCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown cellsSizeXCountNumericUpDown;
        private System.Windows.Forms.NumericUpDown cellsSizeYCountNumericUpDown;
        private System.Windows.Forms.Label cellsXCountLabel;
        private System.Windows.Forms.Label cellsYCountLabel;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button Cancelbutton;
    }
}