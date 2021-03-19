using DevExpress.WinUI.Core.Extensions;
using DevExpress.WinUI.Core.Internal;
using DevExpress.Mvvm.Native;
using FeatureDemo.View;
using FeatureDemo.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.Web;

namespace FeatureDemo.Common {
    public class VerticalStackPanel : Panel {
        public int Spacing {
            get { return (int)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register("Spacing", typeof(int), typeof(VerticalStackPanel), new PropertyMetadata(0));

        protected override Size MeasureOverride(Size availableSize) {
            if(Children.Count == 0) return base.MeasureOverride(availableSize);
            Size res = new Size(0, 0);
            foreach(var child in Children) {
                child.Measure(new Size(availableSize.Width, double.PositiveInfinity));
                res.Width = Math.Max(child.DesiredSize.Width, res.Width);
                if(res.Height != 0 && child.DesiredSize.Height != 0) {
                    res.Height += Spacing;
                }
                res.Height += child.DesiredSize.Height;
            }
            return res;
        }
        protected override Size ArrangeOverride(Size finalSize) {
            if(Children.Count == 0) return base.ArrangeOverride(finalSize);
            Rect finalRect = new Rect(0, 0, finalSize.Width, 0);
            foreach(var child in Children) {
                finalRect.Height = child.DesiredSize.Height;
                child.Arrange(finalRect);
                if(finalRect.Height != 0) {
                    finalRect.Y += finalRect.Height + Spacing;
                }
            }
            return finalSize;
        }
    }
    public class OptionGroupHeader : Control {
        #region static
        public static readonly DependencyProperty TextProperty;
        static OptionGroupHeader() {
            DependencyPropertyRegistrator<OptionGroupHeader>.New()
                .Register<FrameworkElement>(nameof(Text), out TextProperty, null);
        }
        #endregion
        #region dep props
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion
        public OptionGroupHeader() {
            DefaultStyleKey = typeof(OptionGroupHeader);
        }
    }
    public class OptionItem : ContentControl {
        #region static
        public static readonly DependencyProperty HeaderProperty;
        public static readonly DependencyProperty IsHorizontalProperty;
        public static readonly DependencyProperty HeaderWidthProperty;


        static OptionItem() {
            DependencyPropertyRegistrator<OptionItem>.New()
                .Register<string>(nameof(Header), out HeaderProperty, null)
                .Register<bool>(nameof(IsHorizontal), out IsHorizontalProperty, false, d => d.UpdateVisualState())
                .Register<double>(nameof(HeaderWidth), out HeaderWidthProperty, double.NaN);
        }
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
    public class OptionsPanel : ItemsControl {
        public OptionsPanel() {
            DefaultStyleKey = typeof(OptionsPanel);
        }
    }
    public class OptionGroup : ItemsControl {
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(OptionGroup), new PropertyMetadata(null));

        public string Header {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public OptionGroup() {
            DefaultStyleKey = typeof(OptionGroup);
        }
    }
    public class OptionGroupPanel : ItemsControl {
        public OptionGroupPanel() {
            DefaultStyleKey = typeof(OptionGroupPanel);
        }
    }
}
