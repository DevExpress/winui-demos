









namespace ReportsDemo {
    
    public partial class ReportMergingWithPdfReport : DevExpress.XtraReports.UI.XtraReport {
        private void InitializeComponent() {
            DevExpress.XtraReports.ReportInitializer reportInitializer = new DevExpress.XtraReports.ReportInitializer(this, "FeatureDemo.ReportsDemo.Reports.ReportMergingWithPdfReport.vsrepx");

            
            this.topMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.TopMarginBand>("topMarginBand1");
            this.detailBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("detailBand1");
            this.bottomMarginBand1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.BottomMarginBand>("bottomMarginBand1");
            this.OrdersDetailReport = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("OrdersDetailReport");
            this.DetailReport1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport1");
            this.pageInfo1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPageInfo>("pageInfo1");
            this.OrdersDetail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("OrdersDetail");
            this.ReportHeader1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.ReportHeaderBand>("ReportHeader1");
            this.GroupFooter2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupFooterBand>("GroupFooter2");
            this.GroupHeader2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.GroupHeaderBand>("GroupHeader2");
            this.DetailReport = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport");
            this.panel1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPanel>("panel1");
            this.xrTable1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable1");
            this.xrPictureBoxLogo = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPictureBox>("xrPictureBoxLogo");
            this.xrTable4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable4");
            this.xrTable3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable3");
            this.xrTableRow4 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow4");
            this.tableRow1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("tableRow1");
            this.xrTableRow5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow5");
            this.xrTableRow6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow6");
            this.xrTableCell2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell2");
            this.xrTableCell6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell6");
            this.tableCell1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell1");
            this.tableCell2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("tableCell2");
            this.xrTableCell7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell7");
            this.xrTableCell8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell8");
            this.xrTableCell9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell9");
            this.xrTableCell10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell10");
            this.xrTableRow1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow1");
            this.xrTableRow3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow3");
            this.xrTableCell1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell1");
            this.xrTableCell3 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell3");
            this.xrTableRow7 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow7");
            this.xrTableRow8 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow8");
            this.xrTableCell11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell11");
            this.xrTableCell12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell12");
            this.xrTableCell14 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell14");
            this.xrTableCell15 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell15");
            this.xrTableCell16 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell16");
            this.xrTableCell17 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell17");
            this.xrTableCell13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell13");
            this.xrTableCell18 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell18");
            this.xrTableCell19 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell19");
            this.xrTableCell20 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell20");
            this.xrTableCell21 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell21");
            this.xrTableCell22 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell22");
            this.xrPdfSignature1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPdfSignature>("xrPdfSignature1");
            this.xrTable6 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable6");
            this.xrTableRow10 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow10");
            this.xrTableRow11 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow11");
            this.xrTableRow12 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow12");
            this.xrTableCell28 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell28");
            this.xrTableCell29 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell29");
            this.xrTableCell30 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell30");
            this.xrTableCell31 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell31");
            this.xrTableCell32 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell32");
            this.xrTableCell33 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell33");
            this.xrTable5 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable5");
            this.xrTableRow9 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow9");
            this.xrTableCell23 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell23");
            this.xrTableCell24 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell24");
            this.xrTableCell26 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell26");
            this.xrTableCell27 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell27");
            this.xrTableCell25 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell25");
            this.Detail = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail");
            this.xrTable2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTable>("xrTable2");
            this.xrTableRow13 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableRow>("xrTableRow13");
            this.xrTableCell34 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell34");
            this.xrTableCell35 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell35");
            this.xrTableCell36 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell36");
            this.xrTableCell37 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell37");
            this.xrTableCell38 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRTableCell>("xrTableCell38");
            this.Detail1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail1");
            this.DetailReport2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailReportBand>("DetailReport2");
            this.Detail2 = reportInitializer.GetControl<DevExpress.XtraReports.UI.DetailBand>("Detail2");
            this.xrPdfContent1 = reportInitializer.GetControl<DevExpress.XtraReports.UI.XRPdfContent>("xrPdfContent1");

            
            this.paramShowHeader = reportInitializer.GetParameter("paramShowHeader");
            this.paramShowFooter = reportInitializer.GetParameter("paramShowFooter");
            this.paramShowStatus = reportInitializer.GetParameter("paramShowStatus");
            this.paramShowComments = reportInitializer.GetParameter("paramShowComments");
            this.parameterInvoice = reportInitializer.GetParameter("parameterInvoice");

            
            this.reportSqlDataSource = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("reportSqlDataSource");
            this.parameterSqlDataSource = reportInitializer.GetDataSource<DevExpress.DataAccess.Sql.SqlDataSource>("parameterSqlDataSource");

            
            this.HomeOffice_StateName = reportInitializer.GetCalculatedField("HomeOffice_StateName");
            this.Address_StateName = reportInitializer.GetCalculatedField("Address_StateName");

            
            this.HeaderStyle = reportInitializer.GetStyle("HeaderStyle");
            this.General = reportInitializer.GetStyle("General");
            this.Comments = reportInitializer.GetStyle("Comments");
            this.BillingShippingHeaderStyle = reportInitializer.GetStyle("BillingShippingHeaderStyle");
            this.TableHeaderStyle = reportInitializer.GetStyle("TableHeaderStyle");
            this.DetailTableHeaderStyle = reportInitializer.GetStyle("DetailTableHeaderStyle");
            this.TableOddStyle = reportInitializer.GetStyle("TableOddStyle");
        }
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand detailBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.DetailReportBand OrdersDetailReport;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        private DevExpress.XtraReports.UI.XRPageInfo pageInfo1;
        private DevExpress.XtraReports.UI.DetailBand OrdersDetail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.XRPanel panel1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBoxLogo;
        private DevExpress.XtraReports.UI.XRTable xrTable4;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableRow tableRow1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
        private DevExpress.XtraReports.UI.XRTableCell tableCell1;
        private DevExpress.XtraReports.UI.XRTableCell tableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow7;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow8;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell17;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell18;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell21;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell22;
        private DevExpress.XtraReports.UI.XRPdfSignature xrPdfSignature1;
        private DevExpress.XtraReports.UI.XRTable xrTable6;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow10;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow11;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow12;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell30;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell31;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell32;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
        private DevExpress.XtraReports.UI.XRTable xrTable5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow9;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell23;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell24;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell26;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell27;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell25;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell35;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell36;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell37;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell38;
        private DevExpress.DataAccess.Sql.SqlDataSource reportSqlDataSource;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport2;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private DevExpress.XtraReports.UI.XRPdfContent xrPdfContent1;
        private DevExpress.XtraReports.UI.XRControlStyle HeaderStyle;
        private DevExpress.XtraReports.UI.XRControlStyle General;
        private DevExpress.XtraReports.UI.XRControlStyle Comments;
        private DevExpress.XtraReports.UI.XRControlStyle BillingShippingHeaderStyle;
        private DevExpress.XtraReports.UI.XRControlStyle TableHeaderStyle;
        private DevExpress.XtraReports.UI.XRControlStyle DetailTableHeaderStyle;
        private DevExpress.XtraReports.UI.XRControlStyle TableOddStyle;
        private DevExpress.XtraReports.UI.CalculatedField HomeOffice_StateName;
        private DevExpress.XtraReports.UI.CalculatedField Address_StateName;
        private DevExpress.XtraReports.Parameters.Parameter paramShowHeader;
        private DevExpress.XtraReports.Parameters.Parameter paramShowFooter;
        private DevExpress.XtraReports.Parameters.Parameter paramShowStatus;
        private DevExpress.XtraReports.Parameters.Parameter paramShowComments;
        private DevExpress.XtraReports.Parameters.Parameter parameterInvoice;
        private DevExpress.DataAccess.Sql.SqlDataSource parameterSqlDataSource;
    }
}
