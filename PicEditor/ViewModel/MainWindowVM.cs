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

namespace PicEditor.ViewModel
{
    class MainWindowVM : WindowViewModelBase
    {
        #region Константы
        private const int defaultSize = 700;
        private const double imageIndent = 60;
        #endregion

        #region Свойства
        public string NewName { get; set; }
        public int SortParamIndex { get; set; } = -1;
        public Visibility PictureVisibility { get; set; } = Visibility.Hidden;
        public BitmapImage PictureSource { get; set; }
        public double WindowWidght { get; set; }
        public double WindowHeight{ get; set; }
        public double ImageWidth { get; set; } = defaultSize ;
        public double ImageHeight { get; set; } = defaultSize ;

        public BitmapImage DraggablePreview { get; set; }
        public Thickness DraggableMargin { get; set; } = new Thickness(10, 415, 0, 0);
        public Visibility DraggableVisibility { get; set; } = Visibility.Hidden;
        public UserControl FramePage { get; set; }
        #endregion

        #region Делегаты
        public delegate Point PointHandler();
        public delegate double SizeHandler();
        public PointHandler GetMausePosOnWindow;
        #endregion

        #region Поля
        private MainModel model = new MainModel();
        private NavigationService global = NavigationService.GetInstance();
        private readonly FoldersPanel folderPanel = new FoldersPanel();
        #endregion

        #region Конструкторы
        public MainWindowVM()
        {
            //Navigate(imagesPageVM.View);
            //FramePage = imagesPageVM.View;

            global.OpenPicture = (pic) =>
            {
                PictureVisibility = Visibility.Visible;
                Thread.Sleep(10);
                PictureSource = pic;

            };

            global.OpenPictureTR = (pic) =>
            {
                PictureVisibility = Visibility.Visible;
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
                global.Navigate = (uc) =>
                {
                    FramePage = uc;
                };
                FramePage = folderPanel;
                global.HomePage = folderPanel;
                //imagesPageVM2.Show(@"C:\Users\Jetug\Desktop\Krekk0vTest");

                ImageWidth = defaultSize - imageIndent * 2;
                ImageHeight = defaultSize - imageIndent * 2;

                global.ShowPreview = Show;
                //(bmi) =>
                //{
                //    DraggablePreview = bmi;
                //    DraggableVisibility = Visibility.Visible;
                //};

                global.HidePreview = Hide;
                //() =>
                //{
                //    DraggableVisibility = Visibility.Hidden;
                //    DraggablePreview = null;
                //};
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
                global.ShowPreview(null);
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

        public ICommand GoBack
        {
            get => new DelegateCommand(() =>
            {
                global.Navigate(global.HomePage);
            });
        }

        public ICommand Goforward
        {
            get => new DelegateCommand(() =>
            {
                global.Navigate(global.CurrentPage);
            });
        }

        public ICommand WindowMouseDown
        {
            get => new DelegateCommand(() =>
            {
                if(Mouse.XButton1 == MouseButtonState.Pressed)
                    global.Navigate(global.HomePage);
                if (Mouse.XButton2 == MouseButtonState.Pressed)
                    global.Navigate(global.CurrentPage);
            });
        }

        public ICommand SizeChanged
        {
            get => new DelegateCommand<SizeChangedEventArgs>((e) =>
            {
                ImageWidth = e.NewSize.Width - imageIndent * 2;
                ImageHeight = e.NewSize.Height - imageIndent * 2;
            });
        }

        public ICommand ClosePicture
        {
            get => new DelegateCommand(() =>
            {
                PictureVisibility = Visibility.Hidden;
            });
        }

        public ICommand EscapeDown
        {
            get => new DelegateCommand<KeyEventArgs>((e) =>
            {
                if (PictureVisibility == Visibility.Visible)
                {
                    if (e.Key == Key.Escape)
                        PictureVisibility = Visibility.Hidden;
                }
                else
                {
                    //imagesPageVM.UnselectAll.Execute(null); 
                    //imagesPageVM.IsChecked = false;
                }
            });
        }

        public ICommand WindowLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {
                global.DraggableImage = null;
                //global.SelectedImageItems.Clear();
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
                    global.ShowPage<ImagesPanel>(new ImagePageVMParameters(fbd.SelectedPath));
                }
            });
        }

        public ICommand WindowMouseMove
        {
            get => new DelegateCommand<MouseEventArgs>((e) =>
            {
                //if(Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    if (global.DraggableImage != null && DraggablePreview != global.DraggableImage.Preview)
                    {
                        DraggableVisibility = Visibility.Visible;
                        DraggablePreview = global.DraggableImage.Preview;
                    }

                    double x = GetMausePosOnWindow().X - global.ImageMouseX;
                    double y = GetMausePosOnWindow().Y - global.ImageMouseY;
                    DraggableMargin = new Thickness(x, y, 0, 0);

                    //global.ShowPreview(null);
                }
            });
        }

        private const double pictureResizeValue = 100;
        public ICommand ResizePicture
        {
            get => new DelegateCommand<MouseWheelEventArgs>((e) =>
            {
                if(e.Delta > 0)
                {
                    ImageHeight += pictureResizeValue;
                    ImageWidth += pictureResizeValue;
                }
                else if(ImageHeight - pictureResizeValue > pictureResizeValue && ImageWidth - pictureResizeValue > pictureResizeValue)
                {
                    ImageHeight -= pictureResizeValue;
                    ImageWidth -= pictureResizeValue;
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
    }
}