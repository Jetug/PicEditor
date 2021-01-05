using PicEditor.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicEditor.ViewModel
{
    static class ItemToThumbnailConverter
    {
        public static Thumbnail Convert(ImageItem imageItem)
        {
            Thumbnail thumbnail = new Thumbnail()
            {
                Height = imageItem.Height,
                Width = imageItem.Width,
                ImageName = imageItem.Name,
                ImageSource = imageItem.Preview,
                IsHitTestVisible = false,
            };
            return thumbnail;
        }
    }
}
