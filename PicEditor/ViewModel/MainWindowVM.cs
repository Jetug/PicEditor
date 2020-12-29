using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
using Microsoft.Win32;
using PicEditor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using NaturalSort.Extension;
using PicEditor.View;
using System.Drawing.Imaging;

namespace PicEditor.ViewModel
{
    class MainWindowVM : WindowViewModelBase
    {
        #region Константы
        private const int defaultSize = 700;
        private const double imageIndent = 60;
        //private const double pictureResizeValue = 100;
        #endregion

        #region Свойства
        public string NewName { get; set; }
        public int SortParamIndex { get; set; } = -1;
        public Visibility PictureVisibility { get; set; } = Visibility.Hidden;
        public BitmapImage PictureSource { get; set; }
        public double WindowWidght { get; set; }
        public double WindowHeight{ get; set; }
        //public double ImageWidth { get; set; } = defaultSize ;
        //public double ImageHeight { get; set; } = defaultSize ;

        public BitmapImage DraggablePreview { get; set; }
        public Thickness DraggableMargin { get; set; } = new Thickness(10, 415, 0, 0);
        public Visibility DraggableVisibility { get; set; } = Visibility.Hidden;
        public Page FramePage { get; set; }
        #endregion

        #region Делегаты
        public delegate Point PointHandler();
        public delegate double SizeHandler();

        public PointHandler GetMausePosOnWindow;
        public Action GoBack;
        public Action GoForward;
        #endregion

        #region Поля
        private MediaSearcher model = new MediaSearcher();
        private NavigationService global = NavigationService.GetInstance();
        private ViewCreator viewCreator = ViewCreator.GetInstance();
        private readonly FoldersPage folderPage = new FoldersPage();
        private MouseButtonState xButton1;
        #endregion

        #region Конструкторы
        public MainWindowVM()
        {
            //Navigate(imagesPageVM.View);
            //FramePage = imagesPageVM.View;

            global.OpenPicture = (pic) =>
            {
                ShowPicture();
                Thread.Sleep(10);
                PictureSource = pic;

            };

            global.OpenPictureTR = (pic) =>
            {
                ShowPicture();
                PictureSource = null;
                model.GetFullImageTR(pic, (img) => PictureSource = img);
            };

        }
        #endregion

        #region Commands
        public ICommand WindowLoaded
        {
            get => new DelegateCommand(() =>
            {
                FramePage = folderPage;
                global.HomePage = folderPage;

                //ImageWidth = defaultSize - imageIndent * 2;
                //ImageHeight = defaultSize - imageIndent * 2;

                global.ShowThumbnail = Show;
                global.HidePreview = Hide;
            });
        }

        public void Show(BitmapImage bmi)
        {
            DraggablePreview = bmi;
            DraggableVisibility = Visibility.Visible;
        }

        public void Hide()
        {
            DraggableVisibility = Visibility.Hidden;
            DraggablePreview = null;
        }

        public ICommand Ok
        {
            get => new DelegateCommand(() =>
            {
                global.ShowThumbnail(null);
                //DraggableVisibility = Visibility.Visible;
            });
        }

        public ICommand NotOk
        {
            get => new DelegateCommand(() =>
            {
                global.HidePreview();
                //DraggableVisibility = Visibility.Hidden;
            });
        }

        public ICommand WindowMouseDown
        {
            get => new DelegateCommand<MouseEventArgs>((e) =>
            {
                xButton1 = e.XButton1;
            });
        }

        public ICommand ClosePicture
        {
            get => new DelegateCommand(() =>
            {
                HidePicture();
            });
        }

        public ICommand ClosePictureWithKey
        {
            get => new DelegateCommand(() =>
            {
                var st = Keyboard.GetKeyStates(Key.Escape);
                if ( ((Keyboard.GetKeyStates(Key.Escape) & KeyStates.Down) > 0) || xButton1 == MouseButtonState.Pressed)
                {
                    HidePicture();
                    xButton1 = MouseButtonState.Released;
                }
            });
        }

        public ICommand WindowLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {
                global.DraggableImage = null;
                DraggableVisibility = Visibility.Hidden;
            });
        }

        public ICommand OpenFolder
        {
            get => new DelegateCommand(() =>
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                if(fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    viewCreator.ShowPage<ImagesPage>(new ImagesPageParameters(fbd.SelectedPath));
                }
            });
        }

        public ICommand WindowMouseMove
        {
            get => new DelegateCommand<MouseEventArgs>((e) =>
            {
                if(Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    if (global.DraggableImage != null && DraggablePreview != global.DraggableImage.Preview)
                    {
                        DraggableVisibility = Visibility.Visible;
                        DraggablePreview = global.DraggableImage.Preview;
                    }

                    double x = GetMausePosOnWindow().X - global.ImageMouseX;
                    double y = GetMausePosOnWindow().Y - global.ImageMouseY;
                    DraggableMargin = new Thickness(x, y, 0, 0);
                }
            });
        }

        public ICommand WindowClosing
        {
            get => new DelegateCommand(() =>
            {
                ThreadsService threadsService = ThreadsService.GetInstance();
                threadsService.CloseAllThreads();
            });
        }

        public ICommand ShowNextPicture
        {
            get => new DelegateCommand(() =>
            {
                PictureSource = global.NextPicture;
            });
        }

        public ICommand ShowPerviousPicture
        {
            get => new DelegateCommand(() =>
            {
                PictureSource = global.PerviousPicture;
            });
        }
        #endregion

        private void ShowPicture()
        {
            PictureVisibility = Visibility.Visible;
        }

        private void HidePicture()
        {
            PictureVisibility = Visibility.Hidden;
        }
    }
}