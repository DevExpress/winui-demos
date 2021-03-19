using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using ICommand = Microsoft.UI.Xaml.Input.ICommand;

namespace RangeControlDemo {
    public class RangeControlDemoModuleViewModel : ViewModelBase {
        public ICommand SetViewIndexCommand { get; }
        public object SelectedView {
            get => GetProperty<object>();
            private set => SetProperty<object>(value);
        }
        public int SelectedViewIndex {
            get => GetProperty<int>();
            set => SetProperty(value, OnSelectedViewModelIndexChanged);
        }

        private void OnSelectedViewModelIndexChanged() => SelectedView = Views[SelectedViewIndex];

        public List<object> Views { get; }
        public RangeControlDemoModuleViewModel() {
            SetViewIndexCommand = new DelegateCommand<int>(OnSetViewIndexCommandExecuted);
            Views = new List<object>() {
                new CalendarViewModel(),
                new SparkPointViewModel(0, 1000, TimeSpan.FromHours(6)),
                new SparkLineViewModel(0, 5000, TimeSpan.FromHours(2)),
                new SparkAreaViewModel(10000, 5000, TimeSpan.FromHours(2)),
                new SparkBarViewModel(10000, 1000, TimeSpan.FromHours(6))
            };
            SelectedView = Views[0];
        }

        private void OnSetViewIndexCommandExecuted(int index) => SelectedViewIndex = index;
    }

    public class CalendarViewModel : ViewModelBase {
        public DateTime SelectionStart {
            get => GetProperty<DateTime>();
            set => SetProperty(value);
        }
        public DateTime SelectionEnd {
            get => GetProperty<DateTime>();
            set => SetProperty(value);
        }
        public CalendarViewModel() {
            SelectionStart = new DateTime(DateTime.Now.Year - 1, 1, 1);
            SelectionEnd = SelectionStart.AddYears(1);
        }
    }
    public class SparkPointViewModel : RangeControlViewModel {
        public SparkPointViewModel(double firstPointValue, int pointCount, TimeSpan step) : base(firstPointValue, pointCount, step) { }
    }
    public class SparkLineViewModel : RangeControlViewModel {
        public SparkLineViewModel(double firstPointValue, int pointCount, TimeSpan step) : base(firstPointValue, pointCount, step) { }
    }
    public class SparkAreaViewModel : RangeControlViewModel {
        public SparkAreaViewModel(double firstPointValue, int pointCount, TimeSpan step) : base(firstPointValue, pointCount, step) { }
    }
    public class SparkBarViewModel : RangeControlViewModel {
        public SparkBarViewModel(double firstPointValue, int pointCount, TimeSpan step) : base(firstPointValue, pointCount, step) { }
    }

}