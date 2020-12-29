using PicEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PicEditor.View
{
    /// <summary>
    /// Логика взаимодействия для DraggableThumbnail.xaml
    /// </summary>
    public partial class DraggableThumbnail : Window
    {
        public DraggableThumbnail()
        {
            InitializeComponent();
            DraggableThumbnailVM vm = (DataContext as DraggableThumbnailVM);
            vm.CloseWindow = Close;
        }
    }
}
