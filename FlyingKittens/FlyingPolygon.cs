using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlyingKittens
{
    class FlyingPolygon : FlyingRectFigure
    {
        public static FlyingPolygon CreateFlyingPolygon(Color penColor, Color brushColor, params Point[] points)
        {
            int minX = points[0].X;
            int minY = points[0].Y;

            int maxX = points[0].X;
            int maxY = points[0].Y;

            foreach (Point point in points)
            {
                minX = Math.Min(minX, point.X);
                maxX = Math.Max(maxX, point.X);

                minY = Math.Min(minY, point.Y);
                maxY = Math.Max(maxY, point.Y);
            }

            Rectangle box = new Rectangle(minX, minY, maxX - minX, maxY - minY);

            return new FlyingPolygon(penColor, brushColor, box, points);
        }

        Point[] points;

        protected FlyingPolygon(Color penColor, Color brushColor, Rectangle box, Point[] points)
            : base(penColor, brushColor, box)
        {
            this.points = points;
        }

        protected override void DrawPen(Graphics g, Pen pen)
        {
            g.DrawPolygon(pen, points);
        }

        protected override void DrawBrush(Graphics g, Brush brush)
        {
            g.FillPolygon(brush, points);
        }

        public override void Move(Point shift)
        {
            base.Move(shift);

            for (int i = 0; i < points.Length; ++i)
            {
                points[i].X += shift.X;
                points[i].Y += shift.Y;
            }
        }

        protected override FlyingRectFigure CloneRectFigure(Rectangle clonedBox)
        {
            Point[] clonedPoints = new Point[points.Length];
            for (int i = 0; i < points.Length; ++i)
            {
                clonedPoints[i] = new Point(points[i].X, points[i].Y);
            }

            return new FlyingPolygon(penColor, brushColor, clonedBox, clonedPoints);
        }
    }
}
