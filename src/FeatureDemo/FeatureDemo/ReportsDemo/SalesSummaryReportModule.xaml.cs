using System.ComponentModel;

namespace ReportsDemo {
    public sealed partial class SalesSummaryReportModule : ReportDemoModule {
        public SalesSummaryReportModule() {
            Report = new SalesSummaryReport();
            this.InitializeComponent();
        }
    }
}
