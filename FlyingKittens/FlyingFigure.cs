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
        public abstract void Draw(Graphics g);
        public abstract void Move(Point shift);
        public abstract FlyingFigure Clone();
    }
}
