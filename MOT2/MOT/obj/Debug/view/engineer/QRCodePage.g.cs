﻿#pragma checksum "..\..\..\..\view\engineer\QRCodePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DB50F65E658AF75A0DC966D3EC10CDAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MOT.view.worker;
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


namespace MOT.view.engineer {
    
    
    /// <summary>
    /// QRCodePage
    /// </summary>
    public partial class QRCodePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\view\engineer\QRCodePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTip;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\view\engineer\QRCodePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbProductNum;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\view\engineer\QRCodePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReScan;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\view\engineer\QRCodePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLight;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\view\engineer\QRCodePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\..\view\engineer\QRCodePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInputDone;
        
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
            System.Uri resourceLocater = new System.Uri("/MOT;component/view/engineer/qrcodepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\view\engineer\QRCodePage.xaml"
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
            
            #line 9 "..\..\..\..\view\engineer\QRCodePage.xaml"
            ((MOT.view.engineer.QRCodePage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\..\view\engineer\QRCodePage.xaml"
            ((MOT.view.engineer.QRCodePage)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Page_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.labelTip = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.tbProductNum = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.btnReScan = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\view\engineer\QRCodePage.xaml"
            this.btnReScan.Click += new System.Windows.RoutedEventHandler(this.BtnReScan_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnLight = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\..\view\engineer\QRCodePage.xaml"
            this.btnLight.Click += new System.Windows.RoutedEventHandler(this.BtnLight_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\..\..\view\engineer\QRCodePage.xaml"
            this.btnBack.Click += new System.Windows.RoutedEventHandler(this.BtnBack_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnInputDone = ((System.Windows.Controls.Button)(target));
            
            #line 106 "..\..\..\..\view\engineer\QRCodePage.xaml"
            this.btnInputDone.Click += new System.Windows.RoutedEventHandler(this.BtnInputDone_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

