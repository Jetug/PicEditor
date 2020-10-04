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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using NaturalSort.Extension;
using PicEditor.View;

namespace PicEditor.ViewModel
{
    class RenamingWindowVM : ViewModelBase
    {
        #region Свойства
        public string NewName { get; set; }
        public Window View { get; private set; }
        #endregion

        #region Делегаты
        public Action CloseWindow;
        public Action<string> RenameImages;
        #endregion

        #region Конструкторы
        public RenamingWindowVM()
        {
            RenamingWindow renamingWindow = new RenamingWindow();
            renamingWindow.DataContext = this;
            View = renamingWindow;
        }
        #endregion

        #region Команды
        public ICommand Rename
        {
            get => new DelegateCommand(() =>
            {
                RenameImages(NewName);
                CloseWindow();
            });
        }
        #endregion
    }
}
