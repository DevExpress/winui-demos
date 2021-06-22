using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core.Internal;
using FeatureDemo.View;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

namespace FeatureDemo.Common {
    public class ProductGroupPanel : DXPanel {
        #region static
        public static readonly DependencyProperty ColSpaceProperty;
        public static readonly DependencyProperty RowSpaceProperty;
        public static readonly DependencyProperty ColCountProperty;


        public object ItemsSource {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(ProductGroupPanel), new PropertyMetadata(null, OnItemsSourcePropertyChanged));

        static ProductGroupPanel() {
            ColSpaceProperty = DependencyProperty.Register(nameof(ColSpace), typeof(double), typeof(ProductGroupPanel), new PropertyMetadata(0d, OnPropertyChanged));
            RowSpaceProperty = DependencyProperty.Register(nameof(RowSpace), typeof(double), typeof(ProductGroupPanel), new PropertyMetadata(0d, OnPropertyChanged));
            ColCountProperty = DependencyProperty.Register(nameof(ColCount), typeof(int), typeof(ProductGroupPanel), new PropertyMetadata(1, OnPropertyChanged));
        }
        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ProductGroupPanel)d).Controller.ItemsSource = e.NewValue;
        }
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((ProductGroupPanel)d).InvalidateMeasure();
        }

        protected static double CalcItemWidth(double width, int colCount, double space) {
            if(colCount == 0) return 0;
            return Math.Max(0, Math.Round((width - (colCount - 1) * space) / colCount));
        }

        #endregion
        #region dep props
        public double ColSpace {
            get { return (double)GetValue(ColSpaceProperty); }
            set { SetValue(ColSpaceProperty, value); }
        }
        public double RowSpace {
            get { return (double)GetValue(RowSpaceProperty); }
            set { SetValue(RowSpaceProperty, value); }
        }
        public int ColCount {
            get { return (int)GetValue(ColCountProperty); }
            set { SetValue(ColCountProperty, value); }
        }
        #endregion
        DataTemplate itemTemplate;
        public DataTemplate ItemTemplate {
            get => itemTemplate;
            set => PropertySetterHelper.SetProperty(ref itemTemplate, value, () => Controller.ItemTemplate = itemTemplate);
        }
        ItemsSourceController<UIElement, ProductGroupPanel> Controller { get; }
        public ProductGroupPanel() {
            Controller = new ItemsSourceController<UIElement, ProductGroupPanel>(this, x => new ContentControl() { Content = x, ContentTemplate = ItemTemplate });
            Controller.Containers.CollectionChanged += Containers_CollectionChanged;
        }

        private void Containers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            foreach(var newItem in e.NewItems.OfType<ItemsSourceControllerContainerInfo>()) {
                Children.Add((UIElement)newItem.Container);
            }
        }

        List<Rect> ItemRects { get; set; }
        protected override Size MeasureOverride(Size availableSize) {
            if(Children.Count == 0)
                return new Size();
            ItemRects = new List<Rect>();
            double y = 0;
            double itemWidth = CalcItemWidth(availableSize.Width, ColCount, ColSpace);

            double rowHeight = 0;
            int colIndex = 0;

            for(int i = 0; i < Children.Count; i++) {
                var element = Children[i];
                element.Measure(new Size(itemWidth, double.PositiveInfinity));
                rowHeight = Math.Max(element.DesiredSize.Height, rowHeight);
                colIndex++;
                if(colIndex >= ColCount || i == Children.Count - 1) {
                    double x = 0;
                    for(int c = 0; c < ColCount; c++) {
                        ItemRects.Add(new Rect() { X = x, Y = y, Width = itemWidth, Height = rowHeight });
                        x += ColSpace + itemWidth;
                    }

                    if(i != Children.Count - 1) {
                        y += RowSpace;
                    }
                    y += rowHeight;

                    colIndex = 0;
                    rowHeight = 0;
                }
            }

            return new Size(availableSize.Width, y);
        }

        protected override Size ArrangeOverride(Size finalSize) {
            if(Children.Count == 0)
                return finalSize;
            for(int i = 0; i < Children.Count; i++) {
                Children[i].Arrange(ItemRects[i]);
            }
            return finalSize;
        }
    }
    public class UniformPanel : DXPanel {
        #region static
        public static readonly DependencyProperty SpaceProperty;
        public static readonly DependencyProperty MaxItemWidthProperty;

        static UniformPanel() {
            SpaceProperty = DependencyProperty.Register(nameof(Space), typeof(double), typeof(UniformPanel), new PropertyMetadata(0d));
            MaxItemWidthProperty = DependencyProperty.Register(nameof(MaxItemWidth), typeof(double), typeof(UniformPanel), new PropertyMetadata(0d));
        }
        protected static double CalcRealSize(double width, int colCount, double space) {
            if(colCount == 0) return 0;
            return Math.Round((width - (colCount - 1) * space) / colCount);
        }
        #endregion
        #region dep props
        public double Space {
            get { return (double)GetValue(SpaceProperty); }
            set { SetValue(SpaceProperty, value); }
        }
        public double MaxItemWidth {
            get { return (double)GetValue(MaxItemWidthProperty); }
            set { SetValue(MaxItemWidthProperty, value); }
        }
        #endregion

        protected double itemWidth = 0d;
        protected double itemHeight = 0d;
        protected int rowCount = 0;
        protected override Size MeasureOverride(Size availableSize) {
            if(Children.Count == 0)
                return new Size();
            int visibleItemCount = Children.Count(x => x.Visibility == Visibility.Visible);

            double minCount = (availableSize.Width + Space) / (MaxItemWidth + Space);
            int colCount = (int)minCount;
            if(minCount - Math.Truncate(minCount) != 0d) {
                colCount++;
            }
            colCount = Math.Min(visibleItemCount, colCount);
            double width = Math.Min(MaxItemWidth, Math.Truncate((availableSize.Width - (colCount - 1) * Space) / colCount));
            double height = 0;

            foreach(UIElement element in Children) {
                element.Measure(new Size(width, double.PositiveInfinity));
                if(element.Visibility == Visibility.Collapsed)
                    continue;
                height = Math.Max(height, element.DesiredSize.Height);
            }
            if(visibleItemCount == 0)
                return new Size(0, 0);
            rowCount = visibleItemCount / colCount + ((visibleItemCount % colCount) == 0 ? 0 : 1);
            itemWidth = width;
            itemHeight = height;
            return new Size(colCount * width + Space * (colCount - 1), rowCount * height + Space * (rowCount - 1));
        }

        protected override Size ArrangeOverride(Size finalSize) {
            Rect arrangeRect = new Rect(0, 0, itemWidth, itemHeight);
            foreach(UIElement element in Children.Where(x => x.Visibility == Visibility.Visible)) {
                element.Arrange(arrangeRect);
                arrangeRect.X += itemWidth + Space;
                if(arrangeRect.X >= finalSize.Width) {
                    arrangeRect.X = 0;
                    arrangeRect.Y += itemHeight + Space;
                }
            }
            return finalSize;
        }
    }
    public class UniformItemsPanel : UniformPanel, IScrollSnapPointsInfo {
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty ItemContainerStyleProperty;

        static UniformItemsPanel() {
            ItemContainerStyleProperty = DependencyProperty.Register(nameof(ItemContainerStyle), typeof(Style), typeof(UniformItemsPanel), new PropertyMetadata(null));
            ItemsSourceProperty = DependencyProperty.Register(nameof(ItemsSource),typeof(object), typeof(UniformItemsPanel), new PropertyMetadata(null, (d, e) => ((UniformItemsPanel)d).OnItemsSourceChanged()));
        }

        public object ItemsSource {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public Style ItemContainerStyle {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        ItemsSourceController<FrameworkElement, UniformItemsPanel> controller;

        public UniformItemsPanel() {
            controller = new ItemsSourceController<FrameworkElement, UniformItemsPanel>(this, (o) => {
                var container = CreateContainer(o);
                container.DataContext = o;
                if(ItemContainerStyle != null) container.Style = ItemContainerStyle;
                Children.Add(container);
                return container;
            });
            Loading += OnLoading;
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
            VerticalSnapPointsChanged?.Invoke(this, null);
        }

        private void OnLoading(FrameworkElement sender, object args) {
            scroll = this.VisualParents().OfType<ScrollViewer>().FirstOrDefault();
            scroll.Do(x => x.SizeChanged += OnScrollViewerSizeChanged);
        }

        private void OnScrollViewerSizeChanged(object sender, SizeChangedEventArgs e) {
            VerticalSnapPointsChanged?.Invoke(this, null);
        }

        public virtual FrameworkElement CreateContainer(object item) {
            var container = new DemoModuleTile();
            container.Click += Container_Click;
            return container;
        }
        ScrollViewer scroll = null;
        int firstVisibleRow = 0;
        protected override Size MeasureOverride(Size availableSize) {
            var result = base.MeasureOverride(availableSize);
            if(scroll != null) 
                firstVisibleRow = Math.Max(0, (int)Math.Round((scroll.VerticalOffset - this.Margin.Top) / (itemHeight + Space)));
            return result;
        }

        private void Container_Click(object sender, RoutedEventArgs e) {
            ItemClick?.Invoke(this, (sender as FrameworkElement).DataContext);
        }

        public event EventHandler<object> ItemClick;

        protected virtual void OnItemsSourceChanged() {
            controller.ItemsSource = ItemsSource;
        }
        #region IScrollSnapPointsInfo
        public bool AreHorizontalSnapPointsRegular { get { return true; } }

        public bool AreVerticalSnapPointsRegular { get { return false; } }
        List<float> snapPoints = new List<float>();


#pragma warning disable 0067
        public event EventHandler<object> HorizontalSnapPointsChanged;
#pragma warning restore 0067
        public event EventHandler<object> VerticalSnapPointsChanged;
        public IReadOnlyList<float> GetIrregularSnapPoints(Orientation orientation, SnapPointsAlignment alignment) {
            if(scroll == null) return new List<float>();
            int lastCount = snapPoints.Count;
            snapPoints.Clear();
            snapPoints.Add(0);
            float snapPoint = 0;
            for(int i = 1; i < rowCount; i++) {
                snapPoint = (float)(i * (itemHeight + Space) + Margin.Top - 0.5 * Space);
                if(snapPoint >= scroll.ScrollableHeight) {
                    snapPoint = (float)scroll.ScrollableHeight;
                    snapPoints.Add(snapPoint);
                    break;
                } else {
                    snapPoints.Add(snapPoint);
                }
            }
            if(snapPoint != scroll.ScrollableHeight)
                snapPoints.Add((float)scroll.ScrollableHeight);
            return snapPoints;
        }

        public float GetRegularSnapPoints(Orientation orientation, SnapPointsAlignment alignment, out float offset) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class ContentControlUniformPanel : UniformItemsPanel {
        public override FrameworkElement CreateContainer(object item) {
            return new ContentControl();
        }
    }
}