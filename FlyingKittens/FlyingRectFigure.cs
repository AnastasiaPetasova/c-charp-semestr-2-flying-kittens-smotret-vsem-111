using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlyingKittens
{
    abstract class FlyingRectFigure : FlyingFigure
    {
        protected Rectangle box;
        protected FlyingRectFigure(Color penColor, Color brushColor, Rectangle box)
            : base(penColor, brushColor)
        {
            this.box = box;
        }

        public override bool IsOutside(Field field)
        {
            if (box.Right < 0) return true;
            if (box.Bottom < 0) return true;
            if (box.Left > field.Width) return true;
            if (box.Top > field.Height) return true;
            return false;
        }

        public override bool IsInsideX(Field field)
        {
            if (box.Left < 0) return false;
            if (box.Right > field.Width) return false;
            return true;
        }

        public override bool IsInsideY(Field field)
        {
            if (box.Top < 0) return false;
            if (box.Bottom > field.Height) return false;
            return true;
        }

        public override void Move(Point shift)
        {
            box.X += shift.X;
            box.Y += shift.Y;
        }

        protected abstract FlyingRectFigure CloneRectFigure(Rectangle clonedBox);

        public override FlyingFigure Clone()
        {
            Rectangle clonedBox = new Rectangle(box.X, box.Y, box.Width, box.Height);
            return CloneRectFigure(clonedBox);
        }
    }
}
