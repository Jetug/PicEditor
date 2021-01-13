using PicEditor.ViewModel;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace PicEditor.View
{
    /// <summary>
    /// Логика взаимодействия для DraggableThumbnail.xaml
    /// </summary>
    public partial class DraggableThumbnail : Window
    {
        public DraggableThumbnail()
        {
            InitializeComponent();
            DraggableThumbnailVM vm = (DataContext as DraggableThumbnailVM);
            vm.CloseWindow = Close;
            vm.SetWindowLeft = (pos) => Dispatcher.Invoke(() => window.Left = pos);
            vm.SetWindowTop = (pos) => Dispatcher.Invoke(() => window.Top = pos);
        }

        //public new void Show()
        //{
        //    Thread t = new Thread(ShowWindow);
        //    t.TrySetApartmentState(ApartmentState.STA);
        //    t.IsBackground = true;
        //    t.Start();
        //}


        //private void ShowWindow()
        //{
        //    //base.Show();
        //    var window2 = new Window();
        //    window2.Show();
        //    System.Windows.Threading.Dispatcher.Run();
        //}

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            WinAPI.MakeWindowTransparent(this);
        }
    }
}
