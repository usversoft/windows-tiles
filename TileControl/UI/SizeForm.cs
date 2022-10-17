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
    public partial class SizeForm : Form
    {
        public SizeForm()
        {
            InitializeComponent();
        }

        public Size CellsCount
        {
            get
            {
                return new Size((int)cellsXCountNumericUpDown.Value, (int)cellsYCountNumericUpDown.Value);
            }
            set
            {
                cellsXCountNumericUpDown.Value = value.Width;
                cellsYCountNumericUpDown.Value = value.Height;
            }
        }
    }
}
