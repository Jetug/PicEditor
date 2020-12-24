using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using PicEditor.Model;
using PicEditor.View;

namespace PicEditor.ViewModel
{
    class FolderItem : ViewModelBase
    {
        #region Константы
        private const int imgWidgh = 150;
        private const int imgHeigh = 150;
        private const int imgSize = 150;

        private const double thickness = 10;
        #endregion

        #region Свойства
        public double Width { get; set; } = imgWidgh;
        public double Height { get; set; } = imgHeigh;
        private string path = "";
        public string Directory
        {
            get => path;
            set
            {
                path = value;
                Name = Path.GetFileNameWithoutExtension(value);
                CreationDate = File.GetCreationTime(value);
                ModificationDate = File.GetLastWriteTime(value);
            }
        }
        public string Name { get; private set; }
        public BitmapImage Preview { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Point ImageMousePos { get; set; }
        public bool IsSelected{ get; set; }
        public Thickness BorderThickness { get; set; } = new Thickness(thickness);
        #endregion

        #region Поля
        private MediaSearcher model = new MediaSearcher();
        private NavigationService global = NavigationService.GetInstance();
        #endregion

        #region Конструкторы

        public FolderItem(string fullPath)
        {
            Directory = fullPath;
        }

        public FolderItem(BitmapImage source, string fullPath)
        {
            Preview = source;
            Directory = fullPath;
        }
        #endregion

        #region Публичные мотоды
        public async void ShowThumbnail()
        {
            Preview = await Task.Factory.StartNew(() => model.GetFolderThumbnail(Directory, imgSize));
        }
        #endregion

        #region Команды
        public ICommand FolderMouseEnter
        {
            get => new DelegateCommand(() =>
            {
                if (Keyboard.Modifiers == ModifierKeys.Control && Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    IsSelected = !IsSelected;
                }
            });
        }

        public ICommand FolderLeftButtonDown
        {
            get => new DelegateCommand(() =>
            {
                //global.ClickedElement = this;
                //ClickCreator.OnMouseDown(this);
                //BorderThickness = new Thickness(thickness + 2);
            });
        }

        public ICommand FolderLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {
                //if (ClickCreator.OnMouseUp(this))
                //{
                //    global.ShowPage<ImagesPage>(new DirectoryParameters(Directory));
                //}
                //if (global.ClickedElement == this)
                //{
                //    //BorderThickness = new Thickness(thickness);
                //    global.ShowPage<ImagesPage>(new DirectoryParameters(Directory));
                //}
            });
        }

        public ICommand FolderClick
        {
            get => new DelegateCommand(() =>
            {
                global.ShowPage<ImagesPage>(new DirectoryParameters(Directory));
            });
        }


        public ICommand FolderMouseLeave
        {
            get => new DelegateCommand(() =>
            {
                //BorderThickness = new Thickness(thickness);
            });
        }
        #endregion
    }
}