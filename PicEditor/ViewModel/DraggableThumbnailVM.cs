using System;
using System.Windows;
using DevExpress.Mvvm;

namespace PicEditor.ViewModel
{
    class DraggableThumbnailVM : ViewModelBase, INavigable
    {
        public Action CloseWindow;
        public Action<double> SetWindowLeft;
        public Action<double> SetWindowTop;

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
                MouseHook.OnMouseUp += StopDragging;
                MouseHook.OnMouseMove += MoveWindow;
            }
        }

        public object Content { get; set; }

        public DraggableThumbnailVM()
        {
        }

        public void StopDragging(object sender, Point p)
        {
            CloseWindow();
            MouseHook.OnMouseMove -= MoveWindow;
            MouseHook.OnMouseUp -= StopDragging;
        }

        public void MoveWindow(object sender, Point point)
        {
            SetWindowLeft(point.X-30);
            SetWindowTop(point.Y-30);
        }
    }
}
