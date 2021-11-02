using DevExpress.WinUI.Core.Internal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Foundation;

namespace FeatureDemo.Common {
    public class OptionsPanel : DXPanelBase {
        protected Thickness Padding { get; set; } = new Thickness(16);
        static double GroupSpacing = 16;
        static double ItemSpacing = 8;

        protected override Size MeasureCore(Size s) {
            return Layout(s, true, false);
        }
        protected override Size ArrangeCore(Size s) {
            Layout(s, false, true);
            return s;
        }
        Size Layout(Size s, bool measure, bool arrange) {
            if(Children.Count == 0) return new Size();
            double rw = 0;
            double rh = 0;
            bool isFirstChild = true;
            bool isLastChild = false;
            bool prevItemIsGroup = false;
            bool prevItemIsItem = false;
            for(int i = 0; i < Children.Count; i++) {
                var child = Children[i];
                isLastChild = i == Children.Count - 1;

                var margin = child is OptionsHeader
                    ? new Thickness(
                        0, 
                        isFirstChild ? 0 : GroupSpacing, 
                        0,
                        isLastChild ? GroupSpacing : 0)
                    : new Thickness(
                        Padding.Left,
                        isFirstChild 
                            ? Padding.Top 
                            : prevItemIsGroup 
                            ? GroupSpacing
                            : child is OptionItem || prevItemIsItem
                            ? ItemSpacing
                            : 0,
                        Padding.Right,
                        isLastChild ? Padding.Bottom : 0);

                prevItemIsGroup = child is OptionsHeader;
                prevItemIsItem = child is OptionItem;

                Size ds = new Size();
                if(measure) {
                    ds = Measure(child, new Size(s.Width, double.PositiveInfinity), margin);
                }
                if(arrange) {
                    ds = Arrange(child, new Rect(
                        new Point(0, rh), 
                        new Size(s.Width, child.DesiredSize.Height + margin.Top + margin.Bottom)), 
                        margin);
                }
                rw = Math.Max(rw, ds.Width);
                rh += ds.Height;

                isFirstChild = false;
            }
            return new Size(rw, rh);
        }

        static Size AddThickness(Size s, Thickness t) {
            var h = t.Left + t.Right;
            var v = t.Top + t.Bottom;
            return new Size(Math.Max(0, s.Width + h), Math.Max(0, s.Height + v));
        }
        static Size SubtractThickness(Size s, Thickness t) {
            var h = t.Left + t.Right;
            var v = t.Top + t.Bottom;
            return new Size(Math.Max(0, s.Width - h), Math.Max(0, s.Height - v));
        }

        static Size Measure(UIElement child, Size s) {
            child.Measure(s);
            return child.DesiredSize;
        }
        static Size Measure(UIElement child, Size s, Thickness margin) {
            if(margin == default(Thickness))
                return Measure(child, s);
            var ds = Measure(child, SubtractThickness(s, margin));
            ds = AddThickness(ds, margin);
            return new Size(Math.Min(s.Width, ds.Width), Math.Min(s.Height, ds.Height));
        }
        static Size Arrange(UIElement child, Rect r) {
            child.Arrange(r);
            return r.Size();
        }
        static Size Arrange(UIElement child, Rect r, Thickness margin) {
            if(margin == default(Thickness))
                return Arrange(child, r);
            var p = new Point(r.X + margin.Left, r.Y + margin.Top);
            var s = SubtractThickness(r.Size(), margin);
            Arrange(child, new Rect(p, s));
            return r.Size();
        }
    }
    public class OptionsSubPanel : OptionsPanel {
        public OptionsSubPanel() {
            Margin = new Thickness(-16, 16, -16, 0);
            Padding = new Thickness(16, 0, 16, 0);
        }
    }
    public class OptionsHeader : Control {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(OptionsHeader), new PropertyMetadata(null));
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public OptionsHeader() {
            DefaultStyleKey = typeof(OptionsHeader);
        }
    }
    public class OptionItem : ContentControl {
        #region static
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(string), typeof(OptionItem), new PropertyMetadata(null));
        public static readonly DependencyProperty IsHorizontalProperty = DependencyProperty.Register(nameof(IsHorizontal), typeof(bool), typeof(OptionItem), new PropertyMetadata(false, (d, e) => ((OptionItem)d).UpdateVisualState()));
        public static readonly DependencyProperty HeaderWidthProperty = DependencyProperty.Register(nameof(HeaderWidth), typeof(double), typeof(OptionItem), new PropertyMetadata(double.NaN));
        #endregion
        #region dep props
        public string Header {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public double HeaderWidth {
            get { return (double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }
        public bool IsHorizontal {
            get { return (bool)GetValue(IsHorizontalProperty); }
            set { SetValue(IsHorizontalProperty, value); }
        }
        #endregion
        public OptionItem() {
            DefaultStyleKey = typeof(OptionItem);
        }
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            UpdateVisualState();
        }
        void UpdateVisualState() => VisualStateManager.GoToState(this, IsHorizontal ? "Horizontal" : "Vertical", false);
    }
}
