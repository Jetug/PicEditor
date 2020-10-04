using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicEditor.Model
{
    class FileSearcher
    {
        private const string whiteListPath = "WhiteList.txt";
        private const string blackListPath = "BlackList.txt";

        #region Поля
        private List<String> whiteList = new List<string>();
        private List<String> blackList = new List<string>();
        #endregion

        public FileSearcher()
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
    }
}
