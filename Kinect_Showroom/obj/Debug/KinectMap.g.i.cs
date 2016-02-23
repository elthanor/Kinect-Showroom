﻿#pragma checksum "..\..\KinectMap.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7B2F97DA7413CFDBBE03745741CABD6A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;
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


namespace Kinect_Showroom {
    
    
    /// <summary>
    /// KinectMap
    /// </summary>
    public partial class KinectMap : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\KinectMap.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MapImage;
        
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
            System.Uri resourceLocater = new System.Uri("/Kinect_Showroom;component/kinectmap.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\KinectMap.xaml"
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
            
            #line 9 "..\..\KinectMap.xaml"
            ((Kinect_Showroom.KinectMap)(target)).Loaded += new System.Windows.RoutedEventHandler(this.KinectMap_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 12 "..\..\KinectMap.xaml"
            ((Microsoft.Kinect.Toolkit.Controls.KinectCircleButton)(target)).AddHandler(Microsoft.Kinect.Toolkit.Controls.KinectRegion.HandPointerEnterEvent, new System.EventHandler<Microsoft.Kinect.Toolkit.Controls.HandPointerEventArgs>(this.BackBtn_OnHandPointer));
            
            #line default
            #line hidden
            
            #line 12 "..\..\KinectMap.xaml"
            ((Microsoft.Kinect.Toolkit.Controls.KinectCircleButton)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.BackBtn_OnMouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\KinectMap.xaml"
            ((Microsoft.Kinect.Toolkit.Controls.KinectCircleButton)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonBase_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MapImage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
