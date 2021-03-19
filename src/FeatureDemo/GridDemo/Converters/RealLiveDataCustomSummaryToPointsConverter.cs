using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Data;

namespace GridDemo {
    public class RealLiveDataCustomSummaryToPointsConverter : IValueConverter {        
        public object Convert(object value, Type targetType, object parameter, string language) {
            if(value == null) return new List<double>();
            return (parameter is string && (string)parameter == "Positive") ? ((RealLiveDataCustomSummary)value).Positive : ((RealLiveDataCustomSummary)value).Negative;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}