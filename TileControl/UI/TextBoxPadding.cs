using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TilesControl
{
    public partial class TextBoxPadding : UserControl
    {
        public TextBoxPadding()
        {
            InitializeComponent();

            this.BackColorChanged += TextboxPadding_BackColorChanged;
            this.ForeColorChanged += TextBoxPadding_ForeColorChanged;
            this.PaddingChanged += TextboxPadding_PaddingChanged;
            this.SizeChanged += TextboxPadding_PaddingChanged;
        }

        private void TextboxPadding_BackColorChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = this.BackColor;
        }

        private void TextBoxPadding_ForeColorChanged(object sender, EventArgs e)
        {
            textBox1.ForeColor = this.ForeColor;
        }

        private void TextboxPadding_PaddingChanged(object sender, EventArgs e)
        {
            textBox1.Location = new Point(Padding.Left, Padding.Top);
            textBox1.Size = new Size(this.Width - Padding.Left - Padding.Right, this.Height - Padding.Top - Padding.Bottom);
        }     

        public override string Text { get => textBox1.Text; set => textBox1.Text = value; }
    }
}
