using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo.Common {
    public class DemoModuleView : UserControl {
        public UIElement Options { get; set; }
        public bool HideOptionsInitially { get; set; }
        public bool ShowOptionsInOverlay { get; set; }
        public bool HasOptions { get { return Options != null; } }
        public double OptionsPaneWidth { get; set; } = 300;
        
        public DemoModuleView() {
            DataContextChanged += OnDataContextChanged;
        }
        void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args) {
            FrameworkElement rootContentElement = Content as FrameworkElement;
            if(rootContentElement != null)
                rootContentElement.DataContext = args.NewValue;
            FrameworkElement optionsRootElement = Options as FrameworkElement;
            if(optionsRootElement != null)
                optionsRootElement.DataContext = args.NewValue;
        }
    }
    public class DemoSubModuleView : UserControl {
        public UIElement Options { get; set; }
    }
}