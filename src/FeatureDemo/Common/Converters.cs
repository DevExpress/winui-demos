using System;
using FeatureDemo.Data;
using Microsoft.UI.Xaml.Data;
using System.Linq;
using DevExpress.WinUI.Data.Filtering;

namespace FeatureDemo.Common {
    public class OperandValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (value is OperandValue) 
                return (value as OperandValue).Value;
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            if (value is OperandValue)
                return (value as OperandValue).Value;
            return value;
        }
    }
    class InvoceToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if(!(value is Invoices)) {
                return value.ToString();
            }
            var invoice = value as Invoices;
            return string.Format("{0} {1}", invoice.OrderID, invoice.ProductName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
    class BooleanToNullableBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (bool?)(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value == null ? false : ((bool?)value).Value;
        }
    }
    class IdentityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value;
        }
    }
    class IntToDoubleConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            int intValue = (int)value;
            return System.Convert.ToDouble(intValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            double dValue = (double)value;
            return System.Convert.ToInt32(dValue);
        }
    }
    class ObjectToFloatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return (float?)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value;
        }
    }
    public class StringFormatConverter : IValueConverter {
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, string language) {
            try {
                string format = (parameter == null) ? null : parameter.ToString();
                if(String.IsNullOrEmpty(format)) {
                    format = "{0}";
                }
                return String.Format(format, value);
            } catch(Exception) {
                return Windows.UI.Xaml.DependencyProperty.UnsetValue;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
        #endregion
    }
    public class BarCodeEnumToListConveter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            Type type = Type.GetType((string)parameter);
            return Enum.GetValues(type).Cast<object>().Where(x => x.ToString() != "Binary").ToList();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
    public class EnumToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return value.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}