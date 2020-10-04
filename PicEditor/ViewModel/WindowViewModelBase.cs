using DevExpress.Mvvm;
using PicEditor.Model;
using System.Windows;

namespace PicEditor.ViewModel
{
    class WindowViewModelBase : ViewModelBase
    {
        public Window Window
        {
            set => _ = new WindowResizer(value);
        }
    }
}
