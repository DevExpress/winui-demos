using DevExpress.WinUI.Data;
using DevExpress.WinUI.Grid;
using Microsoft.UI.Xaml;
using System;

namespace GridDemo {
    public sealed partial class MultiSelection : GridDemoUserControl {
        decimal sum = 0;

        public MultiSelection() {
            InitializeComponent();
            grid.Loaded += OnLoaded;
            grid.Unloaded += OnUnloaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            grid.SelectionChanged += grid_UpdateSummary;
            grid.SelectedItemChanged += grid_UpdateSummary;
            grid.CustomSummary += grid_CustomSummary;
            Grid.SelectRange(2, 6);
            Grid.SelectItem(10);
        }

        void OnUnloaded(object sender, RoutedEventArgs e) {
            grid.SelectionChanged -= grid_UpdateSummary;
            grid.SelectedItemChanged -= grid_UpdateSummary;
            grid.CustomSummary -= grid_CustomSummary;
            grid.DataContext = null;
        }

        protected internal override GridControl Grid {
            get { return grid; }
        }

        void grid_CustomSummary(object sender, DevExpress.WinUI.Data.CustomSummaryEventArgs e) {
            switch(e.SummaryProcess) {
                case CustomSummaryProcess.Start:
                    this.sum = 0;
                    break;
                case CustomSummaryProcess.Calculate:
                    if(grid.SelectionMode != MultiSelectMode.None) {
                        if(Grid.IsItemSelected(e.RowHandle)) {
                            this.sum += (decimal)e.FieldValue;
                        }
                    }
                    else {
                        this.sum += (decimal)e.FieldValue;
                    }
                    break;
                case CustomSummaryProcess.Finalize:
                    e.TotalValue = this.sum;
                    break;
                default:
                    break;
            }
        }
        void grid_UpdateSummary(object sender, EventArgs e) {
            if(Grid == null) return;
            Grid.UpdateTotalSummary();
        }
    }
}