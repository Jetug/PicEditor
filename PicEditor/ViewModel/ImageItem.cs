using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using PicEditor.Model;
using PicEditor.View;
using System.Threading.Tasks;
using PicEditor.UserControls;

namespace PicEditor.ViewModel
{
    class ImageItem : ThumbnailItem
    {
        #region Свойства
        public new double Width
        {
            get => ((ImagesPageVM)global.CurrentViewModel).ThumdnailWidth;
        }
        public new double Height
        {
            get => ((ImagesPageVM)global.CurrentViewModel).ThumdnailWidth;
        }

        public string Extension { get; private set; }

        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    if (value == true)
                        global.SelectedImageItems.Add(this);
                    else global.SelectedImageItems.Remove(this);
                    isSelected = value;
                    RaisePropertiesChanged();
                }
            }
        }
        #endregion

        #region Поля
        private bool canBeDragged = false;
        private bool needToShowDragging = true;
        #endregion

        #region Конструкторы

        public ImageItem(string directory) : base(directory)
        {
            Extension = Path.GetExtension(directory);
            MouseHook.OnMouseUp += (s, e) =>
            {
                canBeDragged = false;
                needToShowDragging = true;
            };
        }
        #endregion

        #region Публичные мотоды

        public override async void ShowThumbnail()
        {
            Preview = await Task.Factory.StartNew(() => model.GetThumbnail(Directory, imgSize));
        }

        //public void Fill(ImageItem it)
        //{
        //    Preview = it.Preview;
        //    //FullImage = it.FullImage;
        //    Directory = it.Directory;
        //}

        //public void SetDate()
        //{
        //    CreationDate = File.GetCreationTime(Directory);
        //    ModificationDate = File.GetLastWriteTime(Directory);
        //}
        #endregion

        #region Команды

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
                if (Keyboard.Modifiers != ModifierKeys.Control)
                {
                    global.ClickedElement = this;
                    global.ImageMouseX = ImageMousePos.X;
                    global.ImageMouseY = ImageMousePos.Y;
                    canBeDragged = true;
                }
            });
        }

        public ICommand ImageLeftButtonUp
        {
            get => new DelegateCommand(() =>
            {
                if (Keyboard.Modifiers != ModifierKeys.Control)
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
                if (Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Select();
                }
                else
                {
                    global.OpenPictureTR(Directory);
                }
            });
        }

        public ICommand ImageMouseMove
        {
            get => new DelegateCommand(() =>
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed && Keyboard.Modifiers != ModifierKeys.Control && canBeDragged && needToShowDragging)
                {
                    needToShowDragging = false;
                    global.DraggableImage = this;
                    //MouseHook.OnMouseUp += CancelDragging;
                    global.SelectedImageItems.Add(this);
                    global.ShowThumbnail(Preview);
                    ViewCreator viewCreator = ViewCreator.GetInstance();
                    viewCreator.CreateView<DraggableThumbnail>(new DraggableThumbnailParametrs(this)).Show();
                }
            });
        }

        private void CancelDragging(object sender, Point p)
        {
            global.DraggableImage = null;
            MouseHook.OnMouseUp -= CancelDragging;
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