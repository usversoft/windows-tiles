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
    public partial class EditTextForm : Form
    {
        public EditTextForm()
        {
            InitializeComponent();
        }

        public string TextEditable
        {
            get
            {
                return TextTextBox.Text;
            }
            set
            {
                TextTextBox.Text = value;
            }
        }
    }
}
