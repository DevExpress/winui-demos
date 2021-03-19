using DevExpress.WinUI.Core.Internal;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ChartsDemo {
    public sealed partial class AreaSeriesViewModule : DemoModuleView {
        public AreaSeriesViewModule() {
            this.InitializeComponent();
            Loading += OnLoading;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        void OnLoading(FrameworkElement sender, object args) {
            DataContext = new AreaSeriesViewModel();
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            ((AreaSeriesViewModel)DataContext).Initialize();
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            DataContext = null;
        }
    }
}
