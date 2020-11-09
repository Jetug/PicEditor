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
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace PicEditor.ViewModel
{
    class ImageItem : ViewModelBase
    {
        #region Константы
        private const int imgWidgh = 150;
        private const int imgHeigh = 150;
        private const int imgSize = 150;
        #endregion

        #region Свойства
        private string path = "";
        public string Directory
        {
            get => path;
            set
            {
                path = value;
                Name = Path.GetFileNameWithoutExtension(value);
                Extension = Path.GetExtension(value);
                CreationDate = File.GetCreationTime(value);
                ModificationDate = File.GetLastWriteTime(value);
            }
        }
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public BitmapImage Preview { get; set; } = null;
        private BitmapImage _fullImage = null;
        public BitmapImage FullImage
        {
            get
            {
                if (_fullImage == null)
                    _fullImage = model.GetFullImage(Directory);
                return _fullImage;
            }
        }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Point ImageMousePos { get; set; }
        public PointHandler<Visual> GetPositionOn { get; set; }

        public double Width { get; set; } = imgWidgh;
        public double Height { get; set; } = imgHeigh;
        private bool isSelected = false;
        public bool IsSelected 
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertiesChanged();
                }
            }
        }
        #endregion

        #region Поля
        private MediaSearcher model = new MediaSearcher();
        private NavigationService global = NavigationService.GetInstance();
        #endregion

        #region Конструкторы

        public ImageItem(string fullPath)
        {
            Directory = fullPath;
            //ShowThumbnail();
        }
        #endregion

        #region Публичные мотоды
        public Image GetImage()
        {
            Image image = new Image()
            {
                Source = Preview,
                Height = Height,
                Width = Width,
            };
            return image;
        }

        public async void ShowThumbnail()
        {
            //model.GetThumbnailAsync(Directory, imgSize, (bmi) => Preview = bmi);
            
            await Task.Factory.StartNew(() => Preview = model.GetThumbnail(Directory, imgSize));
        }

        public void Fill(ImageItem it)
        {
            Preview = it.Preview;
            //FullImage = it.FullImage;
            Directory = it.Directory;
        }

        public void SetDate()
        {
            CreationDate = File.GetCreationTime(Directory);
            ModificationDate = File.GetLastWriteTime(Directory);
        }
        #endregion

        #region Команды
        public ICommand ImageSelected
        {
            get => new DelegateCommand(() =>
            {
                global.SelectedImageItems.Add(this);
            });
        }

        public ICommand ImageUnselected
        {
            get => new DelegateCommand(() =>
            {
                global.SelectedImageItems.Remove(this);
            });
        }

        public ICommand ImageMouseEnter
        {
            get => new DelegateCommand(() =>
            {
                //if (Keyboard.Modifiers == ModifierKeys.Control && Mouse.LeftButton == MouseButtonState.Pressed)
                //{
                //    Select();
                //}
            });
        }

        public ICommand ImageLeftButtonDown
        {
            get => new DelegateCommand(() =>
            {
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Select();
                }
                else
                {
                    global.ClickedElement = this;
                    global.ImageMouseX = ImageMousePos.X;
                    global.ImageMouseY = ImageMousePos.Y;
                }
            });
        }

        public ICommand ImageLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {
                if (global.ClickedElement == this)
                {
                    //global.OpenPicture(FullImage);
                    global.OpenPictureTR(Directory);
                }
                else if (Keyboard.Modifiers != ModifierKeys.Control)
                {
                    //int i = ImagesPageVM.ImageItems.IndexOf(this);
                    var path = Path.GetDirectoryName(Directory);
                    var imageItems = global.ImageItemCollections[path];
                    int i = imageItems.IndexOf(this);
                    global.DraggableImage = null;

                    foreach (var item in global.SelectedImageItems)
                    {
                        imageItems.Remove(item);
                        imageItems.Insert(i, item);
                    }
                    //global.SelectedImageItems.Clear();
                }
            });
        }
        
        public ICommand ThumbnailClick
        {
            get => new DelegateCommand(() =>
            {
                //if (Keyboard.Modifiers == ModifierKeys.Control)
                //{
                //    Select();
                //}
                global.OpenPictureTR(Directory);
            });
        }

        public ICommand ImageMouseMove
        {
            get => new DelegateCommand(() =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed && Keyboard.Modifiers != ModifierKeys.Control && global.ClickedElement == this)
                {
                    global.DraggableImage = this;
                    global.SelectedImageItems.Add(this);
                    global.ShowPreview(Preview);
                }
            });
        }

        public ICommand RemoveImage
        {
            get => new DelegateCommand(() =>
            {
                var path = Path.GetDirectoryName(Directory);
                var imageItems = global.ImageItemCollections[path];
                imageItems.Remove(this);
                File.Delete(Directory);
            });
        }

        public ICommand RenameImage
        {
            get => new DelegateCommand(() =>
            {

            });
        }
        #endregion

        private void Select()
        {
            if (IsSelected)
            {
                IsSelected = false;
                global.SelectedImageItems.Remove(this);
            }
            else
            {
                IsSelected = true;
                global.SelectedImageItems.Add(this);
            }
        }
    }
}