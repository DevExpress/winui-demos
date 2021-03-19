using DevExpress.WinUI.Grid;

namespace GridDemo {
    public sealed partial class GridSearchPanelModule : GridDemoUserControl {
        public GridSearchPanelModule() {
            this.InitializeComponent();
        }
        protected internal override GridControl Grid {
            get { return grid; }
        }
    }
}
