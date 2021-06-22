using DevExpress.Data.Filtering;
using DevExpress.WinUI.Grid;
using System;

namespace GridDemo {
    public sealed partial class DataFilteringModule : GridDemoUserControl {
        public DataFilteringViewModel ViewModel { get; } = new DataFilteringViewModel();
        public DataFilteringModule() {
            this.InitializeComponent();
            grid.ExpandAllGroups();

            var orderDate = new OperandProperty("UnboundOrderDate");
            var unitPrice = new OperandProperty("UnitPrice");
            var today = DateTime.Today;
            var nextMonth = today.AddMonths(1);
            var filter = orderDate >= new DateTime(today.Year, today.Month, 1) & orderDate < new DateTime(nextMonth.Year, nextMonth.Month, 1);

            grid.FilterCriteria = filter;
        }
        protected internal override GridControl Grid => grid;
    }
}
