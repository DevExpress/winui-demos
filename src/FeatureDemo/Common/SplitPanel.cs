
using System;
using System.Linq;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System.Collections.Generic;
using DevExpress.WinUI.Core.Internal;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using CoreCursor = Windows.UI.Core.CoreCursor;
using CoreCursorType = Windows.UI.Core.CoreCursorType;
namespace FeatureDemo.Common {

    public class ObservableUIElementCollection : ObservableCollectionView<UIElement> { }

    [ContentProperty(Name = "Items")]
    public class SplitPanel : Panel {
        public static readonly DependencyProperty SplitterWidthProperty;
        public static readonly DependencyProperty MinItemWidthProperty;
        public static readonly DependencyProperty SplitterStyleProperty;
        public static readonly DependencyProperty IsLayoutStaticProperty;

        static SplitPanel() {
            new DependencyPropertyRegistrator<SplitPanel>()
		        .Register<bool>(nameof(IsLayoutStatic), out IsLayoutStaticProperty, false)
                .Register<double>(nameof(SplitterWidth), out SplitterWidthProperty, 10d)
                .Register<double>(nameof(MinItemWidth), out MinItemWidthProperty, 1d)
		        .Register<Style>(nameof(SplitterStyle), out SplitterStyleProperty, null)
        ;
        }

        public bool IsLayoutStatic {
            get { return (bool)GetValue(IsLayoutStaticProperty); }
            set { SetValue(IsLayoutStaticProperty, value); }
        }
        public Style SplitterStyle {
            get { return (Style)GetValue(SplitterStyleProperty); }
            set { SetValue(SplitterStyleProperty, value); }
        }
        public double MinItemWidth {
            get { return (double)GetValue(MinItemWidthProperty); }
            set { SetValue(MinItemWidthProperty, value); }
        }
        public double SplitterWidth {
            get { return (double)GetValue(SplitterWidthProperty); }
            set { SetValue(SplitterWidthProperty, value); }
        }
        public ObservableUIElementCollection Items { get; protected set; }

        List<double> Proportions { get; set; } = new List<double>();
        List<Thumb> Splitters { get; set; } = new List<Thumb>();
        public SplitPanel() {
            Items = new ObservableUIElementCollection();
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            Children.Clear();
            UpdateChildren();
            InvalidateMeasure();
        }

        void UpdateChildren() {
            Proportions.Clear();
            Splitters.Clear();
            if (Items.Count > 0) {
                for (int i = 0; i < Items.Count - 1; i++) {
                    Children.Add(Items[i]);
                    var thumb = GenerateSplitThumb();
                    Splitters.Add(thumb);
                    Children.Add(thumb);
                    Proportions.Add(1d);
                }
                Children.Add(Items.Last());
                Proportions.Add(1d);
            }
        }

        Thumb GenerateSplitThumb() {
            Thumb result = new Thumb();
            if(SplitterStyle != null) result.Style = SplitterStyle;
            result.DragDelta += ThumbDragDelta;
            result.DragStarted += ThumbDragStarted;
            result.PointerEntered += ThumbPointerEntered;
            result.PointerExited += ThumbPointerExited;
            result.PointerPressed += ThumbPointerPressed;
            result.PointerReleased += ThumbPointerReleased;
            result.SetBinding(WidthProperty, new Binding() { Source = this, Path = new PropertyPath(nameof(SplitterWidth)) });
            return result;
        }

        CoreCursor arrowCursor = new CoreCursor(CoreCursorType.Arrow, 1);
        CoreCursor resizeCursor = new CoreCursor(CoreCursorType.SizeWestEast, 1);

        void ThumbPointerReleased(object sender, PointerRoutedEventArgs e) {
            (sender as FrameworkElement).ReleasePointerCaptures();
            Window.Current.CoreWindow.PointerCursor = arrowCursor;
        }
        void ThumbPointerPressed(object sender, PointerRoutedEventArgs e) {
            (sender as FrameworkElement).CapturePointer(e.Pointer);
            Window.Current.CoreWindow.PointerCursor = resizeCursor;
        }
        void ThumbPointerExited(object sender, PointerRoutedEventArgs e) {
            if ((sender as FrameworkElement).PointerCaptures == null || (sender as FrameworkElement).PointerCaptures.Count == 0)
                Window.Current.CoreWindow.PointerCursor = arrowCursor;
        }
        void ThumbPointerEntered(object sender, PointerRoutedEventArgs e) {
            if ((sender as FrameworkElement).PointerCaptures == null || (sender as FrameworkElement).PointerCaptures.Count == 0)
                Window.Current.CoreWindow.PointerCursor = resizeCursor;
        }

