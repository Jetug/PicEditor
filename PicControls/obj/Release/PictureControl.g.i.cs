﻿#pragma checksum "..\..\PictureControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B2C7118888BA086F2DAF36492179C4A61042493DA354E103AB2E5D6CC47A2D05"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using PicControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfAnimatedGif;


namespace PicControls {
    
    
    /// <summary>
    /// PictureControl
    /// </summary>
    public partial class PictureControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\PictureControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PicControls.PictureControl pictureControl;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\PictureControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PicControls;component/picturecontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PictureControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.pictureControl = ((PicControls.PictureControl)(target));
            
            #line 12 "..\..\PictureControl.xaml"
            this.pictureControl.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.pictureControl_MouseDown);
            
            #line default
            #line hidden
            
            #line 13 "..\..\PictureControl.xaml"
            this.pictureControl.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.pictureControl_MouseWheel);
            
            #line default
            #line hidden
            
            #line 14 "..\..\PictureControl.xaml"
            this.pictureControl.MouseMove += new System.Windows.Input.MouseEventHandler(this.pictureControl_MouseMove);
            
            #line default
            #line hidden
            
            #line 15 "..\..\PictureControl.xaml"
            this.pictureControl.Loaded += new System.Windows.RoutedEventHandler(this.pictureControl_Loaded);
            
            #line default
            #line hidden
            
            #line 16 "..\..\PictureControl.xaml"
            this.pictureControl.Initialized += new System.EventHandler(this.pictureControl_Initialized);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 20 "..\..\PictureControl.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.image = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\PictureControl.xaml"
            this.image.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.image_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

