using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PicEditor
{
    class ThreadsService
    {
        #region Singleton
        private static ThreadsService instance;

        public static ThreadsService GetInstance()
        {
            if (instance == null)
                instance = new ThreadsService();
            return instance;
        }

        private ThreadsService() { }
        #endregion

        #region Поля
        private readonly List<Thread> threads = new List<Thread>();
        #endregion

        #region Методы
        public Thread StartNewThread(ThreadStart start)
        {
            Thread thread = null;
            thread = new Thread(() =>
            {
                start();
                threads.Remove(thread);
            });
            threads.Add(thread);
            thread.Start();

            return thread;
        }

        public void CloseAllThreads()
        {
            Parallel.ForEach(threads, (tread) => tread.Abort());
        }
        #endregion
    }
}