        double siblingProportionSumm = 0;

        void ThumbDragStarted(object sender, DragStartedEventArgs e) {
            if (sender != null && sender as Thumb != null) {
                var thumb = sender as Thumb;
                int index = Children.IndexOf(thumb);
                int prevIndex = (index - 1) / 2;
                int nextIndex = (index + 1) / 2;
                siblingProportionSumm = Proportions[prevIndex] + Proportions[nextIndex];
            }
        }

        void ThumbDragDelta(object sender, DragDeltaEventArgs e) {
            if (sender != null && sender as Thumb != null) {
                var thumb = sender as Thumb;
                int index = Children.IndexOf(thumb);
                int prevIndex = (index - 1) / 2;
                int nextIndex = (index + 1) / 2;
                double pxProp = Proportions.Count / ActualWidth;
                double proportionalDelta = e.HorizontalChange * pxProp;
                double minProp = pxProp * MinItemWidth;
                Proportions[prevIndex] = Math.Max(minProp, Math.Min(siblingProportionSumm - minProp, Proportions[prevIndex] + proportionalDelta));
                Proportions[nextIndex] = Math.Max(minProp, siblingProportionSumm - Proportions[prevIndex]);
                InvalidateMeasure();
            }
        }
        Dictionary<UIElement, double> ArrangeWidths = new Dictionary<UIElement, double>();
        protected override Size MeasureOverride(Size availableSize) {
            double maxDesiredHeight = 0;
            double splittersWidth = 0;
            ArrangeWidths.Clear();
            foreach(Thumb thumb in Splitters) {
                thumb.Measure(availableSize);
                splittersWidth += thumb.DesiredSize.Width;
                ArrangeWidths.Add(thumb, thumb.DesiredSize.Width);
                if(thumb.DesiredSize.Height > maxDesiredHeight) maxDesiredHeight = thumb.DesiredSize.Height;
            }
            int index = 0;
            double itemsWidth = 0;
            foreach(UIElement item in Items) {
                Size itemAvailableSize = availableSize;
                if(!double.IsPositiveInfinity(availableSize.Width)) {
                    double itemWidth = Math.Min(availableSize.Width - splittersWidth, Math.Max(MinItemWidth, Proportions[index] * (availableSize.Width - splittersWidth) / Items.Count));
                    itemAvailableSize = new Size(itemWidth, availableSize.Height);
                }
                if(IsLayoutStatic) {
                    item.Measure(availableSize);
                    itemsWidth += itemAvailableSize.Width;
                    ArrangeWidths.Add(item, itemAvailableSize.Width);
                } else {
                    item.Measure(itemAvailableSize);
                    double arrangeItemWidth = double.IsPositiveInfinity(itemAvailableSize.Width) ? item.DesiredSize.Width : Math.Max(item.DesiredSize.Width, itemAvailableSize.Width);
                    itemsWidth += arrangeItemWidth;
                    ArrangeWidths.Add(item, arrangeItemWidth);
                }
                if(item.DesiredSize.Height > maxDesiredHeight) maxDesiredHeight = item.DesiredSize.Height;
                index++;
            }
            return new Size(splittersWidth + itemsWidth, maxDesiredHeight);
        }
        protected override Size ArrangeOverride(Size finalSize) {
            double offset = 0;
            foreach(var child in Children) {
                if(IsLayoutStatic && !(child is Thumb)) {
                    child.Clip = new Microsoft.UI.Xaml.Media.RectangleGeometry() {
                        Rect = new Rect(offset, 0, ArrangeWidths[child], finalSize.Height)
                    };
                    child.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
                } else {
                    child.Arrange(new Rect(offset, 0, ArrangeWidths[child], finalSize.Height));
                }
                offset += ArrangeWidths[child];
            }
            return base.ArrangeOverride(finalSize);
        }
    }
}