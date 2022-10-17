namespace TilesControl
{
    partial class AddTileForm
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
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.TextTextBox = new System.Windows.Forms.TextBox();
            this.BrowseButton = new System.Windows.Forms.Button();
            this.TextLabel = new System.Windows.Forms.Label();
            this.ImageLabel = new System.Windows.Forms.Label();
            this.BackColorButton = new System.Windows.Forms.Button();
            this.TextForeColorButton = new System.Windows.Forms.Button();
            this.LinkTextBox = new System.Windows.Forms.TextBox();
            this.LinkLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(497, 105);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(578, 105);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // TextTextBox
            // 
            this.TextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextTextBox.Location = new System.Drawing.Point(54, 6);
            this.TextTextBox.Name = "TextTextBox";
            this.TextTextBox.Size = new System.Drawing.Size(599, 20);
            this.TextTextBox.TabIndex = 2;
            // 
            // BrowseButton
            // 
            this.BrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseButton.Location = new System.Drawing.Point(54, 58);
            this.BrowseButton.Name = "BrowseButton";
            this.BrowseButton.Size = new System.Drawing.Size(599, 23);
            this.BrowseButton.TabIndex = 3;
            this.BrowseButton.Text = "Browse";
            this.BrowseButton.UseVisualStyleBackColor = true;
            this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.Location = new System.Drawing.Point(12, 9);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(28, 13);
            this.TextLabel.TabIndex = 4;
            this.TextLabel.Text = "Text";
            // 
            // ImageLabel
            // 
            this.ImageLabel.AutoSize = true;
            this.ImageLabel.Location = new System.Drawing.Point(12, 63);
            this.ImageLabel.Name = "ImageLabel";
            this.ImageLabel.Size = new System.Drawing.Size(36, 13);
            this.ImageLabel.TabIndex = 5;
            this.ImageLabel.Text = "Image";
            // 
            // BackColorButton
            // 
            this.BackColorButton.Location = new System.Drawing.Point(54, 87);
            this.BackColorButton.Name = "BackColorButton";
            this.BackColorButton.Size = new System.Drawing.Size(88, 23);
            this.BackColorButton.TabIndex = 6;
            this.BackColorButton.Text = "Backcolor";
            this.BackColorButton.UseVisualStyleBackColor = true;
            this.BackColorButton.Click += new System.EventHandler(this.BackColorButton_Click);
            // 
            // TextForeColorButton
            // 
            this.TextForeColorButton.Location = new System.Drawing.Point(148, 87);
            this.TextForeColorButton.Name = "TextForeColorButton";
            this.TextForeColorButton.Size = new System.Drawing.Size(88, 23);
            this.TextForeColorButton.TabIndex = 6;
            this.TextForeColorButton.Text = "Text Forecolor";
            this.TextForeColorButton.UseVisualStyleBackColor = true;
            this.TextForeColorButton.Click += new System.EventHandler(this.TextForeColorButton_Click);
            // 
            // LinkTextBox
            // 
            this.LinkTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinkTextBox.Location = new System.Drawing.Point(54, 32);
            this.LinkTextBox.Name = "LinkTextBox";
            this.LinkTextBox.Size = new System.Drawing.Size(599, 20);
            this.LinkTextBox.TabIndex = 7;
            // 
            // LinkLabel
            // 
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.Location = new System.Drawing.Point(12, 35);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(27, 13);
            this.LinkLabel.TabIndex = 8;
            this.LinkLabel.Text = "Link";
            // 
            // AddTileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 140);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.LinkTextBox);
            this.Controls.Add(this.TextForeColorButton);
            this.Controls.Add(this.BackColorButton);
            this.Controls.Add(this.ImageLabel);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.BrowseButton);
            this.Controls.Add(this.TextTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OKButton);
            this.Name = "AddTileForm";
            this.Text = "Add tile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox TextTextBox;
        private System.Windows.Forms.Button BrowseButton;
        private System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.Label ImageLabel;
        private System.Windows.Forms.Button BackColorButton;
        private System.Windows.Forms.Button TextForeColorButton;
        private System.Windows.Forms.TextBox LinkTextBox;
        private System.Windows.Forms.Label LinkLabel;
    }
}