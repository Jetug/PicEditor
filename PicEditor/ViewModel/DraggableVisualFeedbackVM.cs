using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PicEditor.ViewModel
{
    class DraggableVisualFeedbackVM : ViewModelBase
    {
        //#region Поля
        //private int id;
        //private ViewCreator viewCreator = ViewCreator.GetInstance();
        //#endregion

        //#region Свойства
        //public RenderTargetBitmap ImageSource { get; set; }
        //public double MaxWidth { get; set; }
        //public double MaxHeight { get; set; }
        //#endregion

        //public DraggableVisualFeedbackVM()
        //{
        //    id = viewCreator.GetID();
        //    DraggableVisualFeedbackParametrs param = (DraggableVisualFeedbackParametrs)viewCreator.GetParameters(id);
        //    UIElement uiElement = param.uiElement;

        //    var bounds = VisualTreeHelper.GetDescendantBounds(uiElement);
        //    var renderTargetBitmap = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96, 96, PixelFormats.Pbgra32);

        //    var drawingVisual = new DrawingVisual();
        //    using (var drawingContext = drawingVisual.RenderOpen())
        //    {
        //        var visualBrush = new VisualBrush(uiElement);
        //        drawingContext.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(), bounds.Size));
        //    }

        //    renderTargetBitmap.Render(drawingVisual);

        //    MaxWidth = renderTargetBitmap.PixelWidth;
        //    MaxHeight = renderTargetBitmap.PixelHeight;
        //    ImageSource = renderTargetBitmap;
        //}
    }
}
