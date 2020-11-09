using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
namespace PicEditor.Behaviours
{
    class ScrollViewerBehaviour : Behavior<ScrollViewer>
    {
        //ScrollViewerBehaviour()
        //{
        //    var dp1 = DependencyPropertyDescriptor.FromProperty(VerticalOffsetProperty, typeof(ScrollViewerBehaviour));
        //    dp1?.AddValueChanged(this, VerticalOffsetHandler);
        //}

        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double), typeof(ScrollViewerBehaviour), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ViewportHeightProperty =
            DependencyProperty.Register("ViewportHeight", typeof(double), typeof(ScrollViewerBehaviour), new PropertyMetadata(default(double)));

        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        public double ViewportHeight
        {
            get { return (double)GetValue(ViewportHeightProperty); }
            set { SetValue(ViewportHeightProperty, value); }
        }

        private void VerticalOffsetHandler(object sender, EventArgs eventArgs)
        {
            VerticalOffset = AssociatedObject.VerticalOffset;
        }

        protected override void OnAttached()
        {
            var dp1 = DependencyPropertyDescriptor.FromProperty(VerticalOffsetProperty, typeof(ScrollViewerBehaviour));
                dp1?.AddValueChanged(this, VerticalOffsetHandler);

            VerticalOffset = AssociatedObject.VerticalOffset;
            ViewportHeight = AssociatedObject.ViewportHeight;
        }

        protected override void OnDetaching()
        {

        }
    }
}
