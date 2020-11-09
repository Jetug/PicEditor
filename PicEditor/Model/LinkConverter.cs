using Shell32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicEditor.Model
{
    static class LinkConverter
    {
        /// <summary>
        /// Получает цель ссылки.
        /// </summary>
        /// <param name="lnkPath">Расположение ссылки.</param>
        /// <returns></returns>
        public static string GetLnkTarget(string lnkPath)
        {
            var shl = new Shell();         // Move this to class scope
            lnkPath = Path.GetFullPath(lnkPath);
            var dir = shl.NameSpace(Path.GetDirectoryName(lnkPath));
            var itm = dir.Items().Item(Path.GetFileName(lnkPath));
            var lnk = (ShellLinkObject)itm.GetLink;
            return lnk.Target.Path;
        }

        public static bool IsLink(string path)
        {
            string pathOnly = Path.GetDirectoryName(path);
            string filenameOnly = Path.GetFileName(path);

            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);
            if (folderItem != null)
            {
                return folderItem.IsLink;
            }
            return false;
        }
    }
}
