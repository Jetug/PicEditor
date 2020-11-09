using DevExpress.Mvvm.POCO;
using PicControls;
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
    public partial class ImagesPage : Page
    {
        public ImagesPage()
        {
            InitializeComponent();
        }

        private void MainWin_Loaded(object sender, RoutedEventArgs e)
        {
            ImagesPageVM vm = (DataContext as ImagesPageVM);

            vm.LineUp = LineUp;
            vm.LineDown = LineDown;

            vm.GetMausePosOnScrollView = () => Mouse.GetPosition(scrollView);
            vm.GetMausePosOnPage = () => Mouse.GetPosition(page);
            vm.GetScrollViewHeigh = () => scrollView.ActualHeight;
            vm.PageCaptureMouse = () => 
            {
                if (!page.IsMouseCaptured)
                    page.CaptureMouse();
            };
            vm.PageReleaseMouseCapture = () =>
            {
                if (page.IsMouseCaptured)
                    page.ReleaseMouseCapture();
            };

            vm.GetVerticalOffset = () => scrollView.VerticalOffset;
            vm.GetViewportHeight = () => scrollView.ViewportHeight;
            vm.GetItemPosition = GetPreviewControlPosition;
            vm.page = page;
        }

        private Point GetPreviewControlPosition(int i)
        {
            ThumbnailControl control = (ThumbnailControl)itemsControl.Items[i];
            Point relativePoint = control.TransformToAncestor(page).Transform(new Point(0, 0));
            return relativePoint;
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

        private void wrapPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void wrapPanel_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
