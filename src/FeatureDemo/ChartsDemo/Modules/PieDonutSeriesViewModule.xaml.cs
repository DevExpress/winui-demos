using DevExpress.WinUI.Charts;
using FeatureDemo.Common;
using System;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Controls.Core;
using FeatureDemo.Data;

namespace ChartsDemo {

    public sealed partial class PieDonutSeriesViewModule : DemoModuleView {
        const string LabelPattern = "{VP:P0}";
        const string LegendPointPattern = "{A}";
        const string ToolTipPointPatternForPieDoughnutTotal = "{A}: ${V:0.00}B";
        const string ToolTipPointPatternForPieDoughnutPerCapita = "{A}: ${V:0.00}K";
        const string ToolTipPointPatternForNested = "{A} in {S}\nGSP: ${V:0.0000}B";



        public bool PieChecked {
            get { return (bool)GetValue(PieCheckedProperty); }
            set { SetValue(PieCheckedProperty, value); }
        }

        public static readonly DependencyProperty PieCheckedProperty =
            DependencyProperty.Register("PieChecked", typeof(bool), typeof(PieDonutSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnPieCheckedChanged)));

        public bool DonutChecked {
            get { return (bool)GetValue(DonutCheckedProperty); }
            set { SetValue(DonutCheckedProperty, value); }
        }

        public static readonly DependencyProperty DonutCheckedProperty =
            DependencyProperty.Register("DonutChecked", typeof(bool), typeof(PieDonutSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnDonutCheckedChanged)));


        public bool NestedDonutChecked {
            get { return (bool)GetValue(NestedDonutCheckedProperty); }
            set { SetValue(NestedDonutCheckedProperty, value); }
        }

        public static readonly DependencyProperty NestedDonutCheckedProperty =
            DependencyProperty.Register("NestedDonutChecked", typeof(bool), typeof(PieDonutSeriesViewModule), new PropertyMetadata(false, new PropertyChangedCallback(OnNestedDonutCheckedChanged)));

        Windows.UI.ViewManagement.ApplicationViewOrientation currentOrientation = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Orientation;

        public PieDonutSeriesViewModule() {
            this.InitializeComponent();
            Loaded += OnLoad;
            Unloaded += OnUnloaded;
        }

        void OnLoad(object sender, RoutedEventArgs e) {
            PieChecked = true;
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
        }
        void UpdateSeriesView(Func<Series, SeriesView> func) {            
            foreach(Series series in pieChart.Series)
                series.View = func(series);
        }
        private static void OnPieCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) {
                PieDonutSeriesViewModule module = (PieDonutSeriesViewModule)d;
                module.UpdateSeriesView((series) => {
                    SeriesLabelOptions seriesLabelOptions = new SeriesLabelOptions() { Indent = 10, FontSize = 12, Pattern = LabelPattern };
                    PieSeriesView.SetLabelPosition(seriesLabelOptions, PieLabelPosition.TwoColumns);
                    string toolTipPattern = series.DisplayName == "Total" ? ToolTipPointPatternForPieDoughnutTotal : ToolTipPointPatternForPieDoughnutPerCapita;
                    return new PieSeriesView() {
                        ShowLabels = true,
                        LegendPointPattern = LegendPointPattern,
                        ToolTipPointPattern = toolTipPattern,
                        LabelOptions = seriesLabelOptions,
                        Title = new SeriesTitle() {
                            Content = series.DisplayName,
                            FontSize = 20,
                            Margin = new Thickness()
                        }
                    };
                });
                module.SetLegendHorizontal();
            }
            
        }
        private static void OnDonutCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) {
                PieDonutSeriesViewModule module = (PieDonutSeriesViewModule)d;
                module.UpdateSeriesView((series) => {
                    SeriesLabelOptions seriesLabelOptions = new SeriesLabelOptions() { Indent = 10, FontSize = 12, Pattern = LabelPattern };
                    DonutSeriesView.SetLabelPosition(seriesLabelOptions, PieLabelPosition.Outside);
                    ContourAppearance contourAppearance = new ContourAppearance() {
                        Brush = new SolidColorBrush(Colors.Transparent),
                        StrokeStyle = new StrokeStyle() {
                            Thickness = 3,
                            LineJoin = PenLineJoin.Round
                        }
                    };
                    string toolTipPattern = series.DisplayName == "Total" ? ToolTipPointPatternForPieDoughnutTotal : ToolTipPointPatternForPieDoughnutPerCapita;
                    return new DonutSeriesView() {
                        HoleRadiusPercent = 30,
                        ContourAppearance = contourAppearance,
                        LegendPointPattern = LegendPointPattern,
                        ToolTipPointPattern = toolTipPattern,
                        ShowLabels = true,
                        LabelOptions = seriesLabelOptions,
                        Title = new SeriesTitle() {
                            Content = series.DisplayName,
                            FontSize = 20
                        }
                    };
                });
                module.SetLegendHorizontal();
            }
        }
        private static void OnNestedDonutCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if((bool)e.NewValue) {
                PieDonutSeriesViewModule module = (PieDonutSeriesViewModule)d;
                module.UpdateSeriesView((series) => {
                    return new NestedDonutSeriesView() {
                        HoleRadiusPercent = 30,
                        LegendPointPattern = LegendPointPattern,
                        ToolTipPointPattern = ToolTipPointPatternForNested,
                    };
                });
                module.pieChart.Series[0].ActualView.ShowLabels = false;
                module.pieChart.Series[1].ActualView.ShowLabels = false;
                module.SetLegendPositionForNestedDoughnut();
            }
        }        
        
        void SetLegendPositionForNestedDoughnut() {
            if (currentOrientation == Windows.UI.ViewManagement.ApplicationViewOrientation.Landscape)
                SetLegendVertical();
            else
                SetLegendHorizontal();
        }
        void SetLegendHorizontal() {
            Legend legend = pieChart.Legend;
            legend.HorizontalPosition = HorizontalPosition.Center;
            legend.VerticalPosition = VerticalPosition.BottomOutside;
            legend.Orientation = Orientation.Horizontal;
            legend.MaximumRowsOrColumns = 4;
        }
        void SetLegendVertical() {
            Legend legend = pieChart.Legend;
            legend.HorizontalPosition = HorizontalPosition.RightOutside;
            legend.VerticalPosition = VerticalPosition.Center;
            legend.Orientation = Orientation.Vertical;
            legend.MaximumRowsOrColumns = 8;
        }

        #region ILayoutAwareControl Members
        public void UpdateViewState() {
            this.currentOrientation = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().Orientation;
            if (pieChart.Series[0].ActualView is NestedDonutSeriesView){
                SetLegendPositionForNestedDoughnut();
            }
        }
        #endregion
    }
}
