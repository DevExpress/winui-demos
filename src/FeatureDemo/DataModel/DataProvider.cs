using System;
using System.Collections.Generic;
using DevExpress.Mvvm.Native;

namespace FeatureDemo.DataModel {
    public enum DXReleaseVersion {
        V211
    }
    public class DemoModule {
        public static readonly DXReleaseVersion CurrentVersion = (DXReleaseVersion)Enum.Parse(typeof(DXReleaseVersion), "V" + AssemblyInfo.VersionId);
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
                new DemoModule("SchedulerDemoModule", 
                    "Scheduler Control", "The DevExpress WinUI Scheduler Control displays appointments across a variety of views – views used by many popular calendar and scheduling applications: from a single-day schedule to a month-wide view.") { 
                    Sources = new string[] { "ViewModels/SchedulingDemoViewModel.cs" } }
            });
            #endregion
            #region Ribbon
            var ribbonGroup = new DemoModuleGroup("Ribbon", "Ribbon", "M195.32,89.3c-19,0-41.72-6.4-67.32-25.6S79.65,38.1,60.69,38.1v16h0c18.47,0,37.89,7.54,57.71,22.4,25.48,19.11,51.36,28.8,76.92,28.8A99.74,99.74,0,0,0,240,94.68v90.7a72.8,72.8,0,0,1-9.39,6.63,68.32,68.32,0,0,1-35.29,9.89c-18.48,0-37.9-7.54-57.72-22.4-25.48-19.11-51.36-28.8-76.92-28.8A99.74,99.74,0,0,0,16,161.32V70.62C23,64.77,38.85,54.1,60.68,54.1v-16h0C22.76,38.1,0,63.7,0,63.7V192.3s22.75-25.6,60.68-25.6c19,0,41.72,6.4,67.32,25.6s48.35,25.6,67.32,25.6c37.92,0,60.68-25.6,60.68-25.6V63.7S233.25,89.3,195.32,89.3Z", new List<DemoModule>() {
                new DemoModule("RibbonToolbarModule",
                    "Ribbon Toolbar", "The DevExpress WinUI Ribbon Toolbar replicates key user experience elements of both the Mail App and Microsoft Office. It organizes commands into groups and pages, displays commands in a side pane next to page headers. It also supports an adaptive layout.") { 
                    Sources = new string[] { "RibbonSimplePadViewModel.cs", "RibbonDemoCommonStyles.xaml", "DemoRichEditor/DemoRichEditControl.cs", "DemoRichEditor/DemoRichEditInterfaces.cs", "DemoRichEditor/DemoRichEditorBehavior.cs", "ViewModels/FontStyleGroupViewModel.cs", "ViewModels/InsertGroupViewModel.cs", "ViewModels/ParagraphStyleGroupViewModel.cs" } },
            });
            #endregion
            #region Utility Controls
            var controlsGroup = new DemoModuleGroup("Controls", "Utility Controls", "M184,16a56.06,56.06,0,0,1,15.27,2.1L172.69,44.69a27.31,27.31,0,1,0,38.62,38.62L237.9,56.73a56,56,0,0,1-70.15,68.87l-9.18-2.79-6.79,6.78-22.19,22.19-6.78,6.79,2.79,9.18A56.06,56.06,0,0,1,56.73,237.9l26.58-26.59a27.31,27.31,0,1,0-38.62-38.62L18.1,199.27A56,56,0,0,1,88.25,130.4l9.18,2.79,6.79-6.78,22.19-22.19,6.78-6.79-2.79-9.18A56,56,0,0,1,184,16m0-16a72,72,0,0,0-68.9,92.9L92.9,115.1A72,72,0,0,0,13.73,226.27L56,184a11.31,11.31,0,0,1,16,16L29.73,242.27A72,72,0,0,0,140.9,163.1l22.2-22.2A72,72,0,0,0,242.27,29.73L200,72a11.31,11.31,0,0,1-16-16l42.27-42.27A71.63,71.63,0,0,0,184,0Z", new List<DemoModule>() {
                new DemoModule("BarCodeEmployees", 
                    "QR Code Business Card", "This example demonstrates a business card with an embedded DevExpress WinUI Barcode.") { 
                    Sources = new string[] { "ControlsDemo/Data/EmployeViewModel.cs" } },
                new DemoModule("BarCodeSample2D", 
                    "BarCode 2D", "The DevExpress WinUI Barcode can display QRCode, PDF417, DataMatrix, and DataMatrixGS1 two-dimensional barcodes. You can select a barcode type and customize display options via the side panel of this demo."),
                new DemoModule("BarCodeSample", 
                    "Linear BarCode", "The DevExpress WinUI Barcode can use one of 20 supported symbologies to visualize data as a one-dimensional barcode. You can review supported symbologies and configure them via the side panel of this demo."),
                new DemoModule("RangeControlModule", 
                    "Range Control Clients", "The DevExpress WinUI Range Control visualizes data ranges and allows end-users to specify a desired range. The Range Control supports five types of Clients that use different visualizations for a data range. You can switch between supported Range Control Clients via the side panel of this demo."),
                new DemoModule("AggregationModule", 
                    "Data Aggregation", "You can use the DevExpress WinUI Range Control to filter data before displaying it in another data-bound control. In this demo, the Range Control allows end-users to select a specific date range and display aggregated data for this range in a chart.")
            });
            #endregion
            #region Charts
            var chartsGroup = new DemoModuleGroup("Charts", "Charts", "M 160 0 L 160 64 L 80 64 L 80 128 L 0 128 L 0 256 L 256 256 L 256 0 L 160 0 z M 176 16 L 240 16 L 240 240 L 176 240 L 176 16 z M 96 80 L 160 80 L 160 240 L 96 240 L 96 80 z M 16 144 L 80 144 L 80 240 L 16 240 L 16 144 z", new List<DemoModule>() {
                new DemoModule("DashboardDemoModule", 
                    "Dashboard", "In this demo, we use the DevExpress WinUI Pie and Cartesian Chart controls to create a Population dashboard. Select a Donut series slice to display statistics for a specific category."),
                new DemoModule("BigAmountOfDataModule", 
                    "Large Data Source", "The DevExpress WinUI Cartesian Chart can visualize large data sets without freezing. Zoom in the chart or use the Range Control at the bottom to change the range for on-screen data."),
                new DemoModule("RealTimeDataModule", 
                    "Real-Time Data", "This demo showcases how efficiently the DevExpress WinUI Cartesian Chart can handle frequent updates from the underlying data source."),
                new DemoModule("PieDonutSeriesViewModule", 
                    "Pie and Donut", "The DevExpress WinUI Pie Chart can use Pie, Donut, and Nested Donut series views to display data. It also allows you to format text displayed in its series labels and tooltips."),
                new DemoModule("BarSeriesViewModule", 
                    "Bar and Column", "The DevExpress WinUI Cartesian Chart ships with multiple types of Bar and Column series views. You can select a different series view type via the side panel of this demo."),
                new DemoModule("AreaSeriesViewModule", 
                    "Area", "The DevExpress WinUI Cartesian Chart supports the Area, Stacked Area, Full Stacked Area, and Step Area series views. You can select a different series view type via the side panel of this demo."),
                new DemoModule("LineSeriesViewModule", 
                    "Line", "The DevExpress WinUI Cartesian Chart ships with Line, Step Line, Stacked Line, and Full Stacked Line series views. You can select a different series view type via the side panel of this demo."),
                new DemoModule("ScatterLineSeriesViewModule", 
                    "Scatter Line", "You can use Scatter Lines in the DevExpress WinUI Cartesian Chart to plot math function graphs. Choose a function type or configure point markers via the side panel of this demo."),
                new DemoModule("PointSeriesViewModule", 
                    "Point", "This example demonstrates how to plot a point chart and customize its point markers and labels."),
                new DemoModule("FunnelSeriesViewModule", 
                    "Funnel", "The DevExpress WinUI Funnel Chart can visualize a linear process with multiple connected stages. In this demo, the Funnel Chart displays a basic website conversion funnel with five stages from website visitors to returning customers.") }
            );
            #endregion
            #region Gauges
            var gaugesGroup = new DemoModuleGroup("Gauges", "Gauges", "M256,128a128,128,0,0,1-65.83,111.9l-7.76-14A112.58,112.58,0,0,0,207.2,207.2,112,112,0,0,0,78.43,27.51L66.59,15.67A128,128,0,0,1,256,128ZM16,128A111.49,111.49,0,0,1,27.51,78.43L15.67,66.59A128.06,128.06,0,0,0,65.83,239.9l7.76-14A112.58,112.58,0,0,1,48.8,207.2,111.29,111.29,0,0,1,16,128Zm160,0a48,48,0,1,1-87.12-27.8L26.34,37.66A8,8,0,0,1,37.66,26.34L100.2,88.88A48,48,0,0,1,176,128Zm-16,0a32,32,0,1,0-32,32A32,32,0,0,0,160,128Z", new List<DemoModule>() {
                new DemoModule("HouseClimateDashboardModule", 
                    "House Climate Dashboard", "This example demonstrates a house climate dashboard created with DevExpress WinUI Linear and Circular Gauges. The dashboard tracks house temperature and visualizes real-time energy consumption."),
                new DemoModule("CircularGaugeModule", 
                    "Circular Gauge", "The DevExpress WinUI Circular Gauge visualizes data on a circular scale and ships with extensive appearance customization options. In this demo, three Circular Gauges show the current time in New York, London, and Moscow."),
                new DemoModule("LinearGaugeModule", 
                    "Linear Gauge", "The DevExpress WinUI Linear Gauge visualizes data on a linear scale. Just like our Circular Gauge, it features extensive appearance customization options and can be displayed horizontally or vertically.")
            });
            #endregion
            #region Grid
            var gridGroup = new DemoModuleGroup("Grid", "Grid", "M 12 0 C 5.352 0 0 5.352 0 12 L 0 244 C 0 250.648 5.352 256 12 256 L 244 256 C 250.648 256 256 250.648 256 244 L 256 12 C 256 5.352 250.648 0 244 0 L 12 0 z M 16 16 L 80 16 L 80 80 L 16 80 L 16 16 z M 96 16 L 160 16 L 160 80 L 96 80 L 96 16 z M 176 16 L 240 16 L 240 80 L 176 80 L 176 16 z M 16 96 L 80 96 L 80 160 L 16 160 L 16 96 z M 96 96 L 160 96 L 160 160 L 96 160 L 96 96 z M 176 96 L 240 96 L 240 160 L 176 160 L 176 96 z M 16 176 L 80 176 L 80 240 L 16 240 L 16 176 z M 96 176 L 160 176 L 160 240 L 96 240 L 96 176 z M 176 176 L 240 176 L 240 240 L 176 240 L 176 176 z", new List<DemoModule>() {
                new DemoModule(nameof(GridDemo.PersonalTasksModule),
                    "Personal Task Management", "The DevExpress WinUI Data Grid includes a comprehensive suite of data shaping and UI customization options. Use our fully integrated cell editors or define cell templates as your needs dictate. Our Data Grid allows you to apply conditional formatting, organize data across groups, apply filters, sort data, and search for information using keywords.") { 
                    Sources = new [] { "GridDemo/Data/PersonalTasksViewModel.cs", "GridDemo/Themes/PersonalTasksModuleResources.xaml" } },
                new DemoModule("InplaceEditing", 
                    "Cell Editors", "The DevExpress WinUI Data Grid features a set of built-in columns for popular data editing scenarios and a Template column that supports cell templates defined in XAML.") { 
                    Sources = new [] { "GridDemo/Themes/InplaceEditingStyles.xaml", "GridDemo/Themes/CustomControlStyles.xaml" } },
                new DemoModule("GridSearchPanelModule", 
                    "Search Panel", "The Search Panel allows users to enter a search string and initiate a search operation against all values displayed in the grid. In addition to basic search strings, it supports powerful search expressions, and allows you to look for an exact match or exclude rows that match specific keywords.") {
                    Sources = new [] { "GridDemo/Data/GridSearchPanelViewModel.cs" } },
                new DemoModule("FilteringModule", 
                    "Data Filtering", "The DevExpress WinUI Data Grid includes numerous filtering options so you can deliver a highly flexible user experience. In this demo, each column includes a filter popup UI optimized for each data type. The Automatic Filter Row below column headers offers an alternative to filter popups. Simply select a filter operator and enter a search value into a cell to initiate the filter operation.") { 
                    Sources = new [] { "GridDemo/Data/FilteringViewModel.cs", "GridDemo/Themes/FilteringStyles.xaml" } },
                new DemoModule("GroupingModule",
                    "Group Summaries and Totals", "The DevExpress WinUI Data Grid automatically computes data summaries. You can enable total and group row summaries (SUM, MIN, MAX, COUNT, AVG, and CUSTOM) without writing a single line of code.") {
                    Sources = new [] { "GridDemo/Controls/SalesByYearData.cs", "GridDemo/Themes/GroupingStyles.xaml" } },
                new DemoModule("ConditionalFormattingModule", 
                    "Conditional Formatting", "The DevExpress WinUI Data Grid allows you to highlight cells or rows based on specified criteria, without writing a single line of code.") {
                    Sources = new string[] { }},
                new DemoModule("CellTemplatesModule", 
                    "Custom Cell Templates", "In this demo, we use cell templates to display sparkline charts and data bars for every row and visualize sales data.") { 
                    Sources = new [] { "GridDemo/Themes/CustomControlStyles.xaml" } },
                new DemoModule("GridRowValidation", 
                    "Input Validation", "The DevExpress WinUI Data Grid allows you to apply validation rules to help improve data input accuracy. In this demo, we use input validation to display errors for negative Order ID and Freight values, as well as warnings for empty product names.") {
                    Sources = new string[] { "GridDemo/Data/ValidationViewModel.cs", }},
                new DemoModule("NewItemRowModule", 
                    "New Item Row", "The New Item Row is a service row displayed at the bottom or the top of the DevExpress WinUI Data Grid. Click the New Item Row to add and initialize a new data row. Move focus from the New Item Row to commit the row to the data source.") {
                    Sources = new [] { "GridDemo/Themes/InplaceEditingStyles.xaml", "GridDemo/Themes/CustomControlStyles.xaml" } },
                new DemoModule("MultiSelection", 
                    "Multi-Row Selection", "The DevExpress WinUI Data Grid ships with standard and Extended multi-row select options. In standard multi-row selection mode, SHIFT is used to select a range of rows and CTRL adds individual records to the highlighted list. In Extended mode, each mouse click or Space bar press selects or deselects an individual row.") { 
                    Sources = new string[] { "Data/MultiSelectionViewModel.cs" }},
                new DemoModule("CellSelection", 
                    "Multi-Cell Selection", "Much like row selection, cell selection in the DevExpress WinUI Data Grid ships with two multi-select options. In standard multi-cell selection mode, SHIFT is used to select a block of cells and CTRL adds individual cells to the highlighted list. In Extended mode, each mouse click or Space bar press selects or deselects an individual cell.") { 
                    Sources = new string[] { }},
                new DemoModule("RowAlternationDemo",
                    "Alternating Row Highlighting", "The DevExpress WinUI Data Grid allows you to apply alternate row colors to help improve data visibility and readability.") {
                    Sources = new string[] { }},
                
            });
            #endregion
            #region Editors
            var editorsGroup = new DemoModuleGroup("Editors", "Editors", "M256,192V64H96V40a8,8,0,0,1,8-8h32a8,8,0,0,0,8-8.53A8.18,8.18,0,0,0,135.73,16H96a8,8,0,0,0-8,8,8,8,0,0,0-8-8H40.27A8.18,8.18,0,0,0,32,23.47,8,8,0,0,0,40,32H72a8,8,0,0,1,8,8V64H0V192H80v24a8,8,0,0,1-8,8H40a8,8,0,0,0-8,8.53A8.18,8.18,0,0,0,40.27,240H80a8,8,0,0,0,8-8,8,8,0,0,0,8,8h39.73a8.18,8.18,0,0,0,8.25-7.47,8,8,0,0,0-8-8.53H104a8,8,0,0,1-8-8V192ZM240,80v96H96V80ZM16,176V80H80v96Z", new List<DemoModule>() {
                new DemoModule("ContactDetailsModule", 
                    "Contact Details", "This example demonstrates an edit form created with DevExpress WinUI Data Editors.") {
                    Sources = new string[] { "Data/ContactDetailsViewModel.cs" } },
                new DemoModule("TextEditModule",
                    "Text Editor", "The DevExpress WinUI Text Editor is a single-line or multi-line plain text field. It can display null text and supports read-only states, masked input, and command buttons.") {
                    Sources = new string[] { } },
                new DemoModule("DateEditModule",
                    "Date-Time Editor", "The DevExpress WinUI Date-Time Editor allows end-users to edit date/time values using a dropdown calendar. It also supports masked input and can display null text when the edit box is empty.") {
                    Sources = new string[] { } },
                new DemoModule("ButtonEditModule",
                    "Command Buttons", "DevExpress WinUI editors can display an unlimited number of command buttons within their edit box. Buttons ship with a set of built-in glyphs and support custom font icons and raster images.") {
                    Sources = new string[] { "Data/ButtonEditViewModel.cs" } },
                new DemoModule("SpinEditModule",
                    "Spin Editor", "The DevExpress WinUI Spin Editor allows end-users to edit numeric values using increment/decrement buttons. It supports masked input and can limit the range of accepted values to help improve data input accuracy.") {
                    Sources = new string[] { "Data/ButtonEditViewModel.cs" } },
                new DemoModule("MasksModule",
                    "Masked Input", "You can use masks to restrict data input and format editor values. This tutorial demonstrates mask types available in DevExpress WinUI Data Editors and allows you to customize various mask options."){
                    Sources = new string[] { "NumericMaskModule.xaml", "NumericMaskModule.xaml.cs", "DateTimeMaskModule.xaml", "DateTimeMaskModule.xaml.cs", "TimeSpanMaskModule.xaml", "TimeSpanMaskModule.xaml.cs", "RegExMaskModule.xaml", "RegExMaskModule.xaml.cs", "SimpleMaskModule.xaml", "SimpleMaskModule.xaml.cs", "RegularMaskModule.xaml", "RegularMaskModule.xaml.cs" } },
                new DemoModule("InputValidationModule", 
                    "Input Validation", "DevExpress WinUI Data Editors allow you to apply validation rules to help improve data input accuracy. In this demo, we use event-based validation to display three types of errors: Information, Warning, and Critical.") {
                    Sources = new string[] { "Data/InputValidationViewModel.cs" } },
                new DemoModule("DateNavigatorModule",
                    "Date Navigator", "The DevExpress WinUI Date Navigator allows end-users to choose a date or date range from its calendar. The Date Navigator has a customizable UI and can adapt its layout based on currently applied culture settings.") {
                    Sources = new string[] { } },
                
            });
            #endregion
            DemoModuleGroups.AddRange(new DemoModuleGroup[] { gridGroup, schedulerGroup, editorsGroup, ribbonGroup, chartsGroup, gaugesGroup, controlsGroup });
        }
        private void InitializeProductGroups() {
            var charts = new ProductGroup("Charting", "DashboardDemoModule", "A fast and lightweight chart component with a broad range of data presentation types.");
            var grid = new ProductGroup("Data Grid", nameof(GridDemo.PersonalTasksModule), "A fast and responsive data grid component with unmatched data shaping capabilities.");
            var scheduler= new ProductGroup("Calendar and Scheduling", "SchedulerDemoModule", "A full-featured scheduling solution that easily displays a detailed snapshot of events/appointments in your application across a single day or a week.");
            var gauges = new ProductGroup("Gauges and Indicators", "HouseClimateDashboardModule", "An easy-to-use component with interactive linear and circular gauge indicators.");
            var toolbars = new ProductGroup("Ribbon", "RibbonToolbarModule", "A suite with a lightweight ribbon toolbar control - Ribbon Toolbar, that help you create app forms.");
            var editors = new ProductGroup("Data Editors", "ContactDetailsModule", "An extensive suite of data editors to be used standalone or within container components.");

            ProductGroups.Add(grid);
            ProductGroups.Add(scheduler);
            ProductGroups.Add(editors);
            ProductGroups.Add(toolbars);
            ProductGroups.Add(charts);
            ProductGroups.Add(gauges);
        }
    }
}

