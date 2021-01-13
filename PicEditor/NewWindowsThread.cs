using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace PicEditor
{
    static class NewWindowsThread
    {
        public delegate Window WindowCreation();

        public static void RunWindow(WindowCreation createWindow)
        {
            Thread t = new Thread(() =>
            {
                createWindow().Show();
                System.Windows.Threading.Dispatcher.Run();
            });
            t.TrySetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();
        }
    }
}