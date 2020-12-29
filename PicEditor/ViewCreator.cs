using PicEditor.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PicEditor
{
    class ViewCreator
    {
        #region Singleton
        private static ViewCreator instance;

        private ViewCreator() { }

        public static ViewCreator GetInstance()
        {
            if (instance == null)
                instance = new ViewCreator();
            return instance;
        }
        #endregion

        #region Поля
        private NavigationService ns = NavigationService.GetInstance();
        #endregion

        #region Публичные методы
        public T CreateView<T>(IParameters parameters) where T : FrameworkElement, new()
        {
            //ConstructorParameters.Add(id + 1, parameters);
            T view = new T();
            ((INavigable)view.DataContext).Parameters = parameters;
            return view;
        }

        public void ShowPage<T>(IParameters parameters) where T : Page, new()
        {
            T page = CreateView<T>(parameters);
            ns.Navigate(page);
            ns.CurrentPage = page;
        }
        #endregion
    }

    interface IParameters { }

    struct VoidParameters : IParameters { }

    struct ImagesPageParameters : IParameters
    {
        public string directory;

        public ImagesPageParameters(string directory)
        {
            this.directory = directory;
        }
    }

    struct DraggableVisualFeedbackParametrs: IParameters
    {
        public UIElement uiElement;

        public DraggableVisualFeedbackParametrs(UIElement uiElement)
        {
            this.uiElement = uiElement;
        }
    }

    struct DraggableThumbnailParametrs : IParameters
    {
        public ImageItem imageItem;

        public DraggableThumbnailParametrs(ImageItem imageItem)
        {
            this.imageItem = imageItem;
        }
    }
}