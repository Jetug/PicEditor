using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using PicEditor.ViewModel;
using NaturalSort.Extension;
using DevExpress.Mvvm.Native;
using System.Security.Cryptography;
using System.Globalization;
using System.Reflection;

namespace PicEditor.Model
{
    enum Sorting
    {
        None,
        Name,
        CreationDate,
        ModificationDate
    }

    enum DateType
    {
        Creation,
        Modification,
        CreationAndModification
    }

    class MediaSearcher
    {
        #region Константы
        public const int ImagePreviewSize = 150;
        public const int FolderPreviewSize = 225;

        private const string whiteListPath = "WhiteList.txt";
        private const string blackListPath = "BlackList.txt";

        private readonly string[] imageExtensions = new string[] { ".jpg", ".jpeg", ".bmp", ".png", ".gif", };
        readonly ThreadsService threads = ThreadsService.GetInstance();
        #endregion

        #region Делегаты
        //public Action<BitmapImage, string, int> ShowPreview;
        public Action<List<string>> ShowEmptyPreviews;
        public Action<BitmapImage, string> ShowFolder;
        #endregion

        #region Поля
        private List<string> whiteList = new List<string>();
        private List<string> blackList = new List<string>();
        #endregion

        #region Конструкторы
        public MediaSearcher()
        {
            if (File.Exists(whiteListPath))
            {
                using (StreamReader sr = new StreamReader(whiteListPath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        whiteList.Add(line);
                    }
                }
            }
        }
        #endregion

        #region Публичные методы
        public void GetPictures(string directory, Sorting sorting = Sorting.None, Action end = null)
        {
            threads.StartNewThread(() =>
            {
                List<string> temp = Directory.GetFiles(directory).ToList();
                temp.RemoveAll((pic) => !IsImage(pic));
                IOrderedEnumerable<string> pics = null;

                switch (sorting)
                {
                    case Sorting.Name:
                        pics = temp.OrderBy(p => p, StringComparison.OrdinalIgnoreCase.WithNaturalSort());
                        break;
                    case Sorting.CreationDate:
                        pics = temp.OrderBy(p => File.GetCreationTime(p));
                        break;
                    case Sorting.ModificationDate:
                        pics = temp.OrderBy(p => File.GetLastWriteTime(p));
                        break;
                }

                Application.Current.Dispatcher.Invoke(() => ShowEmptyPreviews(pics.ToList()));

                Application.Current.Dispatcher.Invoke(() => end?.Invoke());
            });
        }

        public void SearchFolders(string path)
        {
            var dirs = Directory.GetDirectories(path).Where(Sort);

            Parallel.ForEach(dirs, (dir) =>
            {
                try
                {
                    var files = Directory.GetFiles(dir).Where(Sort);

                    foreach (var file in files)
                    {
                        if (imageExtensions.Contains(Path.GetExtension(file)))
                        {
                            ScanNestedFolders(dir);
                            break;
                        }
                    }

                    try
                    {
                        Thread thread = new Thread(() => SearchFolders(dir));
                        thread.Start();
                    }
                    catch (UnauthorizedAccessException) { }
                }
                catch (UnauthorizedAccessException) { }
            });

        }

        public void ShowFolders()
        {
            threads.StartNewThread(() =>
            {
                if (File.Exists(whiteListPath))
                {
                    using (StreamReader sr = new StreamReader(whiteListPath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (Directory.Exists(line))
                            {
                                //ScanFolder(line);
                                ScanNestedFolders(line);
                            }
                        }
                    }
                }
                //threads.StartNewThread(() => SearchFolders(@"E:\TestPictures"));
            });
        }

        public void ScanNestedFolders(string directory)
        {
            threads.StartNewThread(() =>
            {
                ScanFolder(directory);
                ScanOnlyNestedFolders(directory);
            });
        }

        private void ScanOnlyNestedFolders(string directory)
        {
            var dirs = Directory.GetDirectories(directory).Where(Sort);
            foreach (var dir in dirs)
            {
                ScanFolder(dir);
                threads.StartNewThread(() => ScanOnlyNestedFolders(dir));
            }
        }

