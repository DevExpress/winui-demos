using DevExpress.WinUI.Controls;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeSymbologyWrapperConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return new SymbologyWrapper((SymbologyBase)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
    public class SymbologyWrapper : DependencyObject {
        public SymbologyBase Symbology {
            get { return (SymbologyBase)GetValue(SymbologyProperty); }
            set { SetValue(SymbologyProperty, value); }
        }
        public static readonly DependencyProperty SymbologyProperty =
            DependencyProperty.Register("Symbology", typeof(SymbologyBase), typeof(SymbologyWrapper), new PropertyMetadata(null));
        public SymbologyWrapper(SymbologyBase generator) {
            Symbology = generator;
        }
    }
}
