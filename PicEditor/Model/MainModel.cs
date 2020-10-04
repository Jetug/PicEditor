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

    class MainModel
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
        public Action<ImageItem, int> ShowPreview;
        public Action<int> ShowEmptyPreviews;
        public Action<FolderItem> ShowFolder;
        #endregion

        #region Поля
        private List<String> whiteList = new List<string>();
        private List<String> blackList = new List<string>();
        #endregion

        #region Конструкторы
        public MainModel()
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

                //Application.Current.Dispatcher.Invoke(() => ShowEmptyPreviews(temp.Count));

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
                //Application.Current.Dispatcher.Invoke(() => MessageBox.Show(pics.GetType().ToString()));
                //pics = tempPics.ToList();
                //pics.RemoveAll((p) => !IsImage(p));
                //Application.Current.Dispatcher.Invoke(() => GiveCount(pics.Count()));
                for (int i = 0; i < pics.Count(); i++)
                {
                    if (IsImage(pics.ElementAt(i)))
                    {
                        string path = pics.ElementAt(i);
                        BitmapImage icon = GetPreview(path, ImagePreviewSize);
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ShowPreview(new ImageItem(icon, path), i);
                        });
                        Thread.Sleep(1);
                    }
                }
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
                            GetFolders(dir);
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
                            ScanFolder(line);
                            GetFolders(line);
                        }
                    }
                }

                //threads.StartNewThread(() => SearchFolders(@"E:\TestPictures"));
            });
        }

        private void GetFolders(string directory)
        {
            var dirs = Directory.GetDirectories(directory).Where(Sort).ToList();
            //dirs.Insert(0, directory);
            foreach (var dir in dirs)
            {
                ScanFolder(dir);
                threads.StartNewThread(() => GetFolders(dir));
            }
        }

        private void ScanFolder(string directory)
        {
            var files = Directory.GetFiles(directory).Where(Sort);
            foreach (var file in files)
            {
                if (imageExtensions.Contains(Path.GetExtension(file)))
                {
                    BitmapImage icon = GetPreview(file, FolderPreviewSize);
                    Application.Current.Dispatcher.Invoke(() => ShowFolder(new FolderItem(icon, directory)));
                    break;
                }
            }
        }

        public void RenameFiles(List<ImageItem> images, string renaming)
        {
            for (int i = 0; i < images.Count; i++)
            {
                string newName = renaming + $"-{i + 1}";
                string dir = Path.GetDirectoryName(images[i].FullPath);
                string newPath = Path.Combine(dir, newName + images[i].Extension);
                if (!File.Exists(newPath))
                {
                    File.Move(images[i].FullPath, newPath);
                    images[i].FullPath = newPath;
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
                        File.SetCreationTime(images[i].FullPath, date);
                        break;
                    case DateType.Modification:
                        File.SetLastWriteTime(images[i].FullPath, date);
                        break;
                    case DateType.CreationAndModification:
                        File.SetCreationTime(images[i].FullPath, date);
                        File.SetLastWriteTime(images[i].FullPath, date);
                        break;
                }
                date += step;
            }
        }

        public async void GetPreviewAsync(string path, int size, Action<BitmapImage> ShowIcon)
        {
            await Task.Run(() => ShowIcon(GetPreview(path, size)));
        }

        public BitmapImage GetPreview(string path, int size)
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