using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace PicControls
{
    /// <summary>
    /// Логика взаимодействия для PreviewControl.xaml
    /// </summary>
    public partial class PreviewControl : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperteyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public PreviewControl()
        {
            InitializeComponent();

            var dp1 = DependencyPropertyDescriptor.FromProperty(ImageSourceProperty, typeof(PreviewControl));
            dp1?.AddValueChanged(this, ImageSourceHandler);

            var dp2 = DependencyPropertyDescriptor.FromProperty(IsSelectedProperty, typeof(PreviewControl));
            dp2?.AddValueChanged(this, IsSelectedHandler);
        }

        private void ImageSourceHandler(object sender, EventArgs eventArgs)
        {
            int i = 0;
        }

        private void IsSelectedHandler(object sender, EventArgs eventArgs)
        {
            
        }
        private Thickness normalBorderThickness;

        public BitmapImage ImageSource
        {
            get { return (BitmapImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(BitmapImage), typeof(PreviewControl), new PropertyMetadata(null));


        public BitmapImage DefaultImage
        {
            get { return (BitmapImage)GetValue(DefaultImageProperty); }
            set {SetValue(DefaultImageProperty, value);}
        }

        public static readonly DependencyProperty DefaultImageProperty =
            DependencyProperty.Register("DefaultImage", typeof(BitmapImage), typeof(PreviewControl), new PropertyMetadata(null));


        public Visibility SelectionVisibility
        {
            get { return (Visibility)GetValue(IsSelectionEnabledProperty); }
            set { 
                SetValue(IsSelectionEnabledProperty, value);
            }
        }

        public static readonly DependencyProperty IsSelectionEnabledProperty =
            DependencyProperty.Register("SelectionVisibility", typeof(Visibility), typeof(PreviewControl), new PropertyMetadata(Visibility.Hidden));


        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        public static readonly DependencyProperty ImageNameProperty =
            DependencyProperty.Register("ImageName", typeof(string), typeof(PreviewControl), new PropertyMetadata());


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(PreviewControl), new PropertyMetadata(false));


        public ICommand OnSelected
        {
            get { return (ICommand)GetValue(OnSelectedProperty); }
            set { SetValue(OnSelectedProperty, value); }
        }

        public static readonly DependencyProperty OnSelectedProperty =
            DependencyProperty.Register("OnSelected", typeof(ICommand), typeof(PreviewControl), new PropertyMetadata());


        public ICommand OnUnselected
        {
            get { return (ICommand)GetValue(OnUnselectedProperty); }
            set { SetValue(OnUnselectedProperty, value); }
        }

        public static readonly DependencyProperty OnUnselectedProperty =
            DependencyProperty.Register("OnUnselected", typeof(ICommand), typeof(PreviewControl), new PropertyMetadata());


        public ICommand PreviewMouseMoveCommand
        {
            get { return (ICommand)GetValue(OnPreviewMouseMoveProperty); }
            set { SetValue(OnPreviewMouseMoveProperty, value); }
        }

        public static readonly DependencyProperty OnPreviewMouseMoveProperty =
            DependencyProperty.Register("PreviewMouseMoveCommand", typeof(ICommand), typeof(PreviewControl), new PropertyMetadata());


        private void selection_Checked(object sender, RoutedEventArgs e)
        {
            OnSelected?.Execute(e);
        }

        private void selection_Unchecked(object sender, RoutedEventArgs e)
        {
            OnUnselected?.Execute(e);
        }

        private void Image_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            PreviewMouseMoveCommand?.Execute(e);
        }

        private void Image_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.LeftCtrl)
            {
                MessageBox.Show("!");
            }
        }

        private void previewControl_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void previewControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            normalBorderThickness = previewControl.BorderThickness;
            previewControl.BorderThickness = new Thickness(previewControl.BorderThickness.Left + 2);
        }

        private void previewControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            previewControl.BorderThickness = normalBorderThickness;
        }

        private void previewControl_MouseLeave(object sender, MouseEventArgs e)
        {
            previewControl.BorderThickness = normalBorderThickness;
        }

        private void previewControl_Loaded(object sender, RoutedEventArgs e)
        {
            normalBorderThickness = previewControl.BorderThickness;
        }
    }
}
