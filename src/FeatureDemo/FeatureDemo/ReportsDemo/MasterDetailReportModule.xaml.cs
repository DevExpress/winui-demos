using System.ComponentModel;

namespace ReportsDemo {
    public sealed partial class MasterDetailReportModule : ReportDemoModule {
        public MasterDetailReportModule() {
            Report = new MasterDetailReport();
            this.InitializeComponent();
        }
    }
}
