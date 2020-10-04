using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Логика взаимодействия для Frame.xaml
    /// </summary>
    public partial class Frame : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Frame()
        {
            InitializeComponent();
        }

        public List<UserControl> History { get; private set; } = new List<UserControl>();

        public UserControl Test
        {
            get { return (UserControl)GetValue(TestProperty); }
            set 
            { 
                SetValue(TestProperty, value);
                History.Add(value);
                NotifyPropertyChanged();
                Frame frame = new Frame();
            }
        }

        public static DependencyProperty TestProperty =
            DependencyProperty.Register("Test", typeof(UserControl), typeof(Frame), new PropertyMetadata());


    }
}