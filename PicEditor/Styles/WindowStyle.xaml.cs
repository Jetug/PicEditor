using System.Windows;

namespace PicEditor.Styles
{
    public partial class WindowStyle : ResourceDictionary
    {
        public WindowStyle()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(sender);
            window.Close();
        }

        private void MaximizeRestoreClick(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(sender);
            window.WindowState ^= WindowState.Maximized;
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            var window = GetWindow(sender);
            window.WindowState = WindowState.Minimized;
        }

        private Window GetWindow(object sender) => (Window)((FrameworkElement)sender).TemplatedParent;
    }
}