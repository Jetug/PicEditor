using PicEditor.ViewModel;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PicEditor.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Thumbnail.xaml
    /// </summary>
    public partial class Thumbnail : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnProperteyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public Thumbnail()
        {
            InitializeComponent();

            var dp1 = DependencyPropertyDescriptor.FromProperty(ImageSourceProperty, typeof(Thumbnail));
            dp1?.AddValueChanged(this, ImageSourceHandler);

            var dp2 = DependencyPropertyDescriptor.FromProperty(IsSelectedProperty, typeof(Thumbnail));
            dp2?.AddValueChanged(this, IsSelectedHandler);
        }

        private void ImageSourceHandler(object sender, EventArgs eventArgs)
        {

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

        public object PointToScreen(object empty)
        {
            throw new NotImplementedException();
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(BitmapImage), typeof(Thumbnail), new PropertyMetadata(null));


        public BitmapImage DefaultImage
        {
            get { return (BitmapImage)GetValue(DefaultImageProperty); }
            set { SetValue(DefaultImageProperty, value); }
        }

        public static readonly DependencyProperty DefaultImageProperty =
            DependencyProperty.Register("DefaultImage", typeof(BitmapImage), typeof(Thumbnail), new PropertyMetadata(null));


        public Visibility SelectionVisibility
        {
            get { return (Visibility)GetValue(IsSelectionEnabledProperty); }
            set
            {
                SetValue(IsSelectionEnabledProperty, value);
            }
        }

        public static readonly DependencyProperty IsSelectionEnabledProperty =
            DependencyProperty.Register("SelectionVisibility", typeof(Visibility), typeof(Thumbnail), new PropertyMetadata(Visibility.Hidden));


        public string ImageName
        {
            get { return (string)GetValue(ImageNameProperty); }
            set { SetValue(ImageNameProperty, value); }
        }

        public static readonly DependencyProperty ImageNameProperty =
            DependencyProperty.Register("ImageName", typeof(string), typeof(Thumbnail), new PropertyMetadata());


        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(Thumbnail), new PropertyMetadata(false));


        public ICommand OnSelected
        {
            get { return (ICommand)GetValue(OnSelectedProperty); }
            set { SetValue(OnSelectedProperty, value); }
        }

        public static readonly DependencyProperty OnSelectedProperty =
            DependencyProperty.Register("OnSelected", typeof(ICommand), typeof(Thumbnail), new PropertyMetadata());


        public ICommand OnUnselected
        {
            get { return (ICommand)GetValue(OnUnselectedProperty); }
            set { SetValue(OnUnselectedProperty, value); }
        }

        public static readonly DependencyProperty OnUnselectedProperty =
            DependencyProperty.Register("OnUnselected", typeof(ICommand), typeof(Thumbnail), new PropertyMetadata());


        public ICommand PreviewMouseMoveCommand
        {
            get { return (ICommand)GetValue(OnPreviewMouseMoveProperty); }
            set { SetValue(OnPreviewMouseMoveProperty, value); }
        }

        public static readonly DependencyProperty OnPreviewMouseMoveProperty =
            DependencyProperty.Register("PreviewMouseMoveCommand", typeof(ICommand), typeof(Thumbnail), new PropertyMetadata());

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }

        public static readonly DependencyProperty ClickCommandProperty =
            DependencyProperty.Register("ClickCommand", typeof(ICommand), typeof(Thumbnail), new PropertyMetadata());

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
            if (e.Key == Key.LeftCtrl)
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
            ClickCreator.OnMouseDown(this);
        }

        private void previewControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            previewControl.BorderThickness = normalBorderThickness;
            if (ClickCreator.OnMouseUp(this))
                ClickCommand.Execute(e);
        }

        private void previewControl_MouseLeave(object sender, MouseEventArgs e)
        {
            previewControl.BorderThickness = normalBorderThickness;
            ClickCreator.OnMouseLeave();
        }

        private void previewControl_Loaded(object sender, RoutedEventArgs e)
        {
            normalBorderThickness = previewControl.BorderThickness;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    previewControl.BorderThickness = normalBorderThickness;
        //    ClickCommand?.Execute(e);
        //}
    }
}
