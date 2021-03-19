using DevExpress.WinUI.Grid;
using Microsoft.UI.Xaml;

namespace GridDemo {
    public sealed partial class GroupingModule : GridDemoUserControl {
        public GroupingModule() {
            this.InitializeComponent();
            grid.Loaded += gridLoaded;            
        }
        protected internal override GridControl Grid { get { return grid; } }
        void gridLoaded(object sender, RoutedEventArgs e) {
            grid.Loaded -= gridLoaded;
            grid.ExpandGroupRow(-1);
            grid.ExpandGroupRow(-2);
        }
    }
}
