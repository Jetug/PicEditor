using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using PicEditor.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NaturalSort.Extension;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Drawing;
using Point = System.Windows.Point;

namespace PicEditor.ViewModel
{
    class ImagesPageVM : ViewModelBase
    {
        #region Константы
        private const int defaultSize = 150;
        const double scrollOffset = 50;
        #endregion

        #region Свойства
        public string NewName { get; set; }
        public int SortParamIndex { get; set; } = -1;
        public BitmapImage PictureSource { get; set; }
        public string Directory { get; set; }
        public double PreviewWidth { get; set; } = defaultSize;
        public double PreviewHeight { get; set; } = defaultSize;
        public double ScrollViewerHeight { get; set; }
        public double ScrollViewerWidth { get; set; }
        public double ScrollViewerVerticalOffset { get; set; }
        public double ScrollViewerViewportHeight { get; set; }
        public Point ScrollViewMousePos { get; set; }
        public BitmapImage DraggablePreview { get; set; }
        public Thickness DraggableMargin { get; set; } = new Thickness(10, 415, 0, 0);
        public Visibility DraggableVisibility { get; set; } = Visibility.Hidden;
        public bool IsChecked { get; set; } = false;

        public Thickness RubberBandMargin { get; set; }
        public double RubberBandHeight { get; set; } = 0;
        public double RubberBandWidth { get; set; } = 0;
        public TranslateTransform RubberBandRenderTransform { get; set; }
        public static bool NeedRuberBand { get; private set; }

        public ObservableCollection<ImageItem> ImageItems { get; set; } = new ObservableCollection<ImageItem> { };
        #endregion

        #region Delegates
        public Action<double> LineUp;
        public Action<double> LineDown;
        public PointHandler GetMausePosOnScrollView;
        public PointHandler GetMausePosOnPage;
        public SizeHandler GetScrollViewHeigh;
        public Action PageCaptureMouse;
        public Action PageReleaseMouseCapture;
        public SizeHandler GetVerticalOffset;
        public SizeHandler GetViewportHeight;
        public PointHandler<int> GetItemPosition;
        #endregion

        #region Поля
        private int id;
        private MediaSearcher model = new MediaSearcher();
        private NavigationService global = NavigationService.GetInstance();
        private Point startDragPoint;

        public Page page;

        #endregion

        #region Конструкторы
        public ImagesPageVM()
        {
            id = global.GetID();

            global.SelectedImageItems.Clear();

            DirectoryParameters parameters = (DirectoryParameters)global.GetParameters(id);
            Directory = parameters.Directory;

            if (!global.ImageItemCollections.ContainsKey(Directory))
            {
                global.ImageItemCollections.Clear();
                global.ImageItemCollections.Add(Directory, ImageItems);

                model.ShowEmptyPreviews += ShowEmptyPreviews;
                model.GetPictures(Directory, Sorting.Name);
            }
            else ImageItems = global.ImageItemCollections[Directory];

            //global.MouseLeftButtonUp += HideRubberBand;

            // MouseHook.OnMouseUp += MouseHook_OnMouseUp;
        }

        private void MouseHook_OnMouseUp(object sender, Point p)
        {
            HideRubberBand();
        }
        #endregion

        #region Команды
        public ICommand WindowLoaded
        {
            get => new DelegateCommand(() =>
            {
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
                var span = new TimeSpan(0, 5, 0);
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
                //if (Mouse.LeftButton == MouseButtonState.Pressed && global.DraggableImage != null)
                //{
                //    Thread thread = new Thread(() =>
                //    {
                //        const double trigger = 50;
                //        const double offset = 5;
                //        const int sleep = 1;
                //        Point pos = ScrollViewMousePos;

                //        while (ScrollViewMousePos.Y <= trigger)
                //        {
                //            LineUp(offset);
                //            Thread.Sleep(sleep);
                //        }
                //        while (ScrollViewMousePos.Y >= GetScrollViewHeigh() - trigger)
                //        {
                //            LineDown(offset);
                //            Thread.Sleep(sleep);
                //        }
                //    });

                //    thread.Start();
                //}
            });
        }

        public ICommand PageLeftButtonDown
        {
            get => new DelegateCommand(() =>
            {
                NeedRuberBand = true;
                startDragPoint = GetMausePosOnPage();
                PageCaptureMouse();
            });
        }

        public ICommand PageMouseUp
        {
            get => new DelegateCommand(() =>
            {
                HideRubberBand();
                NeedRuberBand = false;
                PageReleaseMouseCapture();
            });
        }

