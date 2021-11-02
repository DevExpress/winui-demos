using DevExpress.WinUI.Core.Internal;
using FeatureDemo;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.UI.ViewManagement;

namespace RibbonDemo {
    public sealed partial class RibbonToolbarModule : DemoModuleView {
        public RibbonToolbarModule() {
            ViewModel = new RibbonToolBarViewModel();
            this.InitializeComponent();
            Unloaded += OnUnloaded;
        }

        public RibbonToolBarViewModel ViewModel { get; }

        void OnUnloaded(object sender, RoutedEventArgs e) {
            foreach(var popup in VisualTreeHelper.GetOpenPopups(((App)App.Current).MainWindow)) 
                popup.IsOpen = false;
            try {
                InputPane.GetForCurrentView().TryHide();
            }
            catch { }
        }
    }
}
