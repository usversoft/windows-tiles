using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class BooleanTypeConverter : System.ComponentModel.BooleanConverter
    {
        public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if ((bool)value)
            {
                return "Да";
            }
            else
            {
                return "Нет";
            }
        }
        public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            return ((string)value == "Да");
        }
    }

}
