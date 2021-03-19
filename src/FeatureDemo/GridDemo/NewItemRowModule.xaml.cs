
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GridDemo {
    public class BoolToNewItemRowPosition : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            bool? isChecked = (bool?)value;
            if(isChecked.HasValue && isChecked.Value)
                return NewItemRowPosition.Top;
            else
                return NewItemRowPosition.Bottom;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public class ObservableCollectionConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            return new ObservableCollection<Item>((Items)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }

    public sealed partial class NewItemRowModule : GridDemoUserControl {
        public NewItemRowModule() {
            this.InitializeComponent();
        }

        protected internal override GridControl Grid { get { return grid; } }

        private void grid_AddingNewRow(object sender, AddingNewEventArgs e) {
            e.NewObject = new Item() {
                Priority = FeatureDemo.Data.Priority.Low,
                Status = FeatureDemo.Data.Status.New,
                CreatedDate = DateTime.Now,
                FixedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
