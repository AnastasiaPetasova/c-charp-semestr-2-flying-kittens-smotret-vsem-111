using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingKittens
{
    abstract class FlyingFigure
    {
        protected Color penColor;
        protected Color brushColor;

        protected FlyingFigure(Color penColor, Color brushColor)
        {
            this.penColor = penColor;
            this.brushColor = brushColor;
        }

        public abstract bool IsOutside(Field field);
        public abstract bool IsInsideX(Field field);
        public abstract bool IsInsideY(Field field);

        protected abstract void DrawPen(Graphics g, Pen pen);
        protected abstract void DrawBrush(Graphics g, Brush brush);

        public virtual void Draw(Graphics g)
        {
            if (penColor != Color.Empty)
            {
                using (Pen pen = new Pen(penColor))
                {
                    DrawPen(g, pen);
                }
            }

            if (brushColor != Color.Empty)
            {
                using (Brush brush = new SolidBrush(brushColor))
                {
                    DrawBrush(g, brush);
                }
            }
        }

        public abstract void Move(Point shift);
        public abstract FlyingFigure Clone();
    }
}
