using System;
using System.Collections.Generic;
using ChartsDemo;
using EditorsDemo;
using FeatureDemo.ControlsDemo;
using GaugesDemo;
using GridDemo;
using RangeControlDemo;
using ReportsDemo;
using RibbonDemo;
using SchedulerDemo;

namespace FeatureDemo.DataModel {
    public enum DXReleaseVersion {
        V211
    }
    public class DemoModule {
        public static readonly DXReleaseVersion CurrentVersion = (DXReleaseVersion)Enum.Parse(typeof(DXReleaseVersion), "V" + DevExpress.WinUI.Core.Internal.AssemblyInfo.VersionId);
        public IEnumerable<string> Sources { get; set; }
        public string ViewTypeName { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
        public bool IsHighlighted { get; set; }
        public bool IsNew => CurrentVersion == IntroducedIn;
        public bool IsUpdated => CurrentVersion == UpdatedIn;
        public DXReleaseVersion IntroducedIn { get; set; } = DXReleaseVersion.V211;
        public DXReleaseVersion UpdatedIn { get; set; } = DXReleaseVersion.V211;
        public DemoModuleGroup Group { get; set; }
        public string FullImagePath { get { return "ms-appx:///Images/Modules/" + ViewTypeName + ".png"; } }
        public DemoModule(string viewTypeName, string title, string about, string description = null) {
            ViewTypeName = viewTypeName;
            Title = title;
            About = about;
            Description = description ?? about;
        }
    }

    public class DemoModuleGroup {
        public string Name { get; private set; }
        public string Title { get; private set; }
        public string Icon { get; private set; }
        public List<DemoModule> DemoModules { get; private set; }
        public DemoModuleGroup(string name, string title, string icon, List<DemoModule> demoModules) {
            Name = name;
            Title = title;
            Icon = icon;
            DemoModules = demoModules;
            DemoModules.ForEach(x => x.Group = this);
        }
    }
    public class ProductGroup {
        public string Name { get; }
        public string Description { get;  }
        public string ViewTypeName { get; }
        public ProductGroup(string name, string viewTypeName, string description) {
            Name = name;
            Description = description;
            ViewTypeName = viewTypeName;
        }
    }
    public class DataModel {
        private static DataModel current;
        public static DataModel Current { get { return current ?? (current = new DataModel()); } }

        public List<DemoModuleGroup> DemoModuleGroups { get; } = new List<DemoModuleGroup>();
        public List<ProductGroup> ProductGroups { get; } = new List<ProductGroup>();
        