        private void ScanFolder(string directory)
        {
            var files = Directory.GetFiles(directory).Where(Sort);
            foreach (var file in files)
            {
                if (imageExtensions.Contains(Path.GetExtension(file)))
                {
                    //BitmapImage icon = GetPreview(file, FolderPreviewSize);
                    Application.Current.Dispatcher.Invoke(() => ShowFolder(null, directory));
                    break;
                }
            }
        }

        public void RenameFiles(List<ImageItem> images, string renaming)
        {
            for (int i = 0; i < images.Count; i++)
            {
                string newName = renaming + $"-{i + 1}";
                string dir = Path.GetDirectoryName(images[i].Directory);
                string newPath = Path.Combine(dir, newName + images[i].Extension);
                if (!File.Exists(newPath))
                {
                    File.Move(images[i].Directory, newPath);
                    images[i].Directory = newPath;
                }
            }
        }

        public void EditDate(List<ImageItem> images, DateTime date, TimeSpan step, DateType dateType)
        {
            for (int i = 0; i < images.Count; i++)
            {
                switch (dateType)
                {
                    case DateType.Creation:
                        File.SetCreationTime(images[i].Directory, date);
                        break;
                    case DateType.Modification:
                        File.SetLastWriteTime(images[i].Directory, date);
                        break;
                    case DateType.CreationAndModification:
                        File.SetCreationTime(images[i].Directory, date);
                        File.SetLastWriteTime(images[i].Directory, date);
                        break;
                }
                date += step;
            }
        }

        public BitmapImage GetFolderThumbnail(string directory, int size)
        {
            var files = Directory.GetFiles(directory).Where(Sort);
            return GetThumbnail(files.ElementAt(0), size);
            //Application.Current.Dispatcher.Invoke(() => ShowIcon(bmi));

        }

        public void GetThumbnailAsync(string path, int size, Action<BitmapImage> ShowIcon)
        {
            threads.StartNewThread(() =>
            {
                var bmi = GetThumbnail(path, size);
                Application.Current.Dispatcher.Invoke(() => ShowIcon(bmi));
            });
        }

        public BitmapImage GetThumbnail(string path, int size)
        {
            Bitmap image = new Bitmap(path);

            int x = 0;
            int y = 0;
            int width = image.Width;
            int height = image.Height;

            if (width > height)
            {
                x = (width - height) / 2;
                width = height;
            }
            else if (height > width)
            {
                y = (height - width) / 2;
                height = width;
            }
            image = image.Clone(new Rectangle(x, y, width, height), PixelFormat.Format16bppRgb555);

            //BitmapImage bmi = new BitmapImage(new Uri(path));
            //BitmapSource bms = new CroppedBitmap(bmi, new Int32Rect(x, y, width, height));

            //return Stoi(bms, size);

            Bitmap preview = new Bitmap(image, new System.Drawing.Size(size, size));
            return BitmapToImage(preview);
        }

        public BitmapImage GetFullImage(string path)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            return image;
        }

        public void GetFullImageTR(string path, Action<BitmapImage> ShowImage)
        {
            Thread thread = new Thread(() =>
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(path);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                image.Freeze();
                Application.Current.Dispatcher.Invoke(() =>
                {
                   ShowImage(image);
                });
            });
            thread.Start();
        }

        public void RemoveImage()
        {

        }
        #endregion

        #region Приватные методы

        private void CutImage(BitmapImage bmi)
        {
            BitmapSource topHalf = new CroppedBitmap(bmi, new Int32Rect());
        }

        private bool IsImage(string name)
        {
            string ext = Path.GetExtension(name);
            bool b = imageExtensions.Contains(ext);
            return b;
        }

        private BitmapImage BitmapToImage(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                bitmapimage.Freeze();

                return bitmapimage;
            }
        }

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

        private BitmapImage Stoi(BitmapSource bitmapSource, int size)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            using (MemoryStream memory = new MemoryStream())
            {
                BitmapImage bmi = new BitmapImage();

                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(memoryStream);

                memoryStream.Position = 0;
                bmi.BeginInit();
                bmi.StreamSource = memoryStream;
                bmi.DecodePixelWidth = size;
                bmi.DecodePixelHeight = size;
                bmi.EndInit();

                memoryStream.Close();

                return bmi;
            }
        }
        #endregion
    }
}