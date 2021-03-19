using FeatureDemo.Common;
using Microsoft.UI.Xaml.Data;
using System;
using Windows.UI.Text;

namespace EditorsDemo {
    public sealed partial class ButtonEditModule : DemoModuleView {
        public ButtonEditModule() {
            this.InitializeComponent();
        }
    }
    public class BoolToBoldConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) => (bool)value ? FontWeights.Bold : FontWeights.Normal;

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
    public class BoolToItalicConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) => (bool)value ? FontStyle.Italic : FontStyle.Normal;

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
