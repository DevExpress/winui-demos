









namespace ReportsDemo {
    
    public partial class ChartsReport : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "FeatureDemo.ReportsDemo.Reports.ChartsReport.vsrepx");

            
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.BottomMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("BottomMargin");
            this.ReportHeader = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportHeaderBand>("ReportHeader");
            this.topMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("topMarginBand1");
            this.DetailReport2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport2");
            this.DetailReport1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport1");
            this.xrChart1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRChart>("xrChart1");
            this.xrChart3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRChart>("xrChart3");
            this.xrChart2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRChart>("xrChart2");
            this.xrLabel3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel3");
            this.xrLabel2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel2");
            this.xrLabel1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel1");
            this.lbTitle = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("lbTitle");
            this.VerticalDetail = reportInitializer.GetControl<DevExpress.XtraReports.UI.VerticalDetailBand>("VerticalDetail");
            this.ReportHeader1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportHeaderBand>("ReportHeader1");
            this.ReportFooter = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportFooterBand>("ReportFooter");
            this.xrLabel5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel5");
            this.xrChart6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRChart>("xrChart6");
            this.xrLabel4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel4");
            this.Detail1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail1");
            this.GroupHeader1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupHeaderBand>("GroupHeader1");
            this.GroupHeader2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupHeaderBand>("GroupHeader2");
            this.xrTable1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable1");
            this.xrTableRow3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow3");
            this.xrTableCell2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell2");
            this.xrTableCell3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell3");
            this.xrTableCell7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell7");
            this.xrChart4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRChart>("xrChart4");
            this.xrLine1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLine>("xrLine1");
            this.xrTable2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable2");
            this.xrTableRow1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow1");
            this.xrTableRow2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow2");
            this.xrTableCell1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell1");
            this.xrTableCell4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell4");
            this.xrTableCell5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell5");
            this.xrTableCell6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell6");
            this.xrCrossBandBox1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRCrossBandBox>("xrCrossBandBox1");

            
            this.federationDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.DataFederation.FederationDataSource>("federationDataSource1");
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");
            this.jsonDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Json.JsonDataSource>("jsonDataSource1");

            
            this.Year = reportInitializer.GetCalculatedField("Year");
            this.Month = reportInitializer.GetCalculatedField("Month");
            this.Quarter = reportInitializer.GetCalculatedField("Quarter");
            this.TotalSales = reportInitializer.GetCalculatedField("TotalSales");
        }
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.XRChart xrChart1;
        private DevExpress.XtraReports.UI.XRChart xrChart3;
        private DevExpress.XtraReports.UI.XRChart xrChart2;
        private DevExpress.DataAccess.DataFederation.FederationDataSource federationDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel lbTitle;
        private DevExpress.XtraReports.UI.VerticalDetailBand VerticalDetail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRChart xrChart6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRChart xrChart4;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRCrossBandBox xrCrossBandBox1;
        private DevExpress.XtraReports.UI.CalculatedField Year;
        private DevExpress.XtraReports.UI.CalculatedField Month;
        private DevExpress.XtraReports.UI.CalculatedField Quarter;
        private DevExpress.XtraReports.UI.CalculatedField TotalSales;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.DataAccess.Json.JsonDataSource jsonDataSource1;
    }
}
