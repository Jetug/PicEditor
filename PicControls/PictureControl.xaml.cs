﻿using System;
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

namespace PicControls
{
    /// <summary>
    /// Логика взаимодействия для PictureControl.xaml
    /// </summary>
    public partial class PictureControl : UserControl
    {
        public PictureControl()
        {
            InitializeComponent();
        }

        #region DependencyProperty
        public BitmapImage ImageSource
        {
            get { return (BitmapImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(BitmapImage), typeof(PictureControl), new PropertyMetadata(null));

        public double BackgroundOpacity
        {
            get { return (double)GetValue(BackgroundOpacityProperty); }
            set { SetValue(BackgroundOpacityProperty, value); }
        }

        public static readonly DependencyProperty BackgroundOpacityProperty =
            DependencyProperty.Register("BackgroundOpacity", typeof(double), typeof(PictureControl), new PropertyMetadata(1.0));

        public ICommand BGMouseLeftButtonDown
        {
            get { return (ICommand)GetValue(BGMouseLeftButtonDownProperty); }
            set { SetValue(BGMouseLeftButtonDownProperty, value); }
        }

        public static readonly DependencyProperty BGMouseLeftButtonDownProperty =
            DependencyProperty.Register("BGMouseLeftButtonDown", typeof(ICommand), typeof(PictureControl), new PropertyMetadata());

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(PictureControl), new PropertyMetadata());

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(PictureControl), new PropertyMetadata());



        public ICommand ShowNextPicture
        {
            get { return (ICommand)GetValue(ShowNextPictureProperty); }
            set { SetValue(ShowNextPictureProperty, value); }
        }

        public static readonly DependencyProperty ShowNextPictureProperty =
            DependencyProperty.Register("ShowNextPicture", typeof(ICommand), typeof(PictureControl), new PropertyMetadata());



        public ICommand ShowPerviousPicture
        {
            get { return (ICommand)GetValue(ShowPerviousPictureProperty); }
            set { SetValue(ShowPerviousPictureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowPerviousPicture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowPerviousPictureProperty =
            DependencyProperty.Register("ShowPerviousPicture", typeof(ICommand), typeof(PictureControl), new PropertyMetadata());


        #endregion

        #region События
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BGMouseLeftButtonDown?.Execute(e);
            //PictureControl.Visibility = Visibility.Hidden;
        }

        private void pictureControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Mouse.XButton2 == MouseButtonState.Pressed)
            {
                //pictureControl.Visibility = Visibility.Hidden;
            }
        }

        private void pictureControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ShowNextPicture?.Execute(null);
            }
            else if (e.Delta < 0)
            {
                ShowPerviousPicture?.Execute(null);
            }
        }
        #endregion
    }
}
