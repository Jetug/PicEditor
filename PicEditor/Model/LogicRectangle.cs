using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PicEditor.Model
{
    class LogicRectangle
    {
        public double Left;
        public double Top;
        public double Right;
        public double Bottom;

        public LogicRectangle(Point TopLeft, Point BottomRight)
        {
            Left = TopLeft.X;
            Top = TopLeft.Y;
            Right = BottomRight.X;
            Bottom = BottomRight.Y;
        }

        public bool Intersects(LogicRectangle rect)
        {
            return Intersects(this, rect);
        }

        public static bool Intersects(LogicRectangle r1, LogicRectangle r2)
        {
            return
            (
                (
                    ((r1.Left >= r2.Left && r1.Left <= r2.Right) || (r1.Right >= r2.Left && r1.Right <= r2.Right))
                    &&
                    ((r1.Top >= r2.Top && r1.Top <= r2.Bottom) || (r1.Bottom >= r2.Top && r1.Bottom <= r2.Bottom))
                )
                ||
                (
                    ((r2.Left >= r1.Left && r2.Left <= r1.Right) || (r2.Right >= r1.Left && r2.Right <= r1.Right))
                    &&
                    ((r2.Top >= r1.Top && r2.Top <= r1.Bottom) || (r2.Bottom >= r1.Top && r2.Bottom <= r1.Bottom))
                )
            )
            ||
            (
                (
                    ((r1.Left >= r2.Left && r1.Left <= r2.Right) || (r1.Right >= r2.Left && r1.Right <= r2.Right))
                    &&
                    ((r2.Top >= r1.Top && r2.Top <= r1.Bottom) || (r2.Bottom >= r1.Top && r2.Bottom <= r1.Bottom))
                )
                ||
                (
                    ((r2.Left >= r1.Left && r2.Left <= r1.Right) || (r2.Right >= r1.Left && r2.Right <= r1.Right))
                    &&
                    ((r1.Top >= r2.Top && r1.Top <= r2.Bottom) || (r1.Bottom >= r2.Top && r1.Bottom <= r2.Bottom))
                )
            );
        }
    }
}
