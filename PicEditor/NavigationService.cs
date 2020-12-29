using DevExpress.Mvvm;
using PicEditor.View;
using PicEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PicEditor
{
    class NavigationService
    {
        #region Singleton
        private static NavigationService instance;

        private NavigationService() { }

        public static NavigationService GetInstance()
        {
            if (instance == null)
                instance = new NavigationService();
            return instance;
        }
        #endregion

        #region Делегаты
        public delegate Point PointHandler();

        public Action<BitmapImage> ShowThumbnail { get; set; }
        public Action HidePreview { get; set; }
        public Action<BitmapImage> OpenPicture;
        public Action<string> OpenPictureTR;
        public Predicate<Page> Navigate;
        public PointHandler GetMousePosition;
        #endregion

        #region Свойства
        public ImageItem DraggableImage { get; set; }
        public BitmapImage NextPicture { get; set; }
        public BitmapImage PerviousPicture { get; set; }

        public double ImageMouseX { get; set; }
        public double ImageMouseY { get; set; }

        public List<string> PicturePaths { get; set; } = new List<string>();

        public ObservableCollection<ImageItem> selectedImageItems = new ObservableCollection<ImageItem>();
        public ObservableCollection<ImageItem> SelectedImageItems 
        {
            get => selectedImageItems;
            set
            {
                selectedImageItems = value;
            }
        }
        
        public Dictionary<string, ObservableCollection<ImageItem>> ImageItemCollections = new Dictionary<string, ObservableCollection<ImageItem>>();
        public Dictionary<string, ImagesPageVM> FrameHistory = new Dictionary<string, ImagesPageVM>();
        public ViewModelBase CurrentViewModel;

        public Page HomePage { get; set; }
        public Page CurrentPage { get; set; }

        public object ClickedElement { get; set; }
        #endregion
    }
}