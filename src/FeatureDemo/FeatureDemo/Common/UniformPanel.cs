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
    public class ProductGroupPanel : DXPanelBase {
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
        protected override Size MeasureCore(Size availableSize) {
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

        protected override Size ArrangeCore(Size finalSize) {
            if(Children.Count == 0)
                return finalSize;
            for(int i = 0; i < Children.Count; i++) {
                Children[i].Arrange(ItemRects[i]);
            }
            return finalSize;
        }
    }
}