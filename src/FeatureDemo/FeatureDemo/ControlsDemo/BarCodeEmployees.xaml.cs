using FeatureDemo.Common;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo.ControlsDemo {
    public sealed partial class BarCodeEmployees : DemoModuleView {
        public BarCodeEmployees() {
            ViewModel = new BarCodeEmployeesViewModel();
            InitializeComponent();
        }

        public BarCodeEmployeesViewModel ViewModel { get; }
    }
}
