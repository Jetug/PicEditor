using System.Windows;

namespace PicEditor.ViewModel
{
    static class ClickCreator
    {
        private static object pressedElement;

        public static void OnMouseDown(object element)
        {
            pressedElement = element;
            //MouseHook.OnMouseUp += CancelClick;
        }

        public static bool OnMouseUp(object element) => (pressedElement == element);

        public static void OnMouseLeave()
        {
            pressedElement = null;
        }

        private static void CancelClick(object sender, Point p)
        {
            pressedElement = null;
            MouseHook.OnMouseUp -= CancelClick;
        }
    }
}
