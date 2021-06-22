using FeatureDemo.Common;

namespace GridDemo {
    public sealed partial class RowAlternationDemo : DemoModuleView {
        public MultiSelectionViewModel ViewModel { get; } = new MultiSelectionViewModel();
        public RowAlternationDemo() {
            this.InitializeComponent();
        }
    }
}
