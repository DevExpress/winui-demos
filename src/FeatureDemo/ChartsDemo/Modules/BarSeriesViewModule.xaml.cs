using System;
using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FeatureDemo.Common;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Mvvm.Internal;

namespace ChartsDemo {
    public sealed partial class BarSeriesViewModule : DemoModuleView {
        const string ToolTipPatternStr = "{S}\n{A}: {V}";
        const string ToolTipPatternPercentStr = "{S}\n{A}: {VP:P}";
        const string AxisYLablePatternStr = "{V}";
        const string AxisYLablePatternPercentStr = "{V:P0}";

        #region static
        public static readonly DependencyProperty showAxisYTitleProperty;
        public static readonly DependencyProperty axisYLablePatternProperty;
        public static readonly DependencyProperty isSideBySideBarCheckedProperty;
        public static readonly DependencyProperty isStackedBarCheckedProperty;
        public static readonly DependencyProperty isFullStackedBarCheckedProperty;
        public static readonly DependencyProperty isSideBySideStackedBarCheckedProperty;
        public static readonly DependencyProperty isSideBySideFullStackedBarCheckedProperty;
        public static readonly DependencyProperty isRotatedProperty;
        static BarSeriesViewModule() {
            showAxisYTitleProperty = DependencyProperty.Register("ShowAxisYTitle", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(null));
            axisYLablePatternProperty = DependencyProperty.Register("AxisYLablePattern", typeof(string), typeof(BarSeriesViewModule), new PropertyMetadata(false));
            isSideBySideBarCheckedProperty = DependencyProperty.Register("IsSideBySideBarChecked", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsSideBySideBarCheckedPropertyChanged)));
            isStackedBarCheckedProperty = DependencyProperty.Register("IsStackedBarChecked", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsStackedBarCheckedPropertyChanged)));
            isFullStackedBarCheckedProperty = DependencyProperty.Register("IsFullStackedBarChecked", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsFullStackedBarCheckedPropertyChanged)));
            isSideBySideStackedBarCheckedProperty = DependencyProperty.Register("IsSideBySideStackedBarChecked", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsSideBySideStackedBarCheckedPropertyChanged)));
            isSideBySideFullStackedBarCheckedProperty = DependencyProperty.Register("IsSideBySideFullStackedBarChecked", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnIsSideBySideFullStackedBarCheckedPropertyChanged)));
            isRotatedProperty = DependencyProperty.Register("IsRotated", typeof(bool), typeof(BarSeriesViewModule), new PropertyMetadata(DeviceFamilyHelper.DeviceFamily != DeviceFamily.Desktop));
    }
        private static void OnIsSideBySideBarCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((BarSeriesViewModule)d).OnIsSideBySideBarCheckedChanged((bool)e.OldValue);
        }
        private static void OnIsStackedBarCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((BarSeriesViewModule)d).OnIsStackedBarCheckedChanged((bool)e.OldValue);
        }
        private static void OnIsFullStackedBarCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((BarSeriesViewModule)d).OnIsFullStackedBarCheckedChanged((bool)e.OldValue);
        }
        private static void OnIsSideBySideStackedBarCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((BarSeriesViewModule)d).OnIsSideBySideStackedBarCheckedChanged((bool)e.OldValue);
        }
        private static void OnIsSideBySideFullStackedBarCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((BarSeriesViewModule)d).OnIsSideBySideFullStackedBarCheckedChanged((bool)e.OldValue);
        }

        #endregion
        #region dep props
        public string AxisYLablePattern {
            get { return (string)GetValue(axisYLablePatternProperty); }
            set { SetValue(axisYLablePatternProperty, value); }
        }
        public bool ShowAxisYTitle {
            get { return (bool)GetValue(showAxisYTitleProperty); }
            set { SetValue(showAxisYTitleProperty, value); }
        }
        public bool IsSideBySideBarChecked {
            get { return (bool)GetValue(isSideBySideBarCheckedProperty); }
            set { SetValue(isSideBySideBarCheckedProperty, value); }
        }
        public bool IsStackedBarChecked {
            get { return (bool)GetValue(isStackedBarCheckedProperty); }
            set { SetValue(isStackedBarCheckedProperty, value); }
        }
        public bool IsFullStackedBarChecked {
            get { return (bool)GetValue(isFullStackedBarCheckedProperty); }
            set { SetValue(isFullStackedBarCheckedProperty, value); }
        }
        public bool IsSideBySideStackedBarChecked {
            get { return (bool)GetValue(isSideBySideStackedBarCheckedProperty); }
            set { SetValue(isSideBySideStackedBarCheckedProperty, value); }
        }
        public bool IsSideBySideFullStackedBarChecked {
            get { return (bool)GetValue(isSideBySideFullStackedBarCheckedProperty); }
            set { SetValue(isSideBySideFullStackedBarCheckedProperty, value); }
        }
        public bool IsRotated {
            get { return (bool)GetValue(isRotatedProperty); }
            set { SetValue(isRotatedProperty, value); }
        }
        #endregion

        public BarSeriesViewModule() {
            DataContext = this;
            Loaded += OnLoad;
            Unloaded += OnUnload;
            this.InitializeComponent();
        }

        void UpdateSeriesView(Func<string, SeriesView> func) {
            int seriesCount = chart.Series.Count;
            for (int i = 0; i < seriesCount; i++)
                chart.Series[i].View = func((i < (seriesCount / 2)) ? "Male" : "Famale");
        }
        void SetupAxisSettings(bool showTitle, string pattern) {
            ShowAxisYTitle = showTitle;
            AxisYLablePattern = pattern;
        }
        void OnLoad(object sender, RoutedEventArgs e) {
            IsSideBySideBarChecked = true;
        }
        void OnUnload(object sender, RoutedEventArgs e) {
            DataContext = null;
            chart = null;
        }
        private void OnIsSideBySideBarCheckedChanged(bool oldValue) {
            if(IsSideBySideBarChecked) {
                UpdateSeriesView((group) => { return new SideBySideBarSeriesView() { ToolTipPointPattern = ToolTipPatternStr }; });
                SetupAxisSettings(true, AxisYLablePatternStr);
            }

        }
        void OnIsStackedBarCheckedChanged(bool oldValue) {
            if(IsStackedBarChecked) {
                UpdateSeriesView((group) => { return new StackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternStr }; });
                SetupAxisSettings(true, AxisYLablePatternStr);
            }
        }
        void OnIsFullStackedBarCheckedChanged(bool oldValue) {
            if(IsFullStackedBarChecked) {
                UpdateSeriesView((group) => { return new FullStackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternPercentStr }; });
                SetupAxisSettings(false, AxisYLablePatternPercentStr);
            }
        }
        void OnIsSideBySideStackedBarCheckedChanged(bool oldValue) {
            if(IsSideBySideStackedBarChecked) {
                UpdateSeriesView((group) => { return new SideBySideStackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternStr, StackedGroup = group }; });
                SetupAxisSettings(true, AxisYLablePatternStr);
            }
        }

        void OnIsSideBySideFullStackedBarCheckedChanged(bool oldValue) {
            if(IsSideBySideFullStackedBarChecked) {
                UpdateSeriesView((group) => { return new SideBySideFullStackedBarSeriesView() { ToolTipPointPattern = ToolTipPatternPercentStr, StackedGroup = group }; });
                SetupAxisSettings(false, AxisYLablePatternPercentStr);
            }
        }
    }
}
