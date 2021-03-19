using System;
using DevExpress.WinUI.Core;
using DevExpress.WinUI.Editors;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using DevExpress.WinUI.Scheduler;
using DevExpress.WinUI.Core.Internal;

namespace FeatureDemo.Common {
    public class CalendarSelectionModeComboBox : EnumComboBox<CalendarSelectionMode> { }
    public class StretchComboBox : EnumComboBox<Stretch> { }
    public class DoubleComboBox : ComboBox {
        public DoubleComboBox() {
            ItemsSource = new double[] { double.NaN, 10d, 20d, 30d, 50d, 100d, 150d, 200d };
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            ((ComboBoxItem)element).Content = item.ToString();
            ((ComboBoxItem)element).DataContext = item;


        }
    }
    [Bindable]
    public abstract class EnumComboBox<TEnum> : ComboBox {
        public EnumComboBox() {
            ItemsSource = Enum.GetValues(typeof(TEnum));
        }
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            base.PrepareContainerForItemOverride(element, item);
            ((ComboBoxItem)element).Content = item.ToString();
            ((ComboBoxItem)element).DataContext = item;
        }
    }
}