        public DataModel() {
            InitializeDataForDesktop();
        }
        private void InitializeDataForDesktop()
        {
            InitializeDemoModuleGroups();
            InitializeProductGroups();
        }
        private void InitializeDemoModuleGroups() {
            #region Scheduler
            var schedulerGroup = new DemoModuleGroup("Scheduler", "Scheduler", "M128,0A128,128,0,1,0,256,128,128,128,0,0,0,128,0Zm0,240A112,112,0,0,1,48.8,48.8,112,112,0,0,1,207.2,207.2,111.29,111.29,0,0,1,128,240ZM120,80a8,8,0,0,0-8,8v48a8,8,0,0,0,8,8h48a8,8,0,0,0,0-16H128V88a8,8,0,0,0-8-8Z", new List<DemoModule>() {
                new DemoModule("SchedulerDemoModule", "Scheduler Control", "This demo shows our WinUI Scheduler Control that can display appointments in a variety of views inspired by popular calendar and scheduling applications: from a single-day schedule to a month-wide view. Use the demo to create new appointments, or select existing appointments and edit them. When editing, explore available appointment recurrence patterns.") {
                    IsHighlighted = true, IntroducedIn=DXReleaseVersion.V211, UpdatedIn = DXReleaseVersion.V211, Sources = new string[] { "ViewModels/SchedulingDemoViewModel.cs" } }
            });
            #endregion
            #region Ribbon
            var ribbonGroup = new DemoModuleGroup("Ribbon", "Ribbon", "M195.32,89.3c-19,0-41.72-6.4-67.32-25.6S79.65,38.1,60.69,38.1v16h0c18.47,0,37.89,7.54,57.71,22.4,25.48,19.11,51.36,28.8,76.92,28.8A99.74,99.74,0,0,0,240,94.68v90.7a72.8,72.8,0,0,1-9.39,6.63,68.32,68.32,0,0,1-35.29,9.89c-18.48,0-37.9-7.54-57.72-22.4-25.48-19.11-51.36-28.8-76.92-28.8A99.74,99.74,0,0,0,16,161.32V70.62C23,64.77,38.85,54.1,60.68,54.1v-16h0C22.76,38.1,0,63.7,0,63.7V192.3s22.75-25.6,60.68-25.6c19,0,41.72,6.4,67.32,25.6s48.35,25.6,67.32,25.6c37.92,0,60.68-25.6,60.68-25.6V63.7S233.25,89.3,195.32,89.3Z", new List<DemoModule>() {
                new DemoModule("RibbonToolbarModule",
                               "Ribbon Toolbar",
                               "This demo shows the WinUI Ribbon Toolbar. A lightweight version of the feature-rich Ribbon Control, our Ribbon Toolbar includes ribbon tabs, groups, and custom contextual menu items. With Ribbon Toolbar, you can create compact menus for controls.")
                    { IsHighlighted = true, Sources = new string[] {
                        "RibbonSimplePadViewModel.cs",
                        "RibbonDemoCommonStyles.xaml",
                        "DemoRichEditor/DemoRichEditControl.cs",
                        "DemoRichEditor/DemoRichEditInterfaces.cs",
                        "DemoRichEditor/DemoRichEditorBehavior.cs",
                        "ViewModels/FontStyleGroupViewModel.cs",
                        "ViewModels/InsertGroupViewModel.cs",
                        "ViewModels/ParagraphStyleGroupViewModel.cs" } },
                
            });
            #endregion
            #region Utility Controls
            var controlsGroup = new DemoModuleGroup("Controls", "Utility Controls", "M184,16a56.06,56.06,0,0,1,15.27,2.1L172.69,44.69a27.31,27.31,0,1,0,38.62,38.62L237.9,56.73a56,56,0,0,1-70.15,68.87l-9.18-2.79-6.79,6.78-22.19,22.19-6.78,6.79,2.79,9.18A56.06,56.06,0,0,1,56.73,237.9l26.58-26.59a27.31,27.31,0,1,0-38.62-38.62L18.1,199.27A56,56,0,0,1,88.25,130.4l9.18,2.79,6.79-6.78,22.19-22.19,6.78-6.79-2.79-9.18A56,56,0,0,1,184,16m0-16a72,72,0,0,0-68.9,92.9L92.9,115.1A72,72,0,0,0,13.73,226.27L56,184a11.31,11.31,0,0,1,16,16L29.73,242.27A72,72,0,0,0,140.9,163.1l22.2-22.2A72,72,0,0,0,242.27,29.73L200,72a11.31,11.31,0,0,1-16-16l42.27-42.27A71.63,71.63,0,0,0,184,0Z", new List<DemoModule>() {
                new DemoModule(nameof(BarCodeEmployees), "QR Code Business Card", "This demo uses WinUI Bar Code to generate QR codes within business cards. Each QR code contains the corresponding contact’s personal information.")
                    { Sources = new string[] { "ControlsDemo/Data/EmployeViewModel.cs" } },
                new DemoModule(nameof(BarCodeSample2D), "BarCode 2D", "This demo displays four types of two-dimensional barcodes created with WinUI Bar Code. You can set barcode values and customize related display options."),
                new DemoModule(nameof(BarCodeSample), "Linear BarCode", "This demo shows one-dimensional barcodes created with WinUI Bar Code. You can switch between individual barcode types, set barcode values, and change corresponding display options."),
                new DemoModule(nameof(RangeControlModule), "Range Control Clients", "This demo shows available controls that can be linked to WinUI Range Control.")
                    { IsHighlighted = true },
                new DemoModule(nameof(AggregationModule), "Data Aggregation", "This demo shows how to use WinUI Range Control's Data Aggregation for statistical analysis.")
                    { IsHighlighted = true }
            });
            #endregion
            #region Charts
            var chartsGroup = new DemoModuleGroup("Charts", "Charts", "M 160 0 L 160 64 L 80 64 L 80 128 L 0 128 L 0 256 L 256 256 L 256 0 L 160 0 z M 176 16 L 240 16 L 240 240 L 176 240 L 176 16 z M 96 80 L 160 80 L 160 240 L 96 240 L 96 80 z M 16 144 L 80 144 L 80 240 L 16 240 L 16 144 z", new List<DemoModule>() {
                new DemoModule(nameof(DashboardModule), "Dashboard", "This demo uses the MVVM pattern to create a Population dashboard that includes our Pie and Cartesian Charts.")
                    { IsHighlighted = true },
                new DemoModule(nameof(LargeDataSourceModule), "Large Data Source", "The WinUI Cartesian Chart can display multiple series with a large number of points.")
                    { IsHighlighted = true },
                new DemoModule(nameof(RealTimeDataModule), "Real-Time Data", "This demo shows how the WinUI Chart Control visualizes data from a frequently-updated data source in real time."),
                new DemoModule(nameof(PieSeriesModule), "Pie and Donut", "This demo shows series views that you can plot on the WinUI Pie Chart."),
                new DemoModule(nameof(BarSeriesModule), "Bar and Column", "This demo shows Bar series views available in the WinUI Cartesian Chart."),
                new DemoModule(nameof(AreaSeriesModule), "Area", "This demo shows Area series views available in the WinUI Cartesian Chart."),
                new DemoModule(nameof(LineSeriesModule), "Line", "This demo shows Line series views available in the WinUI Cartesian Chart."),
                new DemoModule(nameof(ScatterLineSeriesModule), "Scatter Line", "This demo shows the Scatter Line series view."),
                new DemoModule(nameof(PointSeriesModule), "Point", "This demo shows the Point series view."),
                new DemoModule(nameof(FunnelSeriesModule), "Funnel", "This demo shows a WinUI Funnel Chart.") }
            );
            #endregion
            #region Gauges
            var gaugesGroup = new DemoModuleGroup("Gauges", "Gauges", "M256,128a128,128,0,0,1-65.83,111.9l-7.76-14A112.58,112.58,0,0,0,207.2,207.2,112,112,0,0,0,78.43,27.51L66.59,15.67A128,128,0,0,1,256,128ZM16,128A111.49,111.49,0,0,1,27.51,78.43L15.67,66.59A128.06,128.06,0,0,0,65.83,239.9l7.76-14A112.58,112.58,0,0,1,48.8,207.2,111.29,111.29,0,0,1,16,128Zm160,0a48,48,0,1,1-87.12-27.8L26.34,37.66A8,8,0,0,1,37.66,26.34L100.2,88.88A48,48,0,0,1,176,128Zm-16,0a32,32,0,1,0-32,32A32,32,0,0,0,160,128Z", new List<DemoModule>() {
                new DemoModule(nameof(HouseClimateModule), "House Climate Dashboard", "In this demo, the WinUI Gauges are used to create a house climate dashboard. The dashboard tracks house temperature, which must be kept at a specified level, and also visualizes real-time energy consumption.")
                    { IsHighlighted = true },
                new DemoModule(nameof(CircularGaugeModule), "Circular Gauge", "In this demo, the WinUI Circular Gauges show the current time in different time zones."),
                new DemoModule(nameof(LinearGaugeModule), "Linear Gauge", "This demo uses level bars and markers in the WinUI Linear Gauge to indicate website visitor trends.")
            });
            #endregion
            #region Grid
            var gridGroup = new DemoModuleGroup("Grid", "Grid", "M 12 0 C 5.352 0 0 5.352 0 12 L 0 244 C 0 250.648 5.352 256 12 256 L 244 256 C 250.648 256 256 250.648 256 244 L 256 12 C 256 5.352 250.648 0 244 0 L 12 0 z M 16 16 L 80 16 L 80 80 L 16 80 L 16 16 z M 96 16 L 160 16 L 160 80 L 96 80 L 96 16 z M 176 16 L 240 16 L 240 80 L 176 80 L 176 16 z M 16 96 L 80 96 L 80 160 L 16 160 L 16 96 z M 96 96 L 160 96 L 160 160 L 96 160 L 96 96 z M 176 96 L 240 96 L 240 160 L 176 160 L 176 96 z M 16 176 L 80 176 L 80 240 L 16 240 L 16 176 z M 96 176 L 160 176 L 160 240 L 96 240 L 96 176 z M 176 176 L 240 176 L 240 240 L 176 240 L 176 176 z", new List<DemoModule>() {
                new DemoModule(nameof(PersonalTasksModule), "Personal Tasks", "This demo shows a WinUI Data Grid that allows you to display, manage, and manipulate tabular data.") 
                    { Sources = new string[] { "Data/PersonalTasksViewModel.cs" } },
                new DemoModule(nameof(GridSearchPanelModule), "Grid Search Panel", "This module demonstrates the built-in Search Panel which provides end-users with an easy way to locate information within the grid.")
                    { Sources = new string[] { "Data/GridSearchPanelViewModel.cs" }},
                new DemoModule(nameof(DataFilteringModule), "Data Filtering", "The WinUI Data Grid allows users to filter its data. Users can select filter values from the column’s Drop-down Filter or type text in the Automatic Filter Row to apply a filter condition.")
                    { Sources = new string[] { "Data/DataFilteringViewModel.cs" }},
                 new DemoModule(nameof(PrintingModule), "Print Options", "The DevExpress WinUI Data Grid allows you to render its content to paper (or export rendered data to DOC, XLS, PDF, HTML and more)."),
                new DemoModule(nameof(GroupingModule), "Grouping and Summary", "This demo shows how to group data and align group summaries for corresponding columns.")
                    { Sources = new string[] { "Data/SalesByYearData.cs" }},
                new DemoModule(nameof(ConditionalFormattingModule), "Conditional Formatting", "The WinUI Data Grid allows you to apply conditional formatting and change the appearance of individual cells and rows based on specific conditions.")
                    { Sources = new string[] { }},
                new DemoModule(nameof(CellTemplatesModule), "Custom Cell Templates", "This following demo shows how to use cell templates to customize the appearance of individual cells in the WinUI Data Grid.")
                    { Sources = new string[] { "Data/SalesDataViewModel.cs" }},

                new DemoModule(nameof(InputValidation), "Input Validation", "This demo shows how to validate data obtained from the WinUI Data Grid’s data source or that which is entered by users.")
                    { Sources = new string[] { "Data/InputValidationViewModel.cs" }},
                new DemoModule(nameof(NewItemRowModule), "New Item Row", "The New Item Row is a service row displayed at the bottom or the top of the WinUI Data Grid. Click the New Item Row to add and specify a new data row. Move focus from the New Item Row to commit the row to the data source.")
                    { Sources = new string[] { "Data/NewItemRowViewModel.cs" }},
                new DemoModule(nameof(MultiRowSelection), "Multi-Row Selection", "The WinUI Data Grid allows you to enable multiple row selection mode. In it, users can select more than one row at a time. This demo shows how to calculate summaries for selected rows.")
                    { Sources = new string[] { "Data/MultiSelectionViewModel.cs" }},
                new DemoModule(nameof(MultiCellSelection), "Multi-Cell Selection", "The WinUI Data Grid allows you to enable multiple cell selection mode. In it, users can select blocks of cells and individual cells within different data rows.")
                    { Sources = new string[] { "Data/SalesByYearData.cs" }},
                new DemoModule(nameof(RowAlternationDemo), "Row Alternation", "The following demo highlights alternate rows to enhance the readability of WinUI Data Grid data. ")
                    { Sources = new string[] { "Data/MultiSelectionViewModel.cs" }},
                new DemoModule("VirtualSourcesModule", "Virtual Data Sources", "This module shows how to use a GridControl with an infinite virtual data source.")
                    { Sources = new string[] { "GridDemo/Data/VirtualSourcesViewModel.cs", "GridDemo/Data/IssuesData.cs" } },
            });
            #endregion
            #region Editors
            var editorsGroup = new DemoModuleGroup("Editors", "Editors", "M256,192V64H96V40a8,8,0,0,1,8-8h32a8,8,0,0,0,8-8.53A8.18,8.18,0,0,0,135.73,16H96a8,8,0,0,0-8,8,8,8,0,0,0-8-8H40.27A8.18,8.18,0,0,0,32,23.47,8,8,0,0,0,40,32H72a8,8,0,0,1,8,8V64H0V192H80v24a8,8,0,0,1-8,8H40a8,8,0,0,0-8,8.53A8.18,8.18,0,0,0,40.27,240H80a8,8,0,0,0,8-8,8,8,0,0,0,8,8h39.73a8.18,8.18,0,0,0,8.25-7.47,8,8,0,0,0-8-8.53H104a8,8,0,0,1-8-8V192ZM240,80v96H96V80ZM16,176V80H80v96Z", new List<DemoModule>() {
                new DemoModule("ContactDetailsModule", "Contact Details", "This demo shows a Contact Detail form that you can create with our WinUI Editors."),
                new DemoModule("TextEditModule", "Text Editor", "This demo shows features supported by a text editor."),
                new DemoModule("DateEditModule", "Date-Time Editor", "This demo shows features available in the WinUI Date-Time Editor."),
                new DemoModule("EditorButtonsModule", "Editor's Buttons", "This demo shows WinUI Editors with custom buttons."),
                new DemoModule("SpinEditModule", "Numeric Editor", "This demo shows features available in our Spin Edit, including value increment/decrement and value range limitation."),
                new DemoModule("MasksModule", "Mask", "Use this demo to review masked input features, including full Regular Expression support."){
                    Sources = new string[] { "NumericMaskModule.xaml", "NumericMaskModule.xaml.cs", "DateTimeMaskModule.xaml", "DateTimeMaskModule.xaml.cs", "TimeSpanMaskModule.xaml", "TimeSpanMaskModule.xaml.cs", "RegExMaskModule.xaml", "RegExMaskModule.xaml.cs", "SimpleMaskModule.xaml", "SimpleMaskModule.xaml.cs", "RegularMaskModule.xaml", "RegularMaskModule.xaml.cs" }
                },
                new DemoModule("InputValidationModule", "Input Validation", "This demo shows WinUI Editor validation capabilities."){
                    Sources = new string[] { "Data/InputValidationViewModel.cs" }
                }
            });
            #endregion
            #region Reports
            var reportsGroup = new DemoModuleGroup("Reporting", "Reporting", "F0 M20,20z M0,0z M13.9944,2.01258L12,0 0,0 0,20 16,20 16,15 15,15 15,19 1,19 1,1 11.5716,1 13.2913,2.72114 15,4.42888 15,6 16,6 16,4 13.9944,2.01258z M19.9954,9.52552L16.4599,5.98999 7.97461,14.4753 7.97461,18.0108 11.5101,18.0108 19.9954,9.52552z M18.5812,9.52552L16.4599,7.4042 9.03516,14.8289 11.1565,16.9503 18.5812,9.52552z M10.9621,8.15564C11.6825,7.65136 12,7.05062 12,6.5 12,5.94938 11.6825,5.34864 10.9621,4.84436 10.2448,4.34229 9.2007,4 8,4 6.7993,4 5.75517,4.34229 5.03793,4.84436 4.31753,5.34864 4,5.94938 4,6.5 4,7.05062 4.31753,7.65136 5.03793,8.15564 5.75517,8.65771 6.7993,9 8,9 9.2007,9 10.2448,8.65771 10.9621,8.15564z M8,10C10.7614,10 13,8.433 13,6.5 13,4.567 10.7614,3 8,3 5.23858,3 3,4.567 3,6.5 3,8.433 5.23858,10 8,10z M11,6.5C11,7.32843 9.65685,8 8,8 6.34315,8 5,7.32843 5,6.5 5,5.67157 6.34315,5 8,5L8,6.5 11,6.5z", new List<DemoModule>() {
                new DemoModule("MasterDetailReportModule", "Master-Detail Report", @"This demo illustrates the use of Detail Report bands when generating a master-detail report. The report displays the invoice data where the Detail Report lists the products ordered.")
                { Sources = new string[] { "Reports/MasterDetailReport.cs", "Reports/MasterDetailReport.Designer.cs", "Reports/MasterDetailReport.vsrepx" } },

                new DemoModule("BarCodeTypesReportModule", "BarCode", "This report demonstrates all bar code types the BarCode control supports and the AutoModule option the BarCode control exposes. Enable this option in the Parameters panel to automatically calculate the width of the narrowest bar or space based on the control's size.")
                { Sources = new string[] { "Reports/BarCodeTypesReport.cs", "Reports/BarCodeTypesReport.Designer.cs", "Reports/BarCodeTypesReport.vsrepx" } },

                new DemoModule("ChartsReportModule", "Chart", "You can use the DevExpress Reporting Chart control to add charts to your reports. In this demo, the Chart control displays sales data collected from different sources, and FederationDataSource is used to combine these sources into one data source. The chart's Data Member is set to the Categories.CategoriesProducts relation.")
                { Sources = new string[] { "Reports/ChartsReport.cs", "Reports/ChartsReport.Designer.cs", "Reports/ChartsReport.vsrepx" } },

                new DemoModule("ReportMergingWithPdfReportModule", "Report Merging with PDF", "In this invoice demo, a report document is merged with PDF content. The report includes an XRPdfContent control that adds a PDF specification document for each product ordered.")
                { Sources = new string[] { "Reports/ReportMergingWithPdfReport.cs", "Reports/ReportMergingWithPdfReport.Designer.cs", "Reports/ReportMergingWithPdfReport.vsrepx" } },

                new DemoModule("SalesSummaryReportModule", "Sales Summary", "This demo illustrates how to use the Cross Tab control to create a Sales Summary report. In this demo, the Cross Tab calculates automatic totals and grand totals across all row and column fields. The top left corner displays row field headers, and row field values are grouped by quarter.")
                { Sources = new string[] { "Reports/SalesSummaryReport.cs", "Reports/SalesSummaryReport.Designer.cs", "Reports/SalesSummaryReport.vsrepx" } },

                new DemoModule("TableReportModule", "Table Report", "This demo illustrates how to use the Table control to create a table report. This control allows you to draw a data-aware table and apply report styles (for example, to odd and even table rows).")
                { Sources = new string[] { "Reports/TableReport.cs", "Reports/TableReport.Designer.cs", "Reports/TableReport.vsrepx" } }
            });
            #endregion
            DemoModuleGroups.AddRange(new DemoModuleGroup[] { gridGroup, editorsGroup, reportsGroup, chartsGroup, schedulerGroup, ribbonGroup, gaugesGroup, controlsGroup });
        }
        private void InitializeProductGroups() {
            var charts = new ProductGroup("Charting", nameof(DashboardModule), "A fast and lightweight chart component with a broad range of data presentation types.");
            var grid = new ProductGroup("Data Grid", nameof(PersonalTasksModule), "A fast and responsive data grid component with unmatched data shaping capabilities.");
            var scheduler= new ProductGroup("Calendar and Scheduling", nameof(SchedulerDemoModule), "A full-featured scheduling solution that easily displays a detailed snapshot of events/appointments in your application across a single day or a week.");
            var gauges = new ProductGroup("Gauges and Indicators", nameof(HouseClimateModule), "An easy-to-use component with interactive linear and circular gauge indicators.");
            var toolbars = new ProductGroup("Ribbon", nameof(RibbonToolbarModule), "A suite with a lightweight ribbon toolbar control - Ribbon Toolbar, that help you create app forms.");
            var editors = new ProductGroup("Data Editors", nameof(ContactDetailsModule), "An extensive suite of data editors to be used standalone or within container components.");
            var reports = new ProductGroup("Reporting", nameof(MasterDetailReportModule), "A feature-complete reporting suite for WinUI applications. DevExpress Reports for WinUI ships with an intuitive Visual Studio Report Designer and a comprehensive suite of report elements such as tables, labels, objects, charts, pivot tables, etc.");

            ProductGroups.Add(grid);
            ProductGroups.Add(reports);
            ProductGroups.Add(editors);
            ProductGroups.Add(charts);
            ProductGroups.Add(scheduler);
            ProductGroups.Add(toolbars);
            ProductGroups.Add(gauges);
        }
    }
}

