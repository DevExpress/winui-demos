using DevExpress.WinUI.Charts;
using DevExpress.WinUI.Core;
using DevExpress.WinUI.Drawing;
using FeatureDemo.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;

namespace ChartsDemo {
    public sealed partial class PieSeriesModule : DemoModuleView {
        const string LabelPattern = "{VP:P0}";
        const string LegendPointPattern = "{A}";
        const string ToolTipPointPatternForPieDoughnutTotal = "{A}: ${V:0.00}B";
        const string ToolTipPointPatternForPieDoughnutPerCapita = "{A}: ${V:0.00}K";
        const string ToolTipPointPatternForNested = "{A} in {S}\nGSP: ${V:0.0000}B";

        public PieSeriesModule() {
            this.InitializeComponent();
        }

        public void OnPieChecked() {
            UpdateSeriesView((series) => {
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
            SetLegendHorizontal();
        }
        public void OnDonutChecked() {
            UpdateSeriesView((series) => {
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
            SetLegendHorizontal();
        }
        public void OnNestedDonutChecked() {
            UpdateSeriesView((series) => {
                return new NestedDonutSeriesView() {
                    HoleRadiusPercent = 30,
                    LegendPointPattern = LegendPointPattern,
                    ToolTipPointPattern = ToolTipPointPatternForNested,
                };
            });
            pieChart.Series[0].ActualView.ShowLabels = false;
            pieChart.Series[1].ActualView.ShowLabels = false;
            SetLegendPositionForNestedDoughnut();
        }

        void UpdateSeriesView(Func<Series, SeriesView> func) {
            foreach(Series series in pieChart.Series)
                series.View = func(series);
        }
        void SetLegendPositionForNestedDoughnut() {
            SetLegendVertical();
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
    }
}
