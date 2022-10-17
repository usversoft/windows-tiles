using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TilesControl
{
    public partial class AddTileForm : Form
    {
        public AddTileForm()
        {
            InitializeComponent();
            BackColorTile = Color.AliceBlue;
            TextForeColorTile = Color.Black;
            BackColorButton.BackColor = BackColorTile;
            BackColorButton.ForeColor = TextForeColorTile;
            TextForeColorButton.BackColor = BackColorTile;
            TextForeColorButton.ForeColor = TextForeColorTile;
        }

        public string TextTile { get => TextTextBox.Text; set => TextTextBox.Text = value; }
        public string LinkTile { get => LinkTextBox.Text; set => LinkTextBox.Text = value; }
        public Image ImageTile { get; set; }
        public Color BackColorTile { get; set; }
        public Color TextForeColorTile { get; set; }

        private void BackColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog frm = new ColorDialog())
            {
                frm.Color = BackColorTile;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BackColorTile = frm.Color;
                    BackColorButton.BackColor = BackColorTile;
                    BackColorButton.ForeColor = TextForeColorTile;

                    TextForeColorButton.BackColor = BackColorTile;
                    TextForeColorButton.ForeColor = TextForeColorTile;
                }
            }
        }

        private void TextForeColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog frm = new ColorDialog())
            {
                frm.Color = TextForeColorTile;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    TextForeColorTile = frm.Color;
                    TextForeColorButton.BackColor = BackColorTile;
                    TextForeColorButton.ForeColor = TextForeColorTile;

                    BackColorButton.BackColor = BackColorTile;
                    BackColorButton.ForeColor = TextForeColorTile;
                }
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog frm = new OpenFileDialog())
            {
                frm.Filter = "*.png|*.png|*.jpg|*.jpg";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImageTile = new Bitmap(frm.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
