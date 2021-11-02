using DevExpress.Data.Filtering;
using DevExpress.WinUI.Grid;
using Microsoft.UI.Xaml.Data;
using System;

namespace GridDemo {
    public sealed partial class VirtualSourcesModule : GridDemoUserControl {
        public VirtualSourcesViewModel ViewModel { get; } = new VirtualSourcesViewModel();
        public VirtualSourcesModule() {
            this.InitializeComponent();
        }

        protected internal override GridControl Grid => throw new NotImplementedException();
    }
    public class OperandValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if(value is OperandValue)
                return (value as OperandValue).Value;

            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            if(value is OperandValue)
                return (value as OperandValue).Value;
            return value;
        }
    }
}
