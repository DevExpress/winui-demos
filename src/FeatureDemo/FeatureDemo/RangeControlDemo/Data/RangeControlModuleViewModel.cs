using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace RangeControlDemo {
    public class RangeControlDemoModuleViewModel : ViewModelBase {
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
        public ICommand SetViewIndexCommand { get; }
        public object SelectedView {
            get => GetValue<object>();
            private set => SetValue(value);
        }
        public int SelectedViewIndex {
            get => GetValue<int>();
            set => SetValue(value, OnSelectedViewModelIndexChanged);
        }
        public List<object> Views { get; }

        void OnSelectedViewModelIndexChanged() => SelectedView = Views[SelectedViewIndex];
        void OnSetViewIndexCommandExecuted(int index) => SelectedViewIndex = index;
    }

    public class CalendarViewModel : ViewModelBase {
        public DateTime SelectionStart {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public DateTime SelectionEnd {
            get => GetValue<DateTime>();
            set => SetValue(value);
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