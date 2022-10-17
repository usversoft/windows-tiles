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
    public partial class SizeCellForm : Form
    {
        public SizeCellForm()
        {
            InitializeComponent();
        }

        public Size CellSize
        {
            get
            {
                return new Size((int)cellsSizeXCountNumericUpDown.Value, (int)cellsSizeYCountNumericUpDown.Value);
            }
            set
            {
                cellsSizeXCountNumericUpDown.Value = value.Width;
                cellsSizeYCountNumericUpDown.Value = value.Height;
            }
        }
    }
}
