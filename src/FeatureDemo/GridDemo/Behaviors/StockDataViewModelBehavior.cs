using DevExpress.WinUI.Data;
using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Mvvm.UI.Interactivity;
using GridDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo {
    public sealed class StockDataViewModelBehavior : Behavior<GridControl> {
        StockDataViewModel Model => (StockDataViewModel)AssociatedObject.DataContext;

        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        void AssociatedObject_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e) {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            Model.BeginUpdate += Model_BeginUpdate;
            Model.EndUpdate += Model_EndUpdate;
            AssociatedObject.CustomSummary += AssociatedObject_CustomSummary;
            Model.Resume();
        }

        void AssociatedObject_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e) {
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
            Model.BeginUpdate -= Model_BeginUpdate;
            Model.EndUpdate -= Model_EndUpdate;
            AssociatedObject.CustomSummary -= AssociatedObject_CustomSummary;
            Model.Unload();
        }

        protected override void OnDetaching() {
            base.OnDetaching();
        }

        void Model_BeginUpdate(object sender, EventArgs e) => AssociatedObject.BeginDataUpdate();
        void Model_EndUpdate(object sender, EventArgs e) => AssociatedObject.EndDataUpdate();
        void AssociatedObject_CustomSummary(object sender, CustomSummaryEventArgs e) {
            if(e.SummaryProcess != CustomSummaryProcess.Finalize)
                return;
            e.TotalValue = new RealLiveDataCustomSummary() { Negative = Model.NegativePriceHistoryList.ToList(), Positive = Model.PositivePriceCountList.ToList() };
        }
    }
    public class RealLiveDataCustomSummary {
        public List<double> Positive { get; set; }
        public List<double> Negative { get; set; }
    }
}
