using System;
using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FeatureDemo.Common;
using DevExpress.WinUI.Core.Internal;

namespace ChartsDemo {
    public sealed partial class LineSeriesViewModule : DemoModuleView {
        const string AxisYLablePatternStr = "{V}";
        const string AxisYLablePatternPercentStr = "{V:P0}";

        #region static
        public static readonly DependencyProperty AxisYLablePatternProperty;
        public static readonly DependencyProperty ShowAxisYTitleProperty;
        public static readonly DependencyProperty IsLineCheckedProperty;
        public static readonly DependencyProperty IsStepLineCheckedProperty;
        public static readonly DependencyProperty IsFullStackedLineCheckedProperty;
        public static readonly DependencyProperty IsStackedLineCheckedProperty;
        static LineSeriesViewModule() {
            AxisYLablePatternProperty = DependencyProperty.Register("AxisYLablePattern", typeof(string), typeof(LineSeriesViewModule), new PropertyMetadata(null));
            ShowAxisYTitleProperty = DependencyProperty.Register("ShowAxisYTitle", typeof(bool), typeof(LineSeriesViewModule), new PropertyMetadata(null));
            IsLineCheckedProperty = DependencyProperty.Register("IsLineChecked", typeof(bool), typeof(LineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(IsLineCheckedPropertyChanged)));
            IsStepLineCheckedProperty = DependencyProperty.Register("IsStepLineChecked", typeof(bool), typeof(LineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(IsStepLineCheckedPropertyChanged)));
            IsFullStackedLineCheckedProperty = DependencyProperty.Register("IsFullStackedLineChecked", typeof(bool), typeof(LineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(IsFullStackedLineCheckedPropertyChanged)));
            IsStackedLineCheckedProperty = DependencyProperty.Register("IsStackedLineChecked", typeof(bool), typeof(LineSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(IsStackedLineCheckedPropertyChanged)));
        }
        private static void IsLineCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineSeriesViewModule)d).IsLineCheckedChanged((bool)e.NewValue, (bool)e.OldValue);
        }
        private static void IsStepLineCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineSeriesViewModule)d).IsStepLineCheckedChanged((bool)e.NewValue, (bool)e.OldValue);
        }
        private static void IsStackedLineCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineSeriesViewModule)d).IsStackedLineCheckedChanged((bool)e.NewValue, (bool)e.OldValue);
        }
        private static void IsFullStackedLineCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((LineSeriesViewModule)d).IsFullStackedLineCheckedChanged((bool)e.NewValue, (bool)e.OldValue);
        }

        #endregion
        #region dep props
        public bool IsLineChecked {
            get { return (bool)GetValue(IsLineCheckedProperty); }
            set { SetValue(IsLineCheckedProperty, value); }
        }
        public bool IsStepLineChecked {
            get { return (bool)GetValue(IsStepLineCheckedProperty); }
            set { SetValue(IsStepLineCheckedProperty, value); }
        }

        public bool IsFullStackedLineChecked {
            get { return (bool)GetValue(IsFullStackedLineCheckedProperty); }
            set { SetValue(IsFullStackedLineCheckedProperty, value); }
        }

        public bool IsStackedLineChecked {
            get { return (bool)GetValue(IsStackedLineCheckedProperty); }
            set { SetValue(IsStackedLineCheckedProperty, value); }
        }
        public string AxisYLablePattern {
            get { return (string)GetValue(AxisYLablePatternProperty); }
            set { SetValue(AxisYLablePatternProperty, value); }
        }
        public bool ShowAxisYTitle {
            get { return (bool)GetValue(ShowAxisYTitleProperty); }
            set { SetValue(ShowAxisYTitleProperty, value); }
        }
        #endregion


        public LineSeriesViewModule() {
            this.InitializeComponent();
            DataContext = this;
            Loaded += OnLoad;
            Unloaded += OnUnloaded;
        }
        void UpdateSeriesView(Func<SeriesView> func) {
            foreach (Series series in chart.Series)
                series.View = func();
        }
        void SetupAxisSettings(bool showTitle, string pattern) {
            ShowAxisYTitle = showTitle;
            AxisYLablePattern = pattern;
        }

        void OnLoad(object sender, RoutedEventArgs e) {
            IsLineChecked = true;

        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            DataContext = null;
            chart = null;
        }
        private void IsLineCheckedChanged(bool newValue, bool oldValue) {
            if(newValue) {
                UpdateSeriesView(() => { return new LineSeriesView() { ShowMarkers = true }; });
                SetupAxisSettings(true, AxisYLablePatternStr);
                chart.ActualAxisY.ActualWholeRange.AutoCorrect = true;
                chart.ActualAxisY.ActualWholeRange.SideMargins = 25;
                chart.ActualAxisX.ActualWholeRange.AutoCorrect = true;
                chart.ActualAxisX.ActualWholeRange.SideMargins = 3;
            }
        }
        private void IsStepLineCheckedChanged(bool newValue, bool oldValue) {
            if(newValue) {
                UpdateSeriesView(() => { return new StepLineSeriesView() { ShowMarkers = true }; });
                SetupAxisSettings(true, AxisYLablePatternStr);
                chart.ActualAxisY.ActualWholeRange.AutoCorrect = true;
                chart.ActualAxisY.ActualWholeRange.SideMargins = 25;
                chart.ActualAxisX.ActualWholeRange.AutoCorrect = true;
                chart.ActualAxisX.ActualWholeRange.SideMargins = 3;
            }
        }
        private void IsStackedLineCheckedChanged(bool newValue, bool oldValue) {
            if(newValue) {
                UpdateSeriesView(() => { return new StackedLineSeriesView() { ShowMarkers = true }; });
                SetupAxisSettings(true, AxisYLablePatternStr);
                chart.ActualAxisY.ActualWholeRange.AutoCorrect = true;
                chart.ActualAxisY.ActualWholeRange.SideMargins = 50;
                chart.ActualAxisX.ActualWholeRange.AutoCorrect = true;
                chart.ActualAxisX.ActualWholeRange.SideMargins = 3;
            }
        }
        private void IsFullStackedLineCheckedChanged(bool newValue, bool oldValue) {
            if(newValue) {
                UpdateSeriesView(() => { return new FullStackedLineSeriesView() { ShowMarkers = true }; });
                SetupAxisSettings(false, AxisYLablePatternPercentStr);
                chart.ActualAxisY.ActualWholeRange.AutoCorrect = false;
                chart.ActualAxisY.ActualWholeRange.StartValueInternal = -0.03;
                chart.ActualAxisY.ActualWholeRange.EndValueInternal = 1.03;
            }
        }
    }
}
