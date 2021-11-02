using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm.CodeGenerators;

namespace RangeControlDemo {
    [GenerateViewModel]
    public partial class RangeControlDemoModuleViewModel {
        public RangeControlDemoModuleViewModel() {
            Views = new List<object>() {
                new CalendarViewModel(),
                new SparkPointViewModel(0, 1000, TimeSpan.FromHours(6)),
                new SparkLineViewModel(0, 5000, TimeSpan.FromHours(2)),
                new SparkAreaViewModel(10000, 5000, TimeSpan.FromHours(2)),
                new SparkBarViewModel(10000, 1000, TimeSpan.FromHours(6))
            };
            SelectedView = Views[0];
        }
        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        public object _SelectedView;

        [GenerateProperty]
        int _SelectedViewIndex;
        void OnSelectedViewIndexChanged() => SelectedView = Views[SelectedViewIndex];

        public List<object> Views { get; }

        [GenerateCommand]
        void SetViewIndex(int index) => SelectedViewIndex = index;
    }

    [GenerateViewModel]
    public partial class CalendarViewModel {
        [GenerateProperty]
        DateTime _SelectionStart;

        [GenerateProperty]
        DateTime _SelectionEnd;

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