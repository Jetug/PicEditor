using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PicEditor
{
    public delegate Point PointHandler();
    public delegate Point PointHandler<in T>(T obj);
    public delegate double SizeHandler();
}
