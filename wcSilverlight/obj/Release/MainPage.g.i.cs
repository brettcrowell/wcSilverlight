﻿#pragma checksum "C:\Users\bcrowell\Dropbox\school\spring2012\web\lapse\wclapse\wcSilverlight - Copy\wcSilverlight\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B6969467234004909E188DBE62F812F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace wcSilverlight {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Shapes.Rectangle videoPanel;
        
        internal System.Windows.Controls.TabControl main;
        
        internal System.Windows.Controls.TabItem tiWebcam;
        
        internal System.Windows.Controls.CheckBox cbReinit;
        
        internal System.Windows.Controls.ListBox lbDevices;
        
        internal System.Windows.Controls.TextBox tbReinit;
        
        internal System.Windows.Controls.CheckBox cbPreview;
        
        internal System.Windows.Controls.Label lblAvailable;
        
        internal System.Windows.Controls.Button btnTestImg;
        
        internal System.Windows.Controls.TabItem tiFilesystem;
        
        internal System.Windows.Controls.TextBox tbImgNum;
        
        internal System.Windows.Controls.Label lblImgName;
        
        internal System.Windows.Controls.TextBox tbImgName;
        
        internal System.Windows.Controls.Label lblImgNum;
        
        internal System.Windows.Controls.ProgressBar pbLapse;
        
        internal System.Windows.Controls.TabItem tiLapse;
        
        internal System.Windows.Controls.Label label1;
        
        internal System.Windows.Controls.Label lblInterval;
        
        internal System.Windows.Controls.Label lblFps;
        
        internal System.Windows.Controls.Label lblSeconds;
        
        internal System.Windows.Controls.Label lblHours;
        
        internal System.Windows.Controls.TextBox tbHours;
        
        internal System.Windows.Controls.TextBox tbSeconds;
        
        internal System.Windows.Controls.TextBox tbFPS;
        
        internal System.Windows.Controls.TextBox tbDelay;
        
        internal System.Windows.Controls.Button btnStartLapse;
        
        internal System.Windows.Controls.TextBox tbDelayStart;
        
        internal System.Windows.Controls.RadioButton rbIndefinite;
        
        internal System.Windows.Controls.RadioButton rbTimed;
        
        internal System.Windows.Controls.Label lbMode;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/wcSilverlight;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.videoPanel = ((System.Windows.Shapes.Rectangle)(this.FindName("videoPanel")));
            this.main = ((System.Windows.Controls.TabControl)(this.FindName("main")));
            this.tiWebcam = ((System.Windows.Controls.TabItem)(this.FindName("tiWebcam")));
            this.cbReinit = ((System.Windows.Controls.CheckBox)(this.FindName("cbReinit")));
            this.lbDevices = ((System.Windows.Controls.ListBox)(this.FindName("lbDevices")));
            this.tbReinit = ((System.Windows.Controls.TextBox)(this.FindName("tbReinit")));
            this.cbPreview = ((System.Windows.Controls.CheckBox)(this.FindName("cbPreview")));
            this.lblAvailable = ((System.Windows.Controls.Label)(this.FindName("lblAvailable")));
            this.btnTestImg = ((System.Windows.Controls.Button)(this.FindName("btnTestImg")));
            this.tiFilesystem = ((System.Windows.Controls.TabItem)(this.FindName("tiFilesystem")));
            this.tbImgNum = ((System.Windows.Controls.TextBox)(this.FindName("tbImgNum")));
            this.lblImgName = ((System.Windows.Controls.Label)(this.FindName("lblImgName")));
            this.tbImgName = ((System.Windows.Controls.TextBox)(this.FindName("tbImgName")));
            this.lblImgNum = ((System.Windows.Controls.Label)(this.FindName("lblImgNum")));
            this.pbLapse = ((System.Windows.Controls.ProgressBar)(this.FindName("pbLapse")));
            this.tiLapse = ((System.Windows.Controls.TabItem)(this.FindName("tiLapse")));
            this.label1 = ((System.Windows.Controls.Label)(this.FindName("label1")));
            this.lblInterval = ((System.Windows.Controls.Label)(this.FindName("lblInterval")));
            this.lblFps = ((System.Windows.Controls.Label)(this.FindName("lblFps")));
            this.lblSeconds = ((System.Windows.Controls.Label)(this.FindName("lblSeconds")));
            this.lblHours = ((System.Windows.Controls.Label)(this.FindName("lblHours")));
            this.tbHours = ((System.Windows.Controls.TextBox)(this.FindName("tbHours")));
            this.tbSeconds = ((System.Windows.Controls.TextBox)(this.FindName("tbSeconds")));
            this.tbFPS = ((System.Windows.Controls.TextBox)(this.FindName("tbFPS")));
            this.tbDelay = ((System.Windows.Controls.TextBox)(this.FindName("tbDelay")));
            this.btnStartLapse = ((System.Windows.Controls.Button)(this.FindName("btnStartLapse")));
            this.tbDelayStart = ((System.Windows.Controls.TextBox)(this.FindName("tbDelayStart")));
            this.rbIndefinite = ((System.Windows.Controls.RadioButton)(this.FindName("rbIndefinite")));
            this.rbTimed = ((System.Windows.Controls.RadioButton)(this.FindName("rbTimed")));
            this.lbMode = ((System.Windows.Controls.Label)(this.FindName("lbMode")));
        }
    }
}
