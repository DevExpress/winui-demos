









namespace ReportsDemo {
    
    public partial class MasterDetailReport : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "FeatureDemo.ReportsDemo.Reports.MasterDetailReport.vsrepx");

            
            this.TopMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("TopMargin");
            this.BottomMargin = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("BottomMargin");
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.ReportHeader = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportHeaderBand>("ReportHeader");
            this.DetailReport = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport");
            this.PageFooter = reportInitializer.GetControl<DevExpress.XtraReports.UI.PageFooterBand>("PageFooter");
            this.xrTable2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable2");
            this.xrTable1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable1");
            this.xrTableRow2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow2");
            this.xrTableCell6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell6");
            this.xrTableCell7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell7");
            this.xrTableCell8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell8");
            this.xrTableCell9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell9");
            this.xrTableCell10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell10");
            this.xrTableRow1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow1");
            this.xrTableCell1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell1");
            this.xrTableCell2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell2");
            this.xrTableCell3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell3");
            this.xrTableCell4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell4");
            this.xrTableCell5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell5");
            this.xrLabel5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel5");
            this.xrLabel4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel4");
            this.xrTable6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable6");
            this.xrLabel2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel2");
            this.xrLabel1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel1");
            this.xrTableRow6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow6");
            this.xrTableRow7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow7");
            this.xrTableCell23 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell23");
            this.xrTableCell29 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell29");
            this.xrTableCell24 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell24");
            this.xrTableCell31 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell31");
            this.xrTableCell25 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell25");
            this.xrTableCell26 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell26");
            this.xrTableCell30 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell30");
            this.xrTableCell27 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell27");
            this.xrTableCell32 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell32");
            this.xrTableCell28 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell28");
            this.Detail1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail1");
            this.GroupHeader1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupHeaderBand>("GroupHeader1");
            this.GroupFooter1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupFooterBand>("GroupFooter1");
            this.xrTable3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable3");
            this.xrTableRow3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow3");
            this.xrTableCell11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell11");
            this.xrTableCell12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell12");
            this.xrTableCell13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell13");
            this.xrTableCell14 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell14");
            this.xrTableCell15 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell15");
            this.xrLabel3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel3");
            this.xrTable4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable4");
            this.xrTableRow4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow4");
            this.xrTableCell16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell16");
            this.xrTableCell17 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell17");
            this.xrTableCell18 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell18");
            this.xrTableCell19 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell19");
            this.xrTableCell20 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell20");
            this.xrLabel6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLabel>("xrLabel6");
            this.xrLine1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRLine>("xrLine1");
            this.xrPageInfo1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPageInfo>("xrPageInfo1");

            
            this.orderDates = reportInitializer.GetParameter("orderDates");
            this.orderDates_Start = ((DevExpress.XtraReports.Parameters.RangeStartParameter)(reportInitializer.GetParameter("orderDates_Start")));
            this.orderDates_End = ((DevExpress.XtraReports.Parameters.RangeEndParameter)(reportInitializer.GetParameter("orderDates_End")));

            
            this.sqlDataSource1 = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("sqlDataSource1");

            
            this.xrControlStyle1 = reportInitializer.GetStyle("xrControlStyle1");
        }
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell23;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell24;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell25;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell26;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell30;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell27;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell32;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRTable xrTable4;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.Parameters.Parameter orderDates;
        private DevExpress.XtraReports.Parameters.RangeStartParameter orderDates_Start;
        private DevExpress.XtraReports.Parameters.RangeEndParameter orderDates_End;
    }
}