        public ICommand PageMouseWheel
        {
            get => new DelegateCommand<MouseWheelEventArgs>((e) =>
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    if (e.Delta > 0)
                    {
                        PreviewHeight += 10;
                        PreviewWidth += 10;
                    }
                    else if (e.Delta < 0)
                    {
                        PreviewHeight -= 10;
                        PreviewWidth -= 10;
                    }
                }
                else
                {
                    if (e.Delta > 0)
                    {
                        LineUp(scrollOffset);
                    }
                    else if (e.Delta < 0)
                    {
                        LineDown(scrollOffset);
                    }
                }
                e.Handled = true;
            });
        }

        public ICommand PageMouseMove
        {
            get => new DelegateCommand<MouseEventArgs>((e) =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed && NeedRuberBand)
                {
                    Point currentPos = GetMausePosOnPage();
                    if (global.DraggableImage != null)
                    {
                        //DraggableMargin = new Thickness(currentPos.X - global.ImageMouseX, currentPos.Y - global.ImageMouseY, 0, 0);
                    }
                    else
                    {
                        double Right = startDragPoint.X < currentPos.X ? startDragPoint.X : currentPos.X;
                        double Bottom = startDragPoint.Y < currentPos.Y ? startDragPoint.Y : currentPos.Y;

                        RubberBandRenderTransform = new TranslateTransform(Right, Bottom);
                        double width = Math.Abs(currentPos.X - startDragPoint.X);
                        double height = Math.Abs(currentPos.Y - startDragPoint.Y);
                        RubberBandWidth = width;
                        RubberBandHeight = height;

                        var viewportHeight = GetViewportHeight();
                        var verticalOffset = GetVerticalOffset();

                        double x2 = Right + width;
                        double y2 = Bottom + height;

                        Point startPoint = new Point(Right, Bottom);
                        Point endPoint = new Point(x2, y2);

                        foreach (var item in ImageItems)
                        {
                            Point topLeft = item.GetPositionOn(page);
                            Point bottomRight = new Point(topLeft.X + PreviewWidth, topLeft.Y + PreviewHeight);

                            LogicRectangle rectangle1 = new LogicRectangle(topLeft, bottomRight);
                            LogicRectangle rectangle2 = new LogicRectangle(startPoint, endPoint);

                            if (rectangle1.Intersects(rectangle2))
                            {
                                item.IsSelected = true;
                            }
                            else item.IsSelected = false;
                        }
                    }
                }
            });
        }

        private bool _IsCrossing(Point _startPoint1, Point _endPoint1, Point _startPoint2, Point _endPoint2)
        {
            Point[] points = new Point[]
            {
                _startPoint1,
                new Point(_endPoint1.X, _startPoint1.Y),
                new Point(_startPoint1.X, _endPoint1.Y),
                _endPoint1,
            };

            bool flag = false;
            foreach (var point in points)
            {
                if (IsPointInRange(_startPoint2, _endPoint2, point))
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        private bool IsPointInRange(Point start, Point end, Point point)
        {
            if ((start.X <= point.X && start.Y <= point.Y) && (point.X <= end.X && point.Y <= end.Y))
                return true;
            return false;
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

            //List<ImageItem> temp = ImageItems.ToList();
            //switch (sorting)
            //{
            //    case Sorting.Name:
            //        temp.Sort((Left, Top) => Left.Name.CompareTo(Top.Name));
            //        break;
            //    case Sorting.CreationDate:
            //        temp.Sort((Left, Top) => Left.CreationDate.CompareTo(Top.CreationDate));
            //        break;
            //    case Sorting.ModificationDate:
            //        temp.Sort((Left, Top) => Left.ModificationDate.CompareTo(Top.ModificationDate));
            //        break;
            //}

            for (int i = 0; i < temp.Count(); i++)
            {
                int j = ImageItems.IndexOf(temp.ElementAt(i));
                if (j != i)
                    ImageItems.Move(j, i);
            }
        }

        private void ShowPreview(BitmapImage bmi, string str, int i)
        {
            //ImageItems.Add(img);

            ImageItems[i].Preview = bmi;
            ImageItems[i].Directory = str;//Fill(img);
            RaisePropertyChanged("ImageItems");
        }

        private void ShowEmptyPreviews(List<string> paths)
        {
            foreach (var p in paths)
            {
                ImageItems.Add(new ImageItem(p));
                //break;
            }

            foreach (var item in ImageItems)
            {
                item.ShowThumbnail();
                //break;
            }

            //ThreadsService threads = ThreadsService.GetInstance();
            //threads.StartNewThread(() =>
            //{
            //    Thread.Sleep(100);
            //    foreach (var item in ImageItems)
            //    {
            //        item.ShowThumbnail();
            //    }
            //});
        }

        private void HideRubberBand()
        {
            RubberBandHeight = 0;
            RubberBandWidth = 0;
        }
        #endregion
    }
}