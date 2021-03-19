using FeatureDemo.Data;
using DevExpress.WinUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Color = Windows.UI.Color;
using DevExpress.WinUI.Core.Internal;

namespace GridDemo {
    public class DemoSparkBarControl : Control {
        public static readonly DependencyProperty ActualValueProperty = DependencyProperty.Register("ActualValue", typeof(double), typeof(DemoSparkBarControl), new PropertyMetadata(0d, (d, e) => ((DemoSparkBarControl)d).UpdateContent()));
        public static readonly DependencyProperty TargetValueProperty = DependencyProperty.Register("TargetValue", typeof(double), typeof(DemoSparkBarControl), new PropertyMetadata(0d, (d, e) => ((DemoSparkBarControl)d).UpdateContent()));
        public static readonly DependencyProperty NegativeDiffColorProperty = DependencyProperty.Register("NegativeDiffColor", typeof(Color), typeof(DemoSparkBarControl), new PropertyMetadata(Colors.Transparent, (d, e) => ((DemoSparkBarControl)d).UpdateContent()));
        public static readonly DependencyProperty PositiveDiffColorProperty = DependencyProperty.Register("PositiveDiffColor", typeof(Color), typeof(DemoSparkBarControl), new PropertyMetadata(Colors.Transparent, (d, e) => ((DemoSparkBarControl)d).UpdateContent()));
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(DemoSparkBarControl), new PropertyMetadata(null));

        public DemoSparkBarControl() {
            BarScale = 1;
            DefaultStyleKey = typeof(DemoSparkBarControl);
        }

        public int BarScale { get; set; }
        public double ActualValue {
            get { return (double)GetValue(ActualValueProperty); }
            set { SetValue(ActualValueProperty, value); }
        }
        public double TargetValue {
            get { return (double)GetValue(TargetValueProperty); }
            set { SetValue(TargetValueProperty, value); }
        }
        public Color NegativeDiffColor {
            get { return (Color)GetValue(NegativeDiffColorProperty); }
            set { SetValue(NegativeDiffColorProperty, value); }
        }
        public Color PositiveDiffColor {
            get { return (Color)GetValue(PositiveDiffColorProperty); }
            set { SetValue(PositiveDiffColorProperty, value); }
        }
        public Brush Fill {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        Border BorderValue { get; set; }
        Border BorderTargetValue { get; set; }
        Border BorderDifference { get; set; }
        TextBlock TextBlockDifference { get; set; }
        double MaxAvailableWidth { get; set; }
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            BorderValue = GetTemplateChild("PART_BorderValue") as Border;
            BorderTargetValue = GetTemplateChild("PART_BorderTargetValue") as Border;
            BorderDifference = GetTemplateChild("PART_BorderDifference") as Border;
            TextBlockDifference = GetTemplateChild("PART_TextBlockDifference") as TextBlock;
            if(BorderValue != null)
                UpdateContent();
        }

