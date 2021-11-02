using DevExpress.WinUI.Grid;
using FeatureDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace GridDemo {
    public abstract class GridDemoUserControl : DemoModuleView {
        public double MinGridWidth { get { return Grid.VisibleColumns.Sum(column => column.FixedWidth ? column.Width.Value : column.MinWidth); }}
        protected internal abstract GridControl Grid { get; }
        public virtual void OnNavigatingFrom() {
            SizeChanged -= GridDemoUserControl_SizeChanged;
            Grid.EndGrouping -= Grid_EndGrouping;
        }
        public virtual void OnNavigatedTo() {
            SizeChanged += GridDemoUserControl_SizeChanged;
            Grid.EndGrouping += Grid_EndGrouping;
            UpdateAutoWidth();
        }

        void Grid_EndGrouping(object sender, EventArgs e) {
            UpdateAutoWidth();
        }
        void GridDemoUserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateAutoWidth();
        }
        void UpdateAutoWidth() {
            if(NeedChangeAutoWidth)
                Grid.AutoWidth = Grid.ActualWidth > MinGridWidth;
        }
        protected virtual bool NeedChangeAutoWidth { get { return true; } }
    }
}
