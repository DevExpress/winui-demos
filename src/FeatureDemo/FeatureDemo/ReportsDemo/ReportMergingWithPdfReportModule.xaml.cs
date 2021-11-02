using System.ComponentModel;

namespace ReportsDemo {
    public sealed partial class ReportMergingWithPdfReportModule : ReportDemoModule {
        public ReportMergingWithPdfReportModule() {
            Report = new ReportMergingWithPdfReport();
            this.InitializeComponent();
        }
    }
}
