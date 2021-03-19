using DevExpress.WinUI.Grid;
using Microsoft.UI.Xaml;

namespace GridDemo {
    public sealed partial class CellTemplatesModule : GridDemoUserControl {
        public CellTemplatesModule() {
            this.InitializeComponent();
            Unloaded += OnUnloaded;
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            gridControl.DataContext = null;
            gridRoot.DataContext = null;
        }
        protected internal override GridControl Grid {
            get { return gridControl; }
        }        
    }
}