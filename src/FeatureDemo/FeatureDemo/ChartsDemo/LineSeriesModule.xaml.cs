using DevExpress.WinUI.Charts;
using FeatureDemo.Common;
using System;

namespace ChartsDemo {
    public sealed partial class LineSeriesModule : DemoModuleView {
        const string AxisYLabelPatternStr = "{V}";
        const string AxisYLabelPatternPercentStr = "{V:P0}";

        public LineSeriesModule() {
            this.InitializeComponent();
        }

        public void OnIsLineChecked() {
            UpdateSeriesView(() => { return new LineSeriesView() { ShowMarkers = true }; });
            SetupAxisSettings(true, AxisYLabelPatternStr);
            chart.ActualAxisY.ActualWholeRange.AutoCorrect = true;
            chart.ActualAxisY.ActualWholeRange.SideMargins = 25;
            chart.ActualAxisX.ActualWholeRange.AutoCorrect = true;
            chart.ActualAxisX.ActualWholeRange.SideMargins = 3;
        }
        public void OnIsStepLineChecked() {
            UpdateSeriesView(() => { return new StepLineSeriesView() { ShowMarkers = true }; });
            SetupAxisSettings(true, AxisYLabelPatternStr);
            chart.ActualAxisY.ActualWholeRange.AutoCorrect = true;
            chart.ActualAxisY.ActualWholeRange.SideMargins = 25;
            chart.ActualAxisX.ActualWholeRange.AutoCorrect = true;
            chart.ActualAxisX.ActualWholeRange.SideMargins = 3;
        }
        public void OnIsStackedLineChecked() {
            UpdateSeriesView(() => { return new StackedLineSeriesView() { ShowMarkers = true }; });
            SetupAxisSettings(true, AxisYLabelPatternStr);
            chart.ActualAxisY.ActualWholeRange.AutoCorrect = true;
            chart.ActualAxisY.ActualWholeRange.SideMargins = 50;
            chart.ActualAxisX.ActualWholeRange.AutoCorrect = true;
            chart.ActualAxisX.ActualWholeRange.SideMargins = 3;
        }
        public void OnIsFullStackedLineChecked() {
            UpdateSeriesView(() => { return new FullStackedLineSeriesView() { ShowMarkers = true }; });
            SetupAxisSettings(false, AxisYLabelPatternPercentStr);
            chart.ActualAxisY.ActualWholeRange.AutoCorrect = false;
            chart.ActualAxisY.ActualWholeRange.StartValueInternal = -0.03;
            chart.ActualAxisY.ActualWholeRange.EndValueInternal = 1.03;
        }
        void UpdateSeriesView(Func<SeriesView> func) {
            foreach(Series series in chart.Series)
                series.View = func();
        }
        void SetupAxisSettings(bool showTitle, string pattern) {
            axisYTitle.Visible = showTitle;
            axisYLabel.Pattern = pattern;
        }
    }
}
