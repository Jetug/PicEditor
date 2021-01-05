using PicEditor.ViewModel;
using System;
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
            vm.PointToScreen = window.PointToScreen;
            vm.SetWindowLeft = (pos) => { window.Left = pos; };
            vm.SetWindowTop = (pos) => { window.Top = pos; };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            WinAPI.MakeWindowTransparent(this);
        }
    }
}
