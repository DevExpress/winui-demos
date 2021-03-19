using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core.Extensions;
using DevExpress.WinUI.Core.Internal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo {
    public sealed class ImageVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) => value?.ToString() == parameter?.ToString() ? Visibility.Visible : Visibility.Collapsed;
        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
