﻿#pragma checksum "..\..\Store.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1B3CB49A224EC8CCA34D3528F224957519CC3C7DAB5B693777961F0672BBD42E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Heat_Exchamger_Lab {
    
    
    /// <summary>
    /// Store
    /// </summary>
    public partial class Store : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstExchanger;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstFluid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddExchanger;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddToHot;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddToCold;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEX;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtHF;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCF;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewExchanger;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnViewFluid;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteExchanger;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Store.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteFluid;
        
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
            System.Uri resourceLocater = new System.Uri("/Heat Exchamger Lab;component/store.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Store.xaml"
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
            this.lstExchanger = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.lstFluid = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.btnAddExchanger = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\Store.xaml"
            this.btnAddExchanger.Click += new System.Windows.RoutedEventHandler(this.btnAddExchanger_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnAddToHot = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\Store.xaml"
            this.btnAddToHot.Click += new System.Windows.RoutedEventHandler(this.btnAddToHot_Click_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnAddToCold = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\Store.xaml"
            this.btnAddToCold.Click += new System.Windows.RoutedEventHandler(this.btnAddToCold_Click_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtEX = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtHF = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtCF = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnViewExchanger = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\Store.xaml"
            this.btnViewExchanger.Click += new System.Windows.RoutedEventHandler(this.btnViewExchanger_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnViewFluid = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\Store.xaml"
            this.btnViewFluid.Click += new System.Windows.RoutedEventHandler(this.btnViewFluid_Click_1);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnDeleteExchanger = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Store.xaml"
            this.btnDeleteExchanger.Click += new System.Windows.RoutedEventHandler(this.btnDeleteExchanger_Click_1);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnDeleteFluid = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\Store.xaml"
            this.btnDeleteFluid.Click += new System.Windows.RoutedEventHandler(this.btnDeleteFluid_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

