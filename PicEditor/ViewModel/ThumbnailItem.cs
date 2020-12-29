using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using PicEditor.Model;
using System.Windows.Media;

namespace PicEditor.ViewModel
{
    abstract class ThumbnailItem : ViewModelBase
    {
        #region Константы
        protected const int imgWidgh = 150;
        protected const int imgHeigh = 150;
        protected const int imgSize = 150;
        #endregion

        #region Свойства
        private string _directory = "";
        public string Directory
        {
            get => _directory;
            set
            {
                _directory = value;
                //Name = Path.GetFileNameWithoutExtension(value);
                //Extension = Path.GetExtension(value);
                //CreationDate = File.GetCreationTime(value);
                //ModificationDate = File.GetLastWriteTime(value);
            }
        }
        public string Name { get; private set; }
        public BitmapImage Preview { get; set; } = null;
        private BitmapImage _fullImage = null;
        public BitmapImage FullImage
        {
            get
            {
                if (_fullImage == null)
                    _fullImage = model.GetFullImage(Directory);
                return _fullImage;
            }
        }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Point ImageMousePos { get; set; }
        public PointHandler<Visual> GetPositionOn { get; set; }

        public double Width { get; set; } = imgWidgh;
        public double Height { get; set; } = imgHeigh;
        #endregion

        #region Поля
        protected MediaSearcher model = new MediaSearcher();
        protected NavigationService global = NavigationService.GetInstance();
        #endregion

        #region Публичные мотоды
        public abstract void ShowThumbnail();
        #endregion

        public ThumbnailItem(string directory)
        {
            Directory = directory;
            Name = Path.GetFileNameWithoutExtension(directory);
            CreationDate = File.GetCreationTime(directory);
            ModificationDate = File.GetLastWriteTime(directory);
        }
    }
}