        void UpdateContent() {
            UpdateActualValue();
            UpdateTargetValue();
            UpdateDifference();
        }
        void UpdateDifference() {
            if(TextBlockDifference == null)
                return;
            double diff = ActualValue - TargetValue;
            TextBlockDifference.Foreground = new SolidColorBrush(diff < 0 ? NegativeDiffColor : PositiveDiffColor);
            TextBlockDifference.Text = (diff / 1000).ToString("$#,0" + "K");
        }
        void UpdateTargetValue() {
            if(BorderTargetValue == null)
                return;
            BorderTargetValue.Margin = WinUICompatibility.CreateThickness(TargetValue / BarScale, 0, 0, 0);
        }
        void UpdateActualValue() {
            if(BorderValue == null)
                return;
            BorderValue.Width = ActualValue / BarScale;
        }
        protected override Size MeasureOverride(Size availableSize) {
            double borderDifferenceWidth = 0;
            if(BorderDifference != null)
                borderDifferenceWidth = BorderDifference.ActualWidth;
            MaxAvailableWidth = availableSize.Width - borderDifferenceWidth;
            Size size = base.MeasureOverride(availableSize);
            if(double.IsInfinity(MaxAvailableWidth))
                MaxAvailableWidth = size.Width;
            return size;
        }
    }

    public class DemoSparkColumnChart  : DemoSparkLineChart {
        readonly double offset = 2.5;
        public DemoSparkColumnChart() {
            DefaultStyleKey = typeof(DemoSparkColumnChart);
            StrokeThickness = 0;            
        }
        public TextBlock TextBlockFirstValue { get; set; }
        public TextBlock TextBlockLastValue { get; set; }
        protected internal override PathSegmentCollection GenerateSegments(List<double> points) {
            PathSegmentCollection segments = new PathSegmentCollection();
            segments.Add(new PolyLineSegment() { Points = CreateColumn(points) });
            return segments;
        }

        PointCollection CreateColumn(List<double> points) {
            PointCollection pointCollection = new PointCollection();
            for(int i = 0; i < points.Count; i++) {
                pointCollection.Add(new Point((Step * i) + offset, MaxAvailableHeight));
                pointCollection.Add(new Point((Step * i) + offset, MaxAvailableHeight - points[i]/BarScale));
                pointCollection.Add(new Point((Step * i) + Step - offset, MaxAvailableHeight - points[i]/BarScale));
                pointCollection.Add(new Point((Step * i) + Step - offset, MaxAvailableHeight));
            }
            pointCollection.Add(new Point((Step * points.Count) + Step, MaxAvailableHeight));
            return pointCollection;
        }

        PointCollection CreateColumn(double point, int orderNumber) {
            PointCollection points = new PointCollection();
            
            points.Add(new Point((Step * orderNumber) + offset, MaxAvailableHeight));
            points.Add(new Point((Step * orderNumber) + offset, point));
            points.Add(new Point((Step * orderNumber) + Step - offset, point));
            points.Add(new Point((Step * orderNumber) + Step - offset, MaxAvailableHeight));
            if(orderNumber == 11)
                points.Add(new Point((Step * orderNumber) + Step, MaxAvailableHeight));
            return points;
        }
        protected internal override Point GetStartPoint() {
            return new Point(offset, MaxAvailableHeight);
        }
        protected internal override double GetStep(Size availableSize) {
            return availableSize.Width / (Points.Count);
        }
        protected internal override void CreateStartEndFigure(PathGeometry geom) {
        }                
    }

    public class DemoSparkAreaChart : DemoSparkLineChart {
        public static readonly DependencyProperty VerticalChartIndentProperty = DependencyProperty.Register("VerticalChartIndent", typeof(double), typeof(DemoSparkAreaChart), new PropertyMetadata(0.0, (d, e) => ((DemoSparkAreaChart)d).OnVerticalChartIndentChanged()));
        
        public double VerticalChartIndent {
            get { return (double)GetValue(VerticalChartIndentProperty); }
            set { SetValue(VerticalChartIndentProperty, value); }
        }
        Point CreatePointCore(double point, int orderNumber, bool useIndent = true) {
            double indent = useIndent ? VerticalChartIndent : 0;
            return new Point(Step * orderNumber, MaxAvailableHeight - (point - MinValue) * scaleFactor - indent);
        }

        double minValue = Double.MaxValue;
        protected double MinValue {
            get {
                if(minValue == Double.MaxValue)
                    UpdateSupportValues();
                return minValue;
            }
        }
        double maxValue = Double.MinValue;
        protected double MaxValue {
            get {
                if(maxValue == Double.MinValue)
                    UpdateSupportValues();
                return maxValue;
            }
        }
        double scaleFactor = Double.NaN;
        protected double ScaleFactor {
            get {
                if(scaleFactor == Double.NaN)
                    UpdateSupportValues();
                return scaleFactor;
            }
        }

        void UpdateSupportValues() {
            if(Points.Count == 0)
                return;
            foreach(double point in Points) {
                if(point < minValue) minValue = point;
                if(point > maxValue) maxValue = point;
            }
            double delta = MaxValue - MinValue;
            scaleFactor = (MaxAvailableHeight - VerticalChartIndent) / delta;
        }
        void ClearSupportValues() {
            minValue = Double.MaxValue;
            maxValue = Double.MinValue;
            scaleFactor = Double.NaN;
        }

        protected internal override Point GetStartPoint() {
            Point point = CreatePointCore(MinValue, 0, false);
            return new Point(point.X, point.Y);
        }

        protected internal override Point GetEndPoint() {
            Point point = CreatePointCore(MinValue, Points.Count - 1, false);
            return new Point(point.X, point.Y);
        }
        protected internal override PathSegmentCollection GenerateSegments(List<double> points) {
            PathSegmentCollection segments = new PathSegmentCollection();
            for(int i = 0; i < points.Count; i++) {
                segments.Add(new LineSegment() { Point = CreatePointCore(points[i], i) });
            }
            segments.Add(new LineSegment() { Point = GetEndPoint() });
            return segments;
        }

        protected override void UpdateData() {
            ClearSupportValues();
            base.UpdateData();
        }

        void OnVerticalChartIndentChanged() {
            UpdateData();
        }
    }

    public class DemoSparkLineChart : ContentControl {
        double _scale = 20;

        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register("Points", typeof(List<double>), typeof(DemoSparkLineChart), new PropertyMetadata(new List<double>(), (d, e) => ((DemoSparkLineChart)d).OnPointsChanged()));
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(DemoSparkLineChart), new PropertyMetadata(null, (d, e) => ((DemoSparkLineChart)d).OnFillChanged()));
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(DemoSparkLineChart), new PropertyMetadata(null, (d, e) => ((DemoSparkLineChart)d).OnStrokeChanged()));
        
        public DemoSparkLineChart() {
            DefaultStyleKey = typeof(DemoSparkLineChart);
            PathCore = new Path() { Stroke = new SolidColorBrush(Colors.Red), StrokeThickness = 1, UseLayoutRounding = false };
            Content = PathCore;
            Step = 10;
        }
    
        protected internal double MaxAvailableHeight { get; private set; }
        public double BarScale {
            get { return _scale; }
            set {
                if(_scale == value)
                    return;
                _scale = value;
                InvalidateMeasure();
            }
        }
        internal double Step { get; set; }
        public double StrokeThickness { get { return PathCore.StrokeThickness; } set { PathCore.StrokeThickness = value; } }
        protected internal Path PathCore { get; set; }
        public Brush Fill {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public List<double> Points {
            get { return (List<double>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        public Brush Stroke {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }
        protected virtual Point CreatePoint(double point, int orderNumber) {
            return new Point(Step * orderNumber, MaxAvailableHeight - (point / BarScale));
        }
        protected internal virtual PathSegmentCollection GenerateSegments(List<double> points) {
            PathSegmentCollection segments = new PathSegmentCollection();
            for(int i = 1; i < points.Count - 1; i++) {
                segments.Add(new LineSegment() { Point = CreatePoint(points[i], i) });
            }
            segments.Add(new LineSegment() { Point = GetEndPoint() });
            return segments;
        }
        protected internal virtual Point GetStartPoint() {
            if(Points.Count == 0)
                return new Point(0, 0);
            Point point = CreatePoint(Points[0], 0);
            return new Point(point.X + (Step / 2), point.Y);
        }
         protected internal virtual Point GetEndPoint() {
            if(Points.Count == 0)
                return new Point(0, 0);
            Point point = CreatePoint(Points[Points.Count - 1], Points.Count - 1);
            return new Point(point.X - (Step / 2), point.Y);
        }
        protected internal virtual double GetStep(Size availableSize) {
            return Points.Count == 0 ? 0 : availableSize.Width / (Points.Count - 1);
        }
        void UpdateContent(List<Double> points, Size availableSize) {
            if(points == null || points.Count == 0 || MaxAvailableHeight == 0)
                return;
            if(availableSize.Width != 0 && !Double.IsInfinity(availableSize.Width)) {
                Step = GetStep(availableSize);
            }            
        }
        protected virtual void UpdateData() {
            PathFigure figure = new PathFigure() { StartPoint = GetStartPoint() };
            figure.Segments = GenerateSegments(Points);

            PathGeometry geom = new PathGeometry();
            geom.Figures.Add(figure);
            CreateStartEndFigure(geom);
            PathCore.Data = geom;            
        }
        protected internal virtual void CreateStartEndFigure(PathGeometry geom) {
            Point point = GetStartPoint();
            PathFigure figure = new PathFigure() { StartPoint = new Point(point.X - (StrokeThickness / 2), point.Y - (StrokeThickness / 2)) };
            figure.Segments.Add(new ArcSegment() { Size = new Size(1, 1), SweepDirection = SweepDirection.Clockwise, Point = new Point(figure.StartPoint.X + StrokeThickness, figure.StartPoint.Y + StrokeThickness) });
            figure.Segments.Add(new ArcSegment() { Size = new Size(1, 1), SweepDirection = SweepDirection.Clockwise, Point = figure.StartPoint });
            geom.Figures.Add(figure);

            point = GetEndPoint();
            figure = new PathFigure() { StartPoint = new Point(point.X - (StrokeThickness / 2), point.Y - (StrokeThickness / 2)) };
            figure.Segments.Add(new ArcSegment() { Size = new Size(1, 1), SweepDirection = SweepDirection.Clockwise, Point = new Point(figure.StartPoint.X + StrokeThickness, figure.StartPoint.Y + StrokeThickness) });
            figure.Segments.Add(new ArcSegment() { Size = new Size(1, 1), SweepDirection = SweepDirection.Clockwise, Point = figure.StartPoint });
            geom.Figures.Add(figure);
        }

        protected override Size MeasureOverride(Size availableSize) {
            bool allowUpdateData = MaxAvailableHeight == 0;
            MaxAvailableHeight = availableSize.Height;
            if(Double.IsInfinity(MaxAvailableHeight))
                MaxAvailableHeight = GetMaxValue();
            UpdateContent(Points, availableSize);
            if(allowUpdateData)
                UpdateData();
            return base.MeasureOverride(availableSize);
        }
        double GetMaxValue() {
            double maxValue = 0;
            for(int i = 0; i < Points.Count; i++) {
                if(maxValue < Points[i])
                    maxValue = Points[i];
            }
            return (maxValue / BarScale) + 1;
        }
        protected internal virtual void OnPointsChanged() {
            UpdateData();
        }
        void OnFillChanged() {
            PathCore.Fill = Fill;
        }
        void OnStrokeChanged() {
            PathCore.Stroke = Stroke;
        }
    }
}
