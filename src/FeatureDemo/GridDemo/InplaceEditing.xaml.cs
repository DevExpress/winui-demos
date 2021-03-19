using DevExpress.WinUI.Grid;

namespace GridDemo {
    public sealed partial class InplaceEditing : GridDemoUserControl {
        public InplaceEditing() {
            this.InitializeComponent();
        }
        protected internal override GridControl Grid { get { return grid; } }
    }
}
