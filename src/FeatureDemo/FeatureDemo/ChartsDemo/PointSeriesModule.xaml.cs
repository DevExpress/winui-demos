using DevExpress.WinUI.Charts;
using FeatureDemo.Common;
using System;
using System.Globalization;
using System.Xml.Linq;

namespace ChartsDemo {
    public sealed partial class PointSeriesModule : DemoModuleView {
        public PointSeriesModule() {
            this.InitializeComponent();
            CreateData();
            OnMarkerSizeChanged();
            OnShowLabelsChanged();
            OnIndentChanged();
            OnAngleChanged();
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

        public void OnMarkerSizeChanged() {
            foreach(Series series in cartesianChart.Series)
                ((PointSeriesView)series.View).MarkerSize = (int)markerSize.Value;
        }
        public void OnShowLabelsChanged() {
            foreach(Series series in cartesianChart.Series)
                ((PointSeriesView)series.View).ShowLabels = showLabels.IsChecked.Value;
        }
        public void OnIndentChanged() {
            foreach(Series series in cartesianChart.Series) {
                PointSeriesView view = (PointSeriesView)series.View;
                view.LabelOptions.Indent = (int)indent.Value;
            }
        }
        public void OnAngleChanged() {
            foreach(Series series in cartesianChart.Series)
                MarkerSeriesView.SetAngle(((PointSeriesView)series.View).LabelOptions, angle.Value);
        }
    }
}
