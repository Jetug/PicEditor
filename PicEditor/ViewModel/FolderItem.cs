using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using DevExpress.Mvvm;
using PicEditor.Model;
using System.Security.Policy;
using System.Collections.Generic;
using System.Threading;
using PicEditor.View;

namespace PicEditor.ViewModel
{
    class FolderItem : ViewModelBase
    {
        #region Константы
        private const int imgWidgh = 150;
        private const int imgHeigh = 150;
        private const double thickness = 10;
        #endregion

        #region Свойства
        public double Width { get; set; } = imgWidgh;
        public double Height { get; set; } = imgHeigh;
        private string path = "";
        public string FullPath
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
        private MainModel model = new MainModel();
        private NavigationService global = NavigationService.GetInstance();
        #endregion

        #region Конструкторы

        public FolderItem(string fullPath)
        {
            FullPath = fullPath;
        }

        public FolderItem(BitmapImage source, string fullPath)
        {
            Preview = source;
            FullPath = fullPath;
        }
        #endregion

        #region Commands
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
                global.ClickedElement = this;
                //BorderThickness = new Thickness(thickness + 2);
            });
        }

        public ICommand FolderLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {
                //if (global.FrameHistory.ContainsKey(FullPath))
                //{
                //    global.FrameHistory[FullPath].Show(FullPath);
                //}
                //else
                //{
                //    ImagesPageVM imagesPageVM = new ImagesPageVM();
                //    global.FrameHistory.Add(FullPath, imagesPageVM);
                //    imagesPageVM.Show(FullPath);
                //}

                //ImagesPageVM imagesPageVM = new ImagesPageVM();
                if (global.ClickedElement == this)
                {
                    //BorderThickness = new Thickness(thickness);
                    global.ShowPage<ImagesPanel>(new ImagePageVMParameters(FullPath));
                }
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