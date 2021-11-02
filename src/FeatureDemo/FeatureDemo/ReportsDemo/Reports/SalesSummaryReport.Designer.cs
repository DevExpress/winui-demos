









namespace ReportsDemo {
    
    public partial class SalesSummaryReport : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "FeatureDemo.ReportsDemo.Reports.SalesSummaryReport.vsrepx");

            
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.BottomMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("BottomMargin");
            this.topMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("topMarginBand1");
            this.crossTab1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRCrossTab>("crossTab1");
            this.crossTabCell1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell1");
            this.crossTabCell2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell2");
            this.crossTabCell3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell3");
            this.crossTabCell4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell4");
            this.crossTabCell5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell5");
            this.crossTabCell6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell6");
            this.crossTabCell7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell7");
            this.crossTabCell8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell8");
            this.crossTabCell9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell9");
            this.crossTabCell10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell10");
            this.crossTabCell11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell11");
            this.crossTabCell12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell12");
            this.crossTabCell13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell13");
            this.crossTabCell14 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell14");
            this.crossTabCell15 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell15");
            this.crossTabCell16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell16");
            this.crossTabCell17 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell17");
            this.crossTabCell18 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell18");
            this.crossTabCell19 = reportInitializer.GetControl<DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell>("crossTabCell19");
            this.xrPictureBox1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPictureBox>("xrPictureBox1");
            this.xrPageInfo1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPageInfo>("xrPageInfo1");
            this.xrLabel2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel2");

            
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");

            
            this.crossTabGeneralStyle1 = reportInitializer.GetStyle("crossTabGeneralStyle1");
            this.crossTabHeaderStyle1 = reportInitializer.GetStyle("crossTabHeaderStyle1");
            this.crossTabDataStyle1 = reportInitializer.GetStyle("crossTabDataStyle1");
            this.crossTabTotalStyle1 = reportInitializer.GetStyle("crossTabTotalStyle1");
        }
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.XRCrossTab crossTab1;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell1;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell2;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell3;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell4;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell5;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell6;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell7;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell8;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell9;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell10;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell11;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell12;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell13;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell14;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell15;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell16;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell17;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell18;
        private DevExpress.XtraReports.UI.CrossTab.XRCrossTabCell crossTabCell19;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabGeneralStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabHeaderStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabDataStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle crossTabTotalStyle1;
    }
}
