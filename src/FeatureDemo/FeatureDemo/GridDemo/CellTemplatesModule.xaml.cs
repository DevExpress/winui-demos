using DevExpress.WinUI.Grid;

namespace GridDemo {
    public sealed partial class CellTemplatesModule : GridDemoUserControl {
        public SalesDataViewModel ViewModel { get; } = new SalesDataViewModel();
        public CellTemplatesModule() {
            this.InitializeComponent();
        }
        protected internal override GridControl Grid { get => gridControl; }
    }
}