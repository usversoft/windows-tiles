using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace BaseClasses
{
    public class BooleanEditor : System.Drawing.Design.UITypeEditor
    {
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        }
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            //Bitmap Img;
            //if ((bool)e.Value)
            //{
            //    Img = Properties.Resources._true;
            //}
            //else
            //{
            //    Img = Properties.Resources._true;
            //}
            //Img.MakeTransparent();
            //e.Graphics.DrawImage(Img, e.Bounds);
            //if (Img != null)
            //    Img.Dispose();
        }
    }
}
