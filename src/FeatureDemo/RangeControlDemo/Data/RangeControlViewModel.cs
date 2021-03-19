using DevExpress.Mvvm;
using Microsoft.UI.Composition;
using System;
using System.Collections;
using System.Collections.Generic;

namespace RangeControlDemo {
    public class RangeControlViewModel : ViewModelBase {
        #region static
        public static string GetDisplayRange(DateTime start, DateTime end) => $"{start.ToString("MMMM dd, yyyy")}-{end.ToString("MMMM dd, yyyy")}";
        #endregion
        #region props
        IEnumerable itemsSource;
        DateTime start;
        readonly Random random = new Random();
        public int PointCount { get; }
        public double FirstPointValue { get; }
        public DateTime VisibleStart {
            get => GetProperty<DateTime>();
            set => SetProperty(value);
        }
        public DateTime VisibleEnd {
            get => GetProperty<DateTime>();
            set => SetProperty(value);
        }
        public DateTime SelectionStart {
            get => GetProperty<DateTime>();
            set => SetProperty(value);
        }
        public DateTime SelectionEnd {
            get => GetProperty<DateTime>();
            set => SetProperty(value);       
        }
        public TimeSpan Step { get; }
        public IEnumerable ItemsSource { get { return itemsSource ?? (itemsSource = CreateItemsSource(PointCount)); } }

        #endregion
        public RangeControlViewModel(double firstPointValue, int pointCount, TimeSpan step) {
            FirstPointValue = firstPointValue;
            PointCount = pointCount;
            Step = step;

            start = new DateTime(DateTime.Now.Ticks - Step.Ticks * PointCount);
            
            VisibleStart = start + TimeSpan.FromTicks(((long)random.Next(PointCount / 5) + PointCount / 5) * Step.Ticks);
            VisibleEnd = VisibleStart + TimeSpan.FromTicks(((long)random.Next(PointCount / 5) + PointCount / 5) * Step.Ticks);
            SelectionStart = start + TimeSpan.FromTicks(((long)random.Next(PointCount / 5) + PointCount / 5) * Step.Ticks);
            SelectionEnd = SelectionStart + TimeSpan.FromTicks(((long)random.Next(PointCount / 5) + PointCount / 5) * Step.Ticks);
        }

        protected double GenerateStartValue(Random random) {
            return FirstPointValue + random.NextDouble() * 100;
        }
        protected double GenerateAddition(Random random) {
            double factor = random.NextDouble();
            if (factor == 1)
                factor = 50;
            else if (factor == 0)
                factor = -50;
            return (factor - 0.5) * 50;
        }
               
        protected IEnumerable CreateItemsSource(int count) {
            var points = new List<DateTimeDataPoint>();

            double value = GenerateStartValue(random);
            points.Add(new DateTimeDataPoint() { Value = start, DisplayValue = value });
            for(int i = 1; i < count; i++) {
                value += GenerateAddition(random);
                start = start + Step;
                points.Add(new DateTimeDataPoint() { Value = start, DisplayValue = value });
            }
            return points;
        }

    }
    public class NumericDataPoint {
        public int Value { get; set; }
        public double DisplayValue { get; set; }
    }
    public class DateTimeDataPoint {
        public DateTime Value { get; set; }
        public double DisplayValue { get; set; }
    }
}