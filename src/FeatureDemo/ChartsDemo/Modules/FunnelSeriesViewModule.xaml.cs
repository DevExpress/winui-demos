using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using DevExpress.Mvvm.Native;
using FeatureDemo.Common;
using DevExpress.WinUI.Core.Internal;

namespace ChartsDemo {
    public sealed partial class FunnelSeriesViewModule : DemoModuleView {
        #region static
        public static readonly DependencyProperty pointDistanceProperty;
        static FunnelSeriesViewModule() {
            pointDistanceProperty = DependencyProperty.Register("PointDistance", typeof(int), typeof(FunnelSeriesViewModule), new PropertyMetadata(0, new PropertyChangedCallback(OnPointDistancePropertyChanged)));
        }
        private static void OnPointDistancePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((FunnelSeriesViewModule)d).OnPointDistanceChanged((int)e.OldValue);
        }
        #endregion
        #region dep props
        public int PointDistance {
            get { return (int)GetValue(pointDistanceProperty); }
            set { SetValue(pointDistanceProperty, value); }
        }
        #endregion

        public FunnelSeriesViewModule() {
            this.InitializeComponent();
            PointDistance = 5;
            DataContext = this;
            Loaded += OnLoaded;
            Unloaded += OnUnLoaded;
        }
        void OnPointDistanceChanged(double oldValue) {
            if(chart == null) return;
            foreach(Series series in chart.Series) {
                ((FunnelSeriesView)series.View).PointDistance = PointDistance;
            }
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            chart.Animate();
        }
        void OnUnLoaded(object sender, RoutedEventArgs e) {
            DataContext = null;
            chart = null;
        }
    }
}
