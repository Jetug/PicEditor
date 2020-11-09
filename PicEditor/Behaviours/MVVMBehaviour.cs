using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PicEditor.Behaviours
{
    class MVVMBehaviour : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty MousePosProperty = DependencyProperty.Register(
            "MousePos", typeof(Point), typeof(MVVMBehaviour), new PropertyMetadata(default(Point)));

        public static readonly DependencyProperty ActualHeightProperty = DependencyProperty.Register(
            "ActualHeight", typeof(double), typeof(MVVMBehaviour), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ActualWidthProperty = DependencyProperty.Register(
            "ActualWidth", typeof(double), typeof(MVVMBehaviour), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty GetPositionOnProperty = DependencyProperty.Register(
            "GetPositionOn", typeof(PointHandler<Visual>), typeof(MVVMBehaviour), new PropertyMetadata());

        public Point MousePos
        {
            get { return (Point)GetValue(MousePosProperty); }
            set { SetValue(MousePosProperty, value); }
        }

        public double ActualHeight
        {
            get { return (double)GetValue(ActualHeightProperty); }
            set { SetValue(ActualHeightProperty, value); }
        }

        public double ActualWidth
        {
            get { return (double)GetValue(ActualWidthProperty); }
            set { SetValue(ActualWidthProperty, value); }
        }

        public PointHandler<Visual> GetPositionOn
        {
            get { return (PointHandler<Visual>)GetValue(GetPositionOnProperty); }
            set { SetValue(GetPositionOnProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
            ActualHeight = AssociatedObject.ActualHeight;
            ActualWidth = AssociatedObject.ActualWidth;
            GetPositionOn = GetPreviewControlPosition;
        }

        private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var pos = mouseEventArgs.GetPosition(AssociatedObject);
            MousePos = pos;
        }

        private Point GetPreviewControlPosition(Visual parent)
        {
            Point relativePoint = AssociatedObject.TransformToAncestor(parent).Transform(new Point(0, 0));
            return relativePoint;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
        }
    }
}
