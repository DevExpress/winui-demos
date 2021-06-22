using FeatureDemo.Common;

namespace SchedulerDemo {
    public sealed partial class SchedulerDemoModule : DemoModuleView {
        public SchedulerDemoViewModel ViewModel { get; } = new SchedulerDemoViewModel();
        public SchedulerDemoModule() {
            this.InitializeComponent();
        }
    }
}
