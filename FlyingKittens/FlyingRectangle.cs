using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlyingKittens
{
    class FlyingRectangle : FlyingRectFigure
    {
        public FlyingRectangle(Color penColor, Color brushColor, Rectangle box)
            : base(penColor, brushColor, box)
        { }

        protected override FlyingRectFigure CloneRectFigure(Rectangle clonedBox)
        {
            return new FlyingRectangle(penColor, brushColor, box);
        }

        protected override void DrawPen(Graphics g, Pen pen)
        {
            g.DrawRectangle(pen, box);
        }

        protected override void DrawBrush(Graphics g, Brush brush)
        {
            g.FillRectangle(brush, box);
        }
    }
}
