using System;
using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using FeatureDemo.Common;
using DevExpress.WinUI.Core;
using Microsoft.UI;
using DevExpress.WinUI;
using Color = Windows.UI.Color;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Controls.Core;

namespace ChartsDemo {
    public sealed partial class ScatterLineSeriesViewModule : DemoModuleView {
        const int a = 10;
        #region static
        public static readonly DependencyProperty isCartesianFoliumProperty;
        public static readonly DependencyProperty isArchimedianSpiralProperty;
        public static readonly DependencyProperty isCardioidProperty;
        public static readonly DependencyProperty markerSizeProperty;
        public static readonly DependencyProperty showMarkersProperty;

        static ScatterLineSeriesViewModule() {
            isCartesianFoliumProperty = DependencyProperty.Register("IsCartesianFolim", typeof(bool), typeof(ScatterLineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCartesianFoliumPropertyChanged)));
            isArchimedianSpiralProperty = DependencyProperty.Register("IsArchimedianSpiral", typeof(bool), typeof(ScatterLineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsArchimedianSpiralPropertyChanged)));
            isCardioidProperty = DependencyProperty.Register("IsCardioid", typeof(bool), typeof(ScatterLineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCardioidPropertyChanged)));
            markerSizeProperty = DependencyProperty.Register("MarkerSize", typeof(double), typeof(ScatterLineSeriesViewModule), new PropertyMetadata(7d));
            showMarkersProperty = DependencyProperty.Register("ShowMarkers", typeof(bool), typeof(ScatterLineSeriesViewModule), new PropertyMetadata(true));
        }
        private static void OnIsCartesianFoliumPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ScatterLineSeriesViewModule)d).OnIsCartesianFoliumChanged((bool)e.OldValue);
        }
        private static void OnIsArchimedianSpiralPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ScatterLineSeriesViewModule)d).OnIsArchimedianSpiralChanged((bool)e.OldValue);
        }
        private static void OnIsCardioidPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ScatterLineSeriesViewModule)d).OnIsCardioidChanged((bool)e.OldValue);
        }
        #endregion
        #region dep props
        public bool IsCartesianFolium {
            get { return (bool)GetValue(isCartesianFoliumProperty); }
            set { SetValue(isCartesianFoliumProperty, value); }
        }
        public bool IsArchimedianSpiral {
            get { return (bool)GetValue(isArchimedianSpiralProperty); }
            set { SetValue(isArchimedianSpiralProperty, value); }
        }
        public bool IsCardioid {
            get { return (bool)GetValue(isCardioidProperty); }
            set { SetValue(isCardioidProperty, value); }
        }
        public double MarkerSize {
            get { return (double)GetValue(markerSizeProperty); }
            set { SetValue(markerSizeProperty, value); }
        }
        public bool ShowMarkers {
            get { return (bool)GetValue(showMarkersProperty); }
            set { SetValue(showMarkersProperty, value); }
        }
        #endregion

        DataPointCollection archimedianSpiral;
        DataPointCollection cardioid;
        DataPointCollection cartesianFolium;
        public ScatterLineSeriesViewModule() {
            this.InitializeComponent();
            DataContext = this;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            MarkerSize = ((ScatterLineSeriesView)cartesianChart.Series[0].View).MarkerSize;
            archimedianSpiral = (DataPointCollection)cartesianChart.Series[0].Data;
            cardioid = (DataPointCollection)cartesianChart.Series[1].Data;
            cartesianFolium = (DataPointCollection)cartesianChart.Series[2].Data;
            CreateArchimedianSpiralPoints();
            CreateCardioidPoints();
            CreateCartesianFoliumPoints();
            IsArchimedianSpiral = true;
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            DataContext = null;
            archimedianSpiral = null;
            cardioid = null;
            cartesianFolium = null;
        }
        void OnIsCartesianFoliumChanged(bool oldValue) {
            if (IsCartesianFolium && cartesianChart != null) {
                MakeAllSeriesInvisible();
                cartesianChart.Series[2].Visible = true;
            }
        }
        void OnIsArchimedianSpiralChanged(bool oldValue) {
            if (IsArchimedianSpiral && cartesianChart != null) {
                MakeAllSeriesInvisible();
                cartesianChart.Series[0].Visible = true;
            }
        }
        void OnIsCardioidChanged(bool oldValue) {
            if (IsCardioid && cartesianChart != null) {
                MakeAllSeriesInvisible();
                cartesianChart.Series[1].Visible = true;
            }
        }
        void CreateArchimedianSpiralPoints() {
            for (int i = 0; i < 720; i += 15) {
                double t = (double)i / 180 * Math.PI;
                double x = t * Math.Cos(t);
                double y = t * Math.Sin(t);
                archimedianSpiral.Add(new DevExpress.WinUI.Charts.DataPoint(x, y));
            }
        }
        void CreateCardioidPoints() {
            for (int i = 0; i < 360; i += 10) {
                double t = (double)i / 180 * Math.PI;
                double x = a * (2 * Math.Cos(t) - Math.Cos(2 * t));
                double y = a * (2 * Math.Sin(t) - Math.Sin(2 * t));
                cardioid.Add(new DevExpress.WinUI.Charts.DataPoint(x, y));
            }
        }
        void CreateCartesianFoliumPoints() {
            for (int i = -30; i < 125; i += 5) {
                double t = Math.Tan((double)i / 180 * Math.PI);
                double x = 3 * (double)a * t / (t * t * t + 1);
                double y = x * t;
                cartesianFolium.Add(new DevExpress.WinUI.Charts.DataPoint(x, y));
            }
        }
        void MakeAllSeriesInvisible() {
            if (cartesianChart == null)
                return;
            foreach (Series series in cartesianChart.Series)
                series.Visible = false;
        }
    }
}
