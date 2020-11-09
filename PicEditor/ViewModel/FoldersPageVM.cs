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
using PicEditor.View;

namespace PicEditor.ViewModel
{
    class FoldersPageVM : ViewModelBase
    {
        #region Константы
        private const int defaultSize = 170;
        private const int defaultWidgh = 150;
        private const int defaultHeigh = 150;
        #endregion

        #region Свойства
        public string NewName { get; set; }
        public int SortParamIndex { get; set; } = -1;
        public BitmapImage PictureSource { get; set; }
        public double PreviewWidth { get; set; } = defaultSize;
        public double PreviewHeight { get; set; } = defaultSize;
        public double ScrollViewHeight { get; set; }
        public Point ScrollViewMousePos { get; set; }
        public bool IsChecked { get; set; } = false;

        public ObservableCollection<FolderItem> FolderItems { get; set; } = new ObservableCollection<FolderItem>();
        #endregion

        #region Делегаты
        public delegate Point PointHandler();
        public delegate double SizeHandler();
        public Action<ImageItem, int> ShowPicture { get; private set; }
        #endregion

        #region Поля
        private int id;
        private MediaSearcher model = new MediaSearcher();
        private NavigationService navigation = NavigationService.GetInstance();
        #endregion

        #region Конструкторы
        public FoldersPageVM()
        {
            //id = navigation.GetID();
            //Parameters param = navigation.GetParameters(id);
            //switch (param)
            //{

            //    case VoidParameters vp:

            //    break;
            //    default:
            //        break;
            //}
            model.ShowFolder = (bmi, str) => {
                var fi = new FolderItem(bmi, str);
                fi.ShowThumbnail();
                FolderItems.Add(fi);
            };
            model.ShowFolders();
        }
        #endregion

        #region Commands
        public ICommand WindowLoaded
        {
            get => new DelegateCommand(() =>
            {
                FolderItems.Clear();
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

        public ICommand SelectAll
        {
            get => new DelegateCommand(() =>
            {
                for (int i = 0; i < FolderItems.Count; i++)
                {
                    FolderItems[i].IsSelected = true;
                }
            });
        }

        public ICommand UnselectAll
        {
            get => new DelegateCommand(() =>
            {
                for (int i = 0; i < FolderItems.Count; i++)
                {
                    FolderItems[i].IsSelected = false;
                }
            });
        }

        public ICommand ClearPage
        {
            get => new DelegateCommand(() =>
            {
                FolderItems.Clear();
            });
        }

        public ICommand AddFolders
        {
            get => new DelegateCommand(() =>
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    model.ScanNestedFolders(fbd.SelectedPath);
                }
            });
        }

        public ICommand FolderDrop
        {
            get => new DelegateCommand<DragEventArgs>((e) =>
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var f in files)
                {
                    string path;
                    if (LinkConverter.IsLink(f))
                        path = LinkConverter.GetLnkTarget(f);
                    else
                        path = f;

                    if (Directory.Exists(path))
                    {
                        bool b = true;
                        foreach (var fi in FolderItems)
                        {
                            if (fi.Directory == path)
                            {
                                b = false;
                                break;
                            }
                        }

                        //Parallel.ForEach(FolderItems, (fi, state) =>
                        //{
                        //    if (fi.Directory == path)
                        //    {
                        //        b = false;
                        //        state.Break();
                        //    }
                        //});

                        if (b == true) model.ScanNestedFolders(path);
                    }
                }
            });
        }
        #endregion

        #region Приватные методы
        private void SortBy(Sorting sorting)
        {
            IOrderedEnumerable<FolderItem> temp = null;
            switch (sorting)
            {
                case Sorting.Name:
                    temp = FolderItems.OrderBy(p => p.Name, StringComparison.OrdinalIgnoreCase.WithNaturalSort());
                    break;
                case Sorting.CreationDate:
                    temp = FolderItems.OrderBy(p => p.CreationDate);
                    break;
                case Sorting.ModificationDate:
                    temp = FolderItems.OrderBy(p => p.ModificationDate);
                    break;
            }
            for (int i = 0; i < temp.Count(); i++)
            {
                FolderItems.Move(FolderItems.IndexOf(temp.ElementAt(i)), i);
            }
        }
        #endregion
    }
}