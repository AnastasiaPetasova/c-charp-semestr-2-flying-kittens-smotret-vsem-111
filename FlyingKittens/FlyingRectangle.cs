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

        public override void Draw(Graphics g)
        {
            if (penColor != null)
            {
                using (Pen pen = new Pen(penColor))
                {
                    g.DrawRectangle(pen, box);
                }
            }
            
            if (brushColor != null)
            {
                using (Brush brush = new SolidBrush(brushColor))
                {
                    g.FillRectangle(brush, box);
                }
            }
        }
    }
}
