using System.ComponentModel;

namespace ReportsDemo {
    public sealed partial class TableReportModule : ReportDemoModule {
        public TableReportModule() {
            Report = new TableReport();
            this.InitializeComponent();
        }
    }
}
