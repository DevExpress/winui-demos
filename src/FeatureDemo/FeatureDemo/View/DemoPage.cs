using FeatureDemo.ViewModel;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo.View {
    public class DemoPageBase : Page {
        public MainViewModel ViewModel { get; } = MainViewModel.Instance;
        public DemoPageBase() { }
    }
}