using PicEditor.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace PicEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CheckForIllegalCrossThreadCall
            MainWindowVM vm = (DataContext as MainWindowVM);
            vm.Window = window;
            vm.GetMausePosOnWindow = () => Mouse.GetPosition(window);
            //ImagesPage imagesPage = new ImagesPage();
            //frame.Navigate(imagesPage);
            vm.GoBack = () => { if (frame.CanGoBack) frame.GoBack(); };
            vm.GoForward = () => { if (frame.CanGoForward) frame.GoForward(); };

            NavigationService navigation = NavigationService.GetInstance();
            navigation.Navigate = frame.Navigate;
            navigation.GetMousePosition = () => Mouse.GetPosition(window);
        }

        private void pic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                pic.Visibility = Visibility.Hidden;
        }
    }
}
