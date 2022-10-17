using System.Drawing;
using System.Windows.Forms;

namespace ScrollAblePanel
{
    partial class ScrollAblePanelControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScrollContainterV = new System.Windows.Forms.Panel();
            this.ScrollAtY = new ScrollAblePanel.ScrollAt();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            this.ScrollContainterH = new System.Windows.Forms.Panel();
            this.ScrollAtX = new ScrollAblePanel.ScrollAt();
            this.SuspendLayout();
            // 
            // ScrollContainterV
            // 
            this.ScrollContainterV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollContainterV.BackColor = System.Drawing.Color.Black;
            this.ScrollContainterV.ForeColor = System.Drawing.Color.Red;
            this.ScrollContainterV.Location = new System.Drawing.Point(397, 3);
            this.ScrollContainterV.Name = "ScrollContainterV";
            this.ScrollContainterV.Size = new System.Drawing.Size(10, 257);
            this.ScrollContainterV.TabIndex = 0;
            this.ScrollContainterV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScrollContainterV_MouseClick);
            // 
            // ScrollAtY
            // 
            this.ScrollAtY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollAtY.BackColor = System.Drawing.Color.Red;
            this.ScrollAtY.Location = new System.Drawing.Point(398, 4);
            this.ScrollAtY.Name = "ScrollAtY";
            this.ScrollAtY.Size = new System.Drawing.Size(8, 32);
            this.ScrollAtY.TabIndex = 1;
            this.ScrollAtY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScrollAtY_MouseDown);
            this.ScrollAtY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollAtY_MouseMove);
            this.ScrollAtY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScrollAtY_MouseUp);
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollPanel.AutoScroll = true;
            this.ScrollPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ScrollPanel.Location = new System.Drawing.Point(-1, -1);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(391, 265);
            this.ScrollPanel.TabIndex = 2;
            this.ScrollPanel.LocationChanged += new System.EventHandler(this.ScrollPanel_LocationChanged);
            this.ScrollPanel.SizeChanged += new System.EventHandler(this.ScrollAblePanelControl_SizeChanged);
            this.ScrollPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScrollAblePanelControl_MouseDown);
            this.ScrollPanel.MouseEnter += new System.EventHandler(this.ScrollPanel_MouseEnter);
            this.ScrollPanel.MouseLeave += new System.EventHandler(this.ScrollPanel_MouseLeave);
            this.ScrollPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollAblePanelControl_MouseMove);
            this.ScrollPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScrollAblePanelControl_MouseUp);
            // 
            // ScrollContainterH
            // 
            this.ScrollContainterH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrollContainterH.BackColor = System.Drawing.Color.Black;
            this.ScrollContainterH.Location = new System.Drawing.Point(3, 251);
            this.ScrollContainterH.Name = "ScrollContainterH";
            this.ScrollContainterH.Size = new System.Drawing.Size(387, 10);
            this.ScrollContainterH.TabIndex = 0;
            this.ScrollContainterH.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ScrollContainterH_MouseClick);
            // 
            // ScrollAtX
            // 
            this.ScrollAtX.BackColor = System.Drawing.Color.Red;
            this.ScrollAtX.Location = new System.Drawing.Point(5, 252);
            this.ScrollAtX.Name = "ScrollAtX";
            this.ScrollAtX.Size = new System.Drawing.Size(32, 8);
            this.ScrollAtX.TabIndex = 0;
            this.ScrollAtX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScrollAtX_MouseDown);
            this.ScrollAtX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScrollAtX_MouseMove);
            this.ScrollAtX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScrollAtX_MouseUp);
            // 
            // ScrollAblePanelControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScrollAtX);
            this.Controls.Add(this.ScrollAtY);
            this.Controls.Add(this.ScrollContainterV);
            this.Controls.Add(this.ScrollContainterH);
            this.Controls.Add(this.ScrollPanel);
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "ScrollAblePanelControl";
            this.Size = new System.Drawing.Size(410, 264);
            this.SizeChanged += new System.EventHandler(this.ScrollAblePanelControl_SizeChanged);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ScrollAblePanelControl_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ScrollContainterV;
        public ScrollAblePanel.ScrollAt ScrollAtY;
        private System.Windows.Forms.Panel ScrollPanel;
        private System.Windows.Forms.Panel ScrollContainterH;
        private ScrollAblePanel.ScrollAt ScrollAtX;
    }
}
