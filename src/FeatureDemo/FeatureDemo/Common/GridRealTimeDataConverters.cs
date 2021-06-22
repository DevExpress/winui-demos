using FeatureDemo.Data;
using DevExpress.WinUI.Charts;
using GridDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Color = Windows.UI.Color;

namespace FeatureDemo.Common {
    public class BooleanToAngleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (bool)value ? 180 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class BooleanToColorConverter : IValueConverter {
        internal static Color GreenColor = Color.FromArgb(255, 3, 176, 137);
        internal static Color RedColor = Color.FromArgb(255, 201, 5, 65);

        public object Convert(object value, Type targetType, object parameter, string language) {
            return new SolidColorBrush((bool)value ? GreenColor : RedColor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class StockPriceConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return String.Format("{0:.00}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class ArrowDirectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return System.Convert.ToDouble(value) > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
