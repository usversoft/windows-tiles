namespace TilesControl
{
    partial class SizeForm
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
            this.cellsXCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cellsYCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.cellsXCountLabel = new System.Windows.Forms.Label();
            this.cellsYCountLabel = new System.Windows.Forms.Label();
            this.OKbutton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cellsXCountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellsYCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // cellsXCountNumericUpDown
            // 
            this.cellsXCountNumericUpDown.Location = new System.Drawing.Point(81, 12);
            this.cellsXCountNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cellsXCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cellsXCountNumericUpDown.Name = "cellsXCountNumericUpDown";
            this.cellsXCountNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cellsXCountNumericUpDown.TabIndex = 0;
            this.cellsXCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cellsYCountNumericUpDown
            // 
            this.cellsYCountNumericUpDown.Location = new System.Drawing.Point(81, 45);
            this.cellsYCountNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cellsYCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cellsYCountNumericUpDown.Name = "cellsYCountNumericUpDown";
            this.cellsYCountNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.cellsYCountNumericUpDown.TabIndex = 1;
            this.cellsYCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cellsXCountLabel
            // 
            this.cellsXCountLabel.AutoSize = true;
            this.cellsXCountLabel.Location = new System.Drawing.Point(12, 14);
            this.cellsXCountLabel.Name = "cellsXCountLabel";
            this.cellsXCountLabel.Size = new System.Drawing.Size(53, 13);
            this.cellsXCountLabel.TabIndex = 2;
            this.cellsXCountLabel.Text = "Cells by X";
            // 
            // cellsYCountLabel
            // 
            this.cellsYCountLabel.AutoSize = true;
            this.cellsYCountLabel.Location = new System.Drawing.Point(12, 47);
            this.cellsYCountLabel.Name = "cellsYCountLabel";
            this.cellsYCountLabel.Size = new System.Drawing.Size(53, 13);
            this.cellsYCountLabel.TabIndex = 3;
            this.cellsYCountLabel.Text = "Cells by Y";
            // 
            // OKbutton
            // 
            this.OKbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKbutton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKbutton.Location = new System.Drawing.Point(45, 71);
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
            // SizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 105);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.OKbutton);
            this.Controls.Add(this.cellsYCountLabel);
            this.Controls.Add(this.cellsXCountLabel);
            this.Controls.Add(this.cellsYCountNumericUpDown);
            this.Controls.Add(this.cellsXCountNumericUpDown);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SizeForm";
            this.Text = "Tile size";
            ((System.ComponentModel.ISupportInitialize)(this.cellsXCountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cellsYCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown cellsXCountNumericUpDown;
        private System.Windows.Forms.NumericUpDown cellsYCountNumericUpDown;
        private System.Windows.Forms.Label cellsXCountLabel;
        private System.Windows.Forms.Label cellsYCountLabel;
        private System.Windows.Forms.Button OKbutton;
        private System.Windows.Forms.Button Cancelbutton;
    }
}