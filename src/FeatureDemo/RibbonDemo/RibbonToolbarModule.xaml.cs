using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI.ViewManagement;

namespace RibbonDemo {
    public sealed partial class RibbonToolbarModule : DemoModuleView {
        public RibbonViewModel ViewModel { get { return DataContext as RibbonViewModel; } }
        public RibbonToolbarModule() {
            this.InitializeComponent();
            Unloaded += OnUnloaded;
        }

        void OnUnloaded(object sender, RoutedEventArgs e) {
            var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
            foreach(var popup in popups) {
                popup.IsOpen = false;
            }
            InputPane.GetForCurrentView().TryHide();
        }
    }
}
