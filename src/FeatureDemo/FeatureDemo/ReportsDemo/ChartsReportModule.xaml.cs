using System.ComponentModel;

namespace ReportsDemo {
    public sealed partial class ChartsReportModule : ReportDemoModule {
        public ChartsReportModule() {
            Report = new ChartsReport();
            this.InitializeComponent();
        }
    }
}
