using DevExpress.WinUI.Charts;
using System.Collections.Generic;

namespace ChartsDemo {
    public class AreaSeriesCompanyMarketValue {
        public string CompanyName { get; set; }
        public double MarketValue { get; set; }
        public AreaSeriesCompanyMarketValue(string companyName, double marketValue) {
            CompanyName = companyName;
            MarketValue = marketValue;
        }
    }
    public class AreaSeriesViewModel : DevExpress.Mvvm.BindableBase {
        const string AxisYLablePatternStr = "{V}";
        const string AxisYLablePatternPercentStr = "{V:P0}";

        #region private fields
        private SeriesView series1View;
        private SeriesView series2View;
        private string axisYLablePattern;
        private bool showAxisYTitle;
        private bool isAreaChecked;
        private bool isStackedAreaChecked;
        private bool isFullStackedAreaChecked;
        private bool isStepAreaChecked;
        private List<AreaSeriesCompanyMarketValue> series1Data;
        private List<AreaSeriesCompanyMarketValue> series2Data;
        #endregion
        #region properties
        public SeriesView Series1View {
            get { return series1View; }
            set { SetProperty(ref series1View, value); }
        }
        public SeriesView Series2View {
            get { return series2View; }
            set { SetProperty(ref series2View, value); }
        }
        public string AxisYLablePattern {
            get { return axisYLablePattern; }
            set { SetProperty(ref axisYLablePattern, value); }
        }
        public bool ShowAxisYTitle {
            get { return showAxisYTitle; }
            set { SetProperty(ref showAxisYTitle, value); }
        }
        public bool IsAreaChecked {
            get { return isAreaChecked; }
            set { SetProperty(ref isAreaChecked, value, OnIsAreaCheckedChanged); }
        }
        public bool IsStackedAreaChecked {
            get { return isStackedAreaChecked; }
            set { SetProperty(ref isStackedAreaChecked, value, OnIsStackedAreaCheckedChanged); }
        }
        public bool IsFullStackedAreaChecked {
            get { return isFullStackedAreaChecked; }
            set { SetProperty(ref isFullStackedAreaChecked, value, OnIsFullStackedAreaCheckedChanged); }
        }
        public bool IsStepAreaChecked {
            get { return isStepAreaChecked; }
            set { SetProperty(ref isStepAreaChecked, value, OnIsStepAreaCheckedChanged); }
        }

        public List<AreaSeriesCompanyMarketValue> Series1Data {
            get { return series1Data; }
            set { SetProperty(ref series1Data, value); }
        }
        public List<AreaSeriesCompanyMarketValue> Series2Data {
            get { return series2Data; }
            set { SetProperty(ref series2Data, value); }
        }

        #endregion
        public void Initialize() {
            Series1Data = new List<AreaSeriesCompanyMarketValue>() {
                new AreaSeriesCompanyMarketValue("Apple", 2254.000),
                new AreaSeriesCompanyMarketValue("Amazon", 1634.000),
                new AreaSeriesCompanyMarketValue("Microsoft", 1682.000),
                new AreaSeriesCompanyMarketValue("Facebook", 776.590),
                new AreaSeriesCompanyMarketValue("Alphabet", 1185.000),
                new AreaSeriesCompanyMarketValue("Tencent", 683.470)
            };
            Series2Data = new List<AreaSeriesCompanyMarketValue>() {
                new AreaSeriesCompanyMarketValue("Apple", 1305.000),
                new AreaSeriesCompanyMarketValue("Amazon", 916.150),
                new AreaSeriesCompanyMarketValue("Microsoft", 1203.000),
                new AreaSeriesCompanyMarketValue("Facebook", 585.320),
                new AreaSeriesCompanyMarketValue("Alphabet", 922.130),
                new AreaSeriesCompanyMarketValue("Tencent", 461.370)
            };
            IsAreaChecked = true;
        }
        void OnIsAreaCheckedChanged(bool oldValue, bool newValue) {
            if(newValue) {
                UpdateSettings(new AreaSeriesView() { ShowContour = true }, new AreaSeriesView() { ShowContour = true }, true, AxisYLablePatternStr);
            }
        }
        void OnIsStackedAreaCheckedChanged(bool oldValue, bool newValue) {
            if(newValue) {
                UpdateSettings(new StackedAreaSeriesView() { ShowLabels = true, LabelOptions = new SeriesLabelOptions { FontSize = 10 } }, new StackedAreaSeriesView() { ShowLabels = true, LabelOptions = new SeriesLabelOptions { FontSize = 10 } }, true, AxisYLablePatternStr);
            }
        }
        void OnIsStepAreaCheckedChanged(bool oldValue, bool newValue) {
            if(newValue) {
                UpdateSettings(new StepAreaSeriesView(), new StepAreaSeriesView(), true, AxisYLablePatternStr);
            }
        }
        void OnIsFullStackedAreaCheckedChanged(bool oldValue, bool newValue) {
            if(newValue) {
                UpdateSettings(new FullStackedAreaSeriesView(), new FullStackedAreaSeriesView(), false, AxisYLablePatternPercentStr);
            }
        }
        void UpdateSettings(SeriesView series1View, SeriesView series2View, bool showTitle, string pattern) {
            Series1View = series1View;
            Series2View = series2View;
            ShowAxisYTitle = showTitle;
            AxisYLablePattern = pattern;
        }
    }
}