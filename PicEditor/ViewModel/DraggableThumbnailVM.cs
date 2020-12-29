using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using PicEditor.Model;
using PicEditor.View;
using System.Threading.Tasks;
using PicEditor.UserControls;
using Gma.System.MouseKeyHook;

namespace PicEditor.ViewModel
{
    class DraggableThumbnailVM : ViewModelBase, INavigable
    {
        public Action CloseWindow;

        private IParameters _parameters;
        public IParameters Parameters
        {
            get => _parameters;
            set
            {
                DraggableThumbnailParametrs parametrs = (DraggableThumbnailParametrs)value;
                ImageItem imageItem = parametrs.imageItem;
                Content = ItemToThumbnailConverter.Convert(imageItem);
                _parameters = value;
            }
        }

        public object Content { get; set; }
         
        public DraggableThumbnailVM()
        {
            MouseHook.OnMouseUp += StopDragging;
        }

        public void StopDragging(object sender, Point p)
        {
            CloseWindow();
            MouseHook.OnMouseUp -= StopDragging;
        }
    }
}
