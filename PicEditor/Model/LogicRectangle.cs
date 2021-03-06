﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PicEditor.Model
{
    class VirtualRectangle
    {
        public double Left;
        public double Top;
        public double Right;
        public double Bottom;

        public VirtualRectangle(Point TopLeft, Point BottomRight)
        {
            Left = TopLeft.X;
            Top = TopLeft.Y;
            Right = BottomRight.X;
            Bottom = BottomRight.Y;
        }

        public bool Intersects(VirtualRectangle rect)
        {
            return Intersects(this, rect);
        }

        public static bool Intersects(VirtualRectangle r1, VirtualRectangle r2)
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
