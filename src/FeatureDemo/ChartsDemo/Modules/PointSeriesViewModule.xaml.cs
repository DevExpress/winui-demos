using System;
using System.Globalization;
using System.Xml.Linq;
using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using FeatureDemo.Common;
using DevExpress.WinUI.Core.Internal;

namespace ChartsDemo {
    public sealed partial class PointSeriesViewModule : DemoModuleView {
        #region static
        public static readonly DependencyProperty showLabelsProperty;
        public static readonly DependencyProperty angleProperty;
        public static readonly DependencyProperty markerSizeProperty;
        public static readonly DependencyProperty indentProperty;

        static PointSeriesViewModule() {
            showLabelsProperty = DependencyProperty.Register("ShowLabels", typeof(bool), typeof(PointSeriesViewModule), new PropertyMetadata(true, new PropertyChangedCallback(OnShowLabelsPropertyChanged)));
            angleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(PointSeriesViewModule), new PropertyMetadata(30d, new PropertyChangedCallback(OnAngleValuePropertyChanged)));
            markerSizeProperty = DependencyProperty.Register("MarkerSize", typeof(int), typeof(PointSeriesViewModule), new PropertyMetadata(25, new PropertyChangedCallback(OnMarkerSizePropertyChanged)));
            indentProperty = DependencyProperty.Register("Indent", typeof(int), typeof(PointSeriesViewModule), new PropertyMetadata(0, new PropertyChangedCallback(OnIndentPropertyChanged)));
        }
        private static void OnShowLabelsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PointSeriesViewModule)d).OnShowLabelsChanged((bool)e.OldValue);
        }
        private static void OnAngleValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PointSeriesViewModule)d).OnAngleValueChanged((double)e.OldValue);
        }
        private static void OnMarkerSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PointSeriesViewModule)d).OnMarkerSizeChanged((int)e.OldValue);
        }
        private static void OnIndentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((PointSeriesViewModule)d).OnIndentChanged((int)e.OldValue);
        }
        #endregion
        #region dep props
        public bool ShowLabels {
            get { return (bool)GetValue(showLabelsProperty); }
            set { SetValue(showLabelsProperty, value); }
        }
        public double Angle {
            get { return (double)GetValue(angleProperty); }
            set { SetValue(angleProperty, value); }
        }
        public int MarkerSize {
            get { return (int)GetValue(markerSizeProperty); }
            set { SetValue(markerSizeProperty, value); }
        }
        public int Indent {
            get { return (int)GetValue(indentProperty); }
            set { SetValue(indentProperty, value); }
        }
        #endregion
        public PointSeriesViewModule() {
            this.InitializeComponent();
            DataContext = this;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            CreateData();
            SetMarkerSizeForSeries();
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            DataContext = null;
            cartesianChart = null;
        }

        void OnShowLabelsChanged(bool oldValue) {
            if(cartesianChart == null) return;
            foreach(Series series in cartesianChart.Series) {
                ((PointSeriesView)series.View).ShowLabels = ShowLabels;
            }

        }
        void OnAngleValueChanged(double oldValue) {
            if(cartesianChart == null) return;
            foreach(Series series in cartesianChart.Series) {
                MarkerSeriesView.SetAngle(((PointSeriesView)series.View).LabelOptions, Angle);
            }

        }
        void OnMarkerSizeChanged(double oldValue) {
            if(cartesianChart == null) return;
            SetMarkerSizeForSeries();
        }
        void OnIndentChanged(int oldValue) {
            if(cartesianChart == null) return;
            foreach(Series series in cartesianChart.Series) {
                PointSeriesView view = (PointSeriesView)series.View;
                view.LabelOptions.Indent = Indent;
            }
        }
        void CreateData() {
            XDocument document = DataLoader.LoadFromXmlResource("/Data/Movies.xml");
            if(document != null) {
                foreach(XElement element in document.Element("Movies").Elements()) {
                    Series series = new Series();
                    series.View = new PointSeriesView();
                    series.DisplayName = element.Element("Name").Value;
                    series.Data = new DataPointCollection() { ArgumentScaleType = ScaleType.Numerical };
                    series.View.ShowLabels = true;
                    series.View.LabelOptions = new SeriesLabelOptions();
                    series.View.LabelOptions.ShowConnectors = false;
                    series.View.LabelOptions.FontSize = 12;
                    series.View.LabelOptions.Pattern = "{S}";
                    series.View.LabelOptions.Indent = 10;
                    series.View.ToolTipPointPattern = "Budget : ${A}M\n" +
                                                      "Grosses: ${V}M";
                    MarkerSeriesView.SetAngle(series.View.LabelOptions, 20);
                    double argument = Convert.ToDouble(element.Element("ProductionBudget").Value, CultureInfo.InvariantCulture);
                    double value = Convert.ToDouble(element.Element("WorlwideGrosses").Value, CultureInfo.InvariantCulture);
                    ((DataPointCollection)series.Data).Add(new DataPoint(argument, value));
                    cartesianChart.Series.Add(series);
                }
            }
        }
        void SetMarkerSizeForSeries() {
            foreach(Series series in cartesianChart.Series) {
                ((PointSeriesView)series.View).MarkerSize = MarkerSize;
            }
        }
    }
}
