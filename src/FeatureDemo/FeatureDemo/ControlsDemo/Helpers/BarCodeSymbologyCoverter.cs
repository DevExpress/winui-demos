using DevExpress.WinUI.Controls;
using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Data;
using DevExpress.WinUI.Controls.Internal;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeSymbologyCoverter : IValueConverter {
        Dictionary<BarCodeSymbology, SymbologyBase> symbologies = new Dictionary<BarCodeSymbology, SymbologyBase>();
        public object Convert(object value, Type targetType, object parameter, string language) {
            var symbologyValue = (BarCodeSymbology)value;
            if(!symbologies.ContainsKey(symbologyValue)) {
                symbologies.Add(symbologyValue, BarCodeSymbologyStorage.Create(symbologyValue));
            }
            return symbologies[symbologyValue];
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return ((SymbologyBase)value).GeneratorBase.SymbologyCode;
        }
    }
}
