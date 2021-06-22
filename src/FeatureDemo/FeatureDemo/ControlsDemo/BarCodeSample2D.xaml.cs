using FeatureDemo.Common;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo.ControlsDemo {
    public sealed partial class BarCodeSample2D : DemoModuleView {
        public BarCodeSample2D() {
            this.InitializeComponent();
            ViewModel = new BarCodeSample2DViewModel();
        }
        public BarCodeSample2DViewModel ViewModel { get; set; }
        public bool GetRadioButtonIsCheckedValue(int currentIndex, int visibleIndex) => currentIndex == visibleIndex;
    }
}
