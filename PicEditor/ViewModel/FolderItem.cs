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
    class FolderItem : ThumbnailItem
    {
        #region Константы
        private const double thickness = 10;
        #endregion

        #region Свойства
        public bool IsSelected{ get; set; }
        public Thickness BorderThickness { get; set; } = new Thickness(thickness);
        #endregion

        #region Поля
        private MediaSearcher model = new MediaSearcher();
        private ViewCreator global = ViewCreator.GetInstance();
        #endregion

        #region Конструкторы

        public FolderItem(string directory) : base(directory) { }

        public FolderItem(BitmapImage source, string directory) : base(directory)
        {
            Preview = source;
        }
        #endregion

        #region Публичные мотоды
        public override async void ShowThumbnail()
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

            });
        }

        public ICommand FolderLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {

            });
        }

        public ICommand FolderClick
        {
            get => new DelegateCommand(() =>
            {
                global.ShowPage<ImagesPage>(new ImagesPageParameters(Directory));
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