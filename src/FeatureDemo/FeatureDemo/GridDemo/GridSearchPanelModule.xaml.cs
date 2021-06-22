using DevExpress.WinUI.Grid;

namespace GridDemo {
    public sealed partial class GridSearchPanelModule : GridDemoUserControl {
        public GridSearchPanelViewModel ViewModel { get; } = new GridSearchPanelViewModel();
        public GridSearchPanelModule() => this.InitializeComponent();
        protected internal override GridControl Grid => grid;
    }
}
