using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace ScrollAblePanel
{
    internal class GraphicsUtils
    {
        internal void DrawCounter(PaintEventArgs pevent, Rectangle r, Color clr, float _radius)
        {
            var brush = new System.Drawing.SolidBrush(clr);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;


            Pen p = new Pen(brush);
            g.DrawRectangle(p,r);
            g.FillRectangle(brush,r);

            //g.FillRoundedRectangle(brush, r.Left, r.Top, r.Width, r.Height, _radius);
        }
    }
}
