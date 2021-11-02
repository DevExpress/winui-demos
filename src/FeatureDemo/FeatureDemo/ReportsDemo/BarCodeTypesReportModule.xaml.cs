using System.ComponentModel;

namespace ReportsDemo {
    public sealed partial class BarCodeTypesReportModule : ReportDemoModule {
        public BarCodeTypesReportModule() {
            Report = new BarCodeTypesReport();
            this.InitializeComponent();
        }
    }
}
