﻿#pragma checksum "..\..\..\UserControls\Thumbnail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E9A1F4B7D314188203790AC91042B7F9AE6B8BCFD8A245D3784A728AC05D3B88"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Xpf.DXBinding;
using PicEditor.UserControls;
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


namespace PicEditor.UserControls {
    
    
    /// <summary>
    /// Thumbnail
    /// </summary>
    public partial class Thumbnail : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\UserControls\Thumbnail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PicEditor.UserControls.Thumbnail previewControl;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\UserControls\Thumbnail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox selection;
        
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
            System.Uri resourceLocater = new System.Uri("/PicEditor;component/usercontrols/thumbnail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\Thumbnail.xaml"
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
            this.previewControl = ((PicEditor.UserControls.Thumbnail)(target));
            
            #line 11 "..\..\..\UserControls\Thumbnail.xaml"
            this.previewControl.MouseEnter += new System.Windows.Input.MouseEventHandler(this.previewControl_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\UserControls\Thumbnail.xaml"
            this.previewControl.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.previewControl_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\UserControls\Thumbnail.xaml"
            this.previewControl.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.previewControl_MouseLeftButtonUp);
            
            #line default
            #line hidden
            
            #line 14 "..\..\..\UserControls\Thumbnail.xaml"
            this.previewControl.MouseLeave += new System.Windows.Input.MouseEventHandler(this.previewControl_MouseLeave);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\UserControls\Thumbnail.xaml"
            this.previewControl.Loaded += new System.Windows.RoutedEventHandler(this.previewControl_Loaded);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\UserControls\Thumbnail.xaml"
            this.previewControl.MouseMove += new System.Windows.Input.MouseEventHandler(this.previewControl_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 57 "..\..\..\UserControls\Thumbnail.xaml"
            ((System.Windows.Controls.Image)(target)).PreviewMouseMove += new System.Windows.Input.MouseEventHandler(this.Image_PreviewMouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.selection = ((System.Windows.Controls.CheckBox)(target));
            
            #line 85 "..\..\..\UserControls\Thumbnail.xaml"
            this.selection.Checked += new System.Windows.RoutedEventHandler(this.selection_Checked);
            
            #line default
            #line hidden
            
            #line 86 "..\..\..\UserControls\Thumbnail.xaml"
            this.selection.Unchecked += new System.Windows.RoutedEventHandler(this.selection_Unchecked);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

