using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Grid.Printing;
using DevExpress.WinUI.Printing;
using DevExpress.WinUI.Printing.Internal;
using DevExpress.XtraPrinting;
using FeatureDemo.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using static FeatureDemo.Data.VehiclesData;
using PrintingImageSource = DevExpress.XtraPrinting.Drawing.ImageSource;

namespace GridDemo {
    public sealed partial class PrintingModule : GridDemoUserControl, INotifyPropertyChanged {

        public PrintingModule() {
            InitializeComponent();
            Grid.Loaded += OnGridLoaded;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        IEnumerable<Model> data;
        public IEnumerable<Model> Data {
            get => data;
            private set {
                data = value;
                RaisePropetryChanged();
            }
        }
        bool isPreviewVisible = false;
        public bool IsPreviewVisible {
            get => isPreviewVisible;
            private set {
                isPreviewVisible = value;
                OnIsPreviewVisibleChanged();
            }
        }
        public bool IsGridVisible { get; private set; } = true;
        PrintableLink printDocument;
        public PrintableLink PrintDocument {
            get => printDocument;
            private set {
                if(printDocument != value) {
                    printDocument = value;
                    RaisePropetryChanged();
                }
            }
        }
        bool printColumnHeaders = true;
        public bool PrintColumnHeaders {
            get => printColumnHeaders;
            set {
                printColumnHeaders = value;
                OnPrintPropertyChanged();
            }
        }
        bool printGroupSummary = true;
        public bool PrintGroupSummary {
            get => printGroupSummary;
            set {
                printGroupSummary = value;
                OnPrintPropertyChanged();
            }
        }
        bool printTotalSummary = true;
        public bool PrintTotalSummary {
            get => printTotalSummary;
            set {
                printTotalSummary = value;
                OnPrintPropertyChanged();
            }
        }
        bool printFixedTotalSummary = true;
        public bool PrintFixedTotalSummary {
            get => printFixedTotalSummary;
            set {
                printFixedTotalSummary = value;
                OnPrintPropertyChanged();
            }
        }
        bool printAllGroupsExpanded = false;
        public bool PrintAllGroupsExpanded {
            get => printAllGroupsExpanded;
            set {
                printAllGroupsExpanded = value;
                OnPrintPropertyChanged();
            }
        }

        protected internal override GridControl Grid => grid;

        public void ShowPrintPreview() {
            UpdatePrintDocument();
            IsPreviewVisible = true;
        }

        public void UpdatePrintDocument() => PrintDocument = CreateLink();

        void OnPrintPropertyChanged([CallerMemberName]string name = null) {
            RaisePropetryChanged(name);
            UpdatePrintDocument();
        }
        void NavigateBack() => IsPreviewVisible = false;
        PrintableLink CreateLink() {
            var printer = new GridControlPrinter(Grid);
            return new PrintableLink(printer);
        }
        void OnIsPreviewVisibleChanged() {
            RaisePropetryChanged(nameof(IsPreviewVisible));
            IsGridVisible = !isPreviewVisible;
            RaisePropetryChanged(nameof(IsGridVisible));
        }
        async Task Initialize() => Data = await VehiclesData.GetModels();
        void RaisePropetryChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        async void OnGridLoaded(object sender, RoutedEventArgs e) {
            await Initialize();
            Grid.Loaded -= OnGridLoaded;
            ExpandGroups(5);
        }
        void ExpandGroups(int count) {
            for(int i = 1; i <= count; i++)
                Grid.ExpandGroupRow(-1 * i);
        }
    }
}
