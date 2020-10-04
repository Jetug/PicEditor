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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicEditor.View
{
    /// <summary>
    /// Логика взаимодействия для ImagesPanel.xaml
    /// </summary>
    public partial class ImagesPanel : UserControl
    {
        public ImagesPanel()
        {
            InitializeComponent();
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            ImagesPageVM vm = (DataContext as ImagesPageVM);

            vm.LineUp = LineUp;
            vm.LineDown = LineDown;

            vm.GetMausePosOnScrollView = () => Mouse.GetPosition(scrollView);
            vm.GetMausePosOnWindow = () => Mouse.GetPosition(MainWin);
            vm.GetScrollViewHeigh = () => scrollView.ActualHeight;
        }

        private void LineUp(double offset)
        {
            Application.Current.Dispatcher.Invoke(() =>
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset - offset));
        }

        private void LineDown(double offset)
        {
            Application.Current.Dispatcher.Invoke(() =>
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + offset));
        }

        private void scrollView_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = Mouse.GetPosition(scrollView);
            //tb.Text = pos.ToString();
        }
    }
}
