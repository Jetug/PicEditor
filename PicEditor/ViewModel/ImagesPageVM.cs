using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI;
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

namespace PicEditor.ViewModel
{
    class ImagesPageVM : ViewModelBase
    {
        #region Константы
        private const int defaultSize = 150;
        #endregion

        #region Свойства
        public string NewName { get; set; }
        public int SortParamIndex { get; set; } = -1;
        public BitmapImage PictureSource { get; set; }
        public string directory;
        public string Directory
        { 
            get => directory;
            set
            {
                try
                {
                    global.ImageItemCollections.Add(value, ImageItems);
                }
                catch (ArgumentException)
                {
                    ImageItems = global.ImageItemCollections[value];
                }
                //global.ImageItemCollections.Add(value, ImageItems);
                directory = value;
            }
        }
        public double PreviewWidth { get; set; } = defaultSize;
        public double PreviewHeight { get; set; } = defaultSize;
        public double ScrollViewHeight { get; set; }
        public Point ScrollViewMousePos { get; set; }
        public BitmapImage DraggablePreview { get; set; }
        public Thickness DraggableMargin { get; set; } = new Thickness(10, 415, 0, 0);
        public Visibility DraggableVisibility { get; set; } = Visibility.Hidden;
        public bool IsChecked { get; set; } = false;

        public ObservableCollection<ImageItem> ImageItems { get; set; } = new ObservableCollection<ImageItem>();
        #endregion

        #region Delegates
        public delegate Point PointHandler();
        public delegate double SizeHandler();
        public Action<double> LineUp;
        public Action<double> LineDown;
        public PointHandler GetMausePosOnScrollView;
        public PointHandler GetMausePosOnWindow;
        public SizeHandler GetScrollViewHeigh;
        #endregion

        #region Поля
        private int id;
        private MainModel model = new MainModel();
        private NavigationService global = NavigationService.GetInstance();
        #endregion

        #region Конструкторы
        public ImagesPageVM()
        {
            id = global.GetID();

            

            global.SelectedImageItems.Clear();
            //global.ShowPreview = (prev) =>
            //{
            //    DraggablePreview = prev;
            //    DraggableVisibility = Visibility.Visible;
            //};

            //global.HidePreview = () => DraggableVisibility = Visibility.Hidden;


            ImagePageVMParameters parameters = (ImagePageVMParameters)global.GetParameters(id);
            Directory = parameters.Directory;

            model.ShowPreview += ShowPreview;
            model.ShowEmptyPreviews += ShowEmptyPreviews;

            model.GetPictures(Directory, Sorting.Name);
        }
        #endregion

        #region Команды
        public ICommand WindowLoaded
        {
            get => new DelegateCommand(() =>
            {
                //ImageItems.Clear();

                //for (int i = 0; i < 100; i++)
                //{
                //    ImageItems.Add(new ImageItem(""));
                //}

                //model.GetPictures(@"C:\Users\kserg\OneDrive\Рабочий стол\imgs\TestThink", SortingType.Name);//, () => SortBy(Sorting.Name));
            });
        }

        public ICommand RenameAll
        {
            get => new DelegateCommand(() =>
            {
                model.RenameFiles(ImageItems.ToList(), NewName);
            });
        }

        public ICommand Rename
        {
            get => new DelegateCommand(() =>
            {
                RenamingWindowVM renaming = new RenamingWindowVM();
                renaming.RenameImages = (name) => model.RenameFiles(ImageItems.ToList(), name);
                renaming.View.ShowDialog();
            });
        }

        public ICommand EditDate
        {
            get => new DelegateCommand(() =>
            {
                var date = new DateTime(2018, 1, 1, 1, 0, 0);
                var span = new TimeSpan(0,5,0);
                model.EditDate(ImageItems.ToList(), date, span, DateType.CreationAndModification);
                for (int i = 0; i < ImageItems.Count; i++)
                {
                    ImageItems[i].SetDate();
                }
            });
        }

        public ICommand Scroll
        {
            get => new DelegateCommand(() =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed && global.DraggableImage != null)
                {
                    const double trigger = 50;
                    const double offset = 5;
                    const int sleep = 1;
                    Point pos = ScrollViewMousePos;

                    Thread thread = new Thread(() =>
                    {
                        while (ScrollViewMousePos.Y <= trigger)
                        {
                            LineUp(offset);
                            Thread.Sleep(sleep);
                        }
                        while (ScrollViewMousePos.Y >= GetScrollViewHeigh() - trigger)
                        {
                            LineDown(offset);
                            Thread.Sleep(sleep);
                        }
                    });

                    thread.Start();
                }
            });
        }

        public ICommand WinMouseMove
        {
            get => new DelegateCommand<MouseEventArgs>((e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed && global.DraggableImage != null)
                {
                    Point pos = GetMausePosOnWindow();
                    DraggableMargin = new Thickness(pos.X - global.ImageMouseX, pos.Y - global.ImageMouseY, 0, 0);
                }
            });
        }

        public ICommand SelectAll
        {
            get => new DelegateCommand(() =>
            {
                for (int i = 0; i < ImageItems.Count; i++)
                {
                    ImageItems[i].IsSelected = true;
                }
            });
        }

        public ICommand UnselectAll
        {
            get => new DelegateCommand(() =>
            {
                for (int i = 0; i < ImageItems.Count; i++)
                {
                    ImageItems[i].IsSelected = false;
                }
            });
        }

        public ICommand SortParamChanged
        {
            get => new DelegateCommand(() =>
            {
                switch (SortParamIndex)
                {
                    case 0:
                        SortBy(Sorting.Name);
                        break;
                    case 1:
                        SortBy(Sorting.CreationDate);
                        break;
                    case 2:
                        SortBy(Sorting.ModificationDate);
                        break;
                }
            });
        }
        #endregion

        #region Приватные методы
        private void SortBy(Sorting sorting)
        {
            IOrderedEnumerable<ImageItem> temp = null;
            switch (sorting)
            {
                case Sorting.Name:
                    temp = ImageItems.OrderBy(p => p.Name, StringComparison.OrdinalIgnoreCase.WithNaturalSort());
                    break;
                case Sorting.CreationDate:
                    temp = ImageItems.OrderBy(p => p.CreationDate);
                    break;
                case Sorting.ModificationDate:
                    temp = ImageItems.OrderBy(p => p.ModificationDate);
                    break;
            }
            for (int i = 0; i < temp.Count(); i++)
            {
                ImageItems.Move(ImageItems.IndexOf(temp.ElementAt(i)), i);
            }
        }

        private void ShowPreview(ImageItem img, int i)
        {
            ImageItems.Add(img);

            //ImageItems[i].Fill(img);
            //RaisePropertyChanged("ImageItems");
        }

        private void ShowEmptyPreviews(int count)
        {
            for (int i = 0; i < count; i++)
            {
                BitmapImage bmi = new BitmapImage();
                ImageItems.Add(new ImageItem(bmi));
            }
        }
        #endregion
    }
}