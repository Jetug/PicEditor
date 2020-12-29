using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PicEditor.Model
{
    class Saver
    {
        #region Константы
        private const string whiteListPath = "WhiteList.txt";
        private const string blackListPath = "BlackList.txt";
        private readonly string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".bmp", ".png", ".gif", };

        readonly ThreadsService threads = ThreadsService.GetInstance();
        #endregion

        #region Поля
        private List<String> whiteList = new List<string>();
        private List<String> blackList = new List<string>();
        #endregion

        #region Публичные методы
        public List<string> GetFolderPaths()
        {

            return null;
        }

        public void AddFolder(string path)
        {
            using(StreamWriter sw = new StreamWriter(whiteListPath, true))
            {
                sw.WriteLine(path);
            }
        }

        public void RemoveFolder()
        {

        }

        //public void ScanNestedFolders(string directory)
        //{
        //    threads.StartNewThread(() =>
        //    {
        //        ScanFolder(directory);
        //        ScanOnlyNestedFolders(directory);
        //    });
        //}

        //private void ScanOnlyNestedFolders(string directory)
        //{
        //    var dirs = Directory.GetDirectories(directory).Where(Sort);
        //    foreach (var dir in dirs)
        //    {
        //        ScanFolder(dir);
        //        threads.StartNewThread(() => ScanOnlyNestedFolders(dir));
        //    }
        //}

        //private void ScanFolder(string directory)
        //{
        //    var files = Directory.GetFiles(directory).Where(Sort);
        //    foreach (var file in files)
        //    {
        //        if (imageExtensions.Contains(Path.GetExtension(file)))
        //        {
        //            //BitmapImage icon = GetPreview(file, FolderPreviewSize);
        //            Application.Current.Dispatcher.Invoke(() => ShowFolder(null, directory));
        //            break;
        //        }
        //    }
        //}
        #endregion

        bool Sort(string path)
        {
            if (FileAttributes.System != (File.GetAttributes(path) & FileAttributes.System)
            && FileAttributes.Hidden != (File.GetAttributes(path) & FileAttributes.Hidden)
            && !whiteList.Contains(path) && !blackList.Contains(path))
            {
                return true;
            }
            return false;
        }
    }
}
