using Microsoft.UI.Xaml;
using FeatureDemo.ViewModel;
using DevExpress.WinUI.Core.Internal;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Input;

namespace FeatureDemo.View {
    public sealed partial class DemoModulePage : DemoPageBase {

        public DemoModulePage() {
            InitializeComponent();            
        }
        private async void DemoModuleLoadingIndicator_Loaded(object sender, RoutedEventArgs e) {
            await Task.Yield();
            pivot.Visibility = Visibility.Visible;
        }
        ManipulationModes GetPivotManipulationMode(bool prevNextEnable) => prevNextEnable ? ManipulationModes.System : ManipulationModes.None;
    }
}
