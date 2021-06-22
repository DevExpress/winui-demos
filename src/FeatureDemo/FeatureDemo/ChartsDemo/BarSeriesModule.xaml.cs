using DevExpress.WinUI.Charts;
using FeatureDemo.Common;
using System;

namespace ChartsDemo {
    public sealed partial class BarSeriesModule : DemoModuleView {
        const string ToolTipPatternStr = "{S}\n{A}: {V}";
        const string ToolTipPatternPercentStr = "{S}\n{A}: {VP:P}";
        const string AxisYLabelPatternStr = "{V}";
        const string AxisYLabelPatternPercentStr = "{V:P0}";

        public BarSeriesModule() {
            this.InitializeComponent();
        }
        public void OnIsSideBySideBarChecked() {
            UpdateSeriesView((group) => { return new SideBySideBarSeriesView() { ToolTipPointPattern = ToolTipPatternStr }; });
            SetupAxisSettings(true, AxisYLabelPatternStr);
        }
        public void OnIsStackedBarChecked() {
            UpdateSeriesView((group) => { return new StackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternStr }; });
            SetupAxisSettings(true, AxisYLabelPatternStr);
        }
        public void OnIsFullStackedBarChecked() {
            UpdateSeriesView((group) => { return new FullStackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternPercentStr }; });
            SetupAxisSettings(false, AxisYLabelPatternPercentStr);
        }
        public void OnIsSideBySideStackedBarChecked() {
            UpdateSeriesView((group) => { return new SideBySideStackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternStr, StackedGroup = group }; });
            SetupAxisSettings(true, AxisYLabelPatternStr);
        }
        public void OnIsSideBySideFullStackedBarChecked() {
            UpdateSeriesView((group) => { return new SideBySideFullStackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternPercentStr, StackedGroup = group }; });
            SetupAxisSettings(false, AxisYLabelPatternPercentStr);
        }

        void UpdateSeriesView(Func<string, SeriesView> func) {
            int seriesCount = chart.Series.Count;
            for(int i = 0; i < seriesCount; i++)
                chart.Series[i].View = func((i < (seriesCount / 2)) ? "Male" : "Famale");
        }
        void SetupAxisSettings(bool showTitle, string pattern) {
            axisYTitle.Visible = showTitle;
            axisYLabel.Pattern = pattern;
        }
    }
}
