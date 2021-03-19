using FeatureDemo.Common;
using System;
using Windows.Foundation.Metadata;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using DevExpress.WinUI.Core.Extensions;

namespace FeatureDemo.ControlsDemo {
    public sealed partial class BarCodeSample : DemoModuleView {
        public BarCodeSample() {
            ViewModel = new BarCodeSampleViewModel();
            this.InitializeComponent();
        }
        public Visibility GetModuleItemVisibility(bool autoModule) => (!autoModule).ToVisibility();
        public BarCodeSampleViewModel ViewModel { get; set; }
    }
}
