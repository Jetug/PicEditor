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
        public Action<BitmapImage> ShowPreview { get; set; }
        public Action HidePreview { get; set; }
        public Action<BitmapImage> OpenPicture;
        public Action<string> OpenPictureTR;
        public Action<UserControl> Navigate;
        //public Action ShowNextPicture;
        //public Action ShowPerviousPicture;
        #endregion

        #region Поля
        private int maxID = -1;
        #endregion

        #region Свойства
        private ImageItem _draggableImage = null;
        public ImageItem DraggableImage { get; set; }
        //{
        //    get => _draggableImage;
        //    set
        //    {
        //        _draggableImage = value;
        //        if (value != null)
        //            ShowPreview(value.Preview);
        //        else
        //        {
        //            HidePreview();
        //            //ClickedElement = null;
        //        }
        //    }
        //}

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
        public Dictionary<int, Parameters> ConstructorParameters = new Dictionary<int, Parameters>();

        public UserControl HomePage { get; set; }
        public UserControl CurrentPage { get; set; }

        public object ClickedElement { get; set; }
        #endregion

        #region Публичные методы
        public void ShowPage<T>(Parameters parameters) where T : UserControl, new()
        {
            ConstructorParameters.Add(maxID+1, parameters);
            T page = new T();
            Navigate(page);
            CurrentPage = page;
        }

        public int GetID()
        {
            return ++maxID;
        }

        public Parameters GetParameters(int id)
        {
            return ConstructorParameters[id];
        }
        #endregion
    }

    abstract class Parameters
    {
        
    }

    class ImagePageVMParameters: Parameters
    {
        public string Directory;

        public ImagePageVMParameters(string directory)
        {
            Directory = directory;
        }
    }
}