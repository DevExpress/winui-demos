using DevExpress.WinUI.Core.Internal;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;

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
