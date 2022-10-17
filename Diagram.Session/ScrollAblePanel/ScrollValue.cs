using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScrollAblePanel
{
    public class ScrollValue
    {
        [DefaultValue(0)]
        public int Minimum { get; set; }
        [DefaultValue(0)]
        public int Maximum { get; set; }
        public event EventHandler ValueChanged;
        private int _value = 0;
        [DefaultValue(0)]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    if (ValueChanged != null)
                    {
                        ValueChanged (this, new EventArgs());
                    }
                }

            }
        }
    }
}
