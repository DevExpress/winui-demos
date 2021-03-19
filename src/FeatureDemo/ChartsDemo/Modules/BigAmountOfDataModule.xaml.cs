using System;
using System.Collections.Generic;
using DevExpress.WinUI.Charts;
using Microsoft.UI.Xaml;
using FeatureDemo.Common;
using DevExpress.WinUI.Core.Internal;

namespace ChartsDemo {
    public sealed partial class BigAmountOfDataModule : DemoModuleView {
        public BigAmountOfDataModule() {
            this.InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            int pointsCountPerSeries = 100000;
            GenerateData(pointsCountPerSeries);
            chart.ActualAxisX.VisualRange = new VisualAxisRange() { StartValueInternal = pointsCountPerSeries / 10, EndValueInternal = pointsCountPerSeries / 5 };
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            chart.Series[0].DataContext = null;
            chart.Series[1].DataContext = null;
            chart = null;
        }
        void GenerateData(int count) {
            chart.Series[0].DataContext = new DataGenerator(count);
            chart.Series[1].DataContext = new DataGenerator(count);
        }
    }

    class DataGenerator: ChartDataAdapter {
        List<SeriesPoint> points = new List<SeriesPoint>();
        Random rnd = new Random();
        Random additionalRnd = new Random();

        public List<SeriesPoint> PointsList {
            get {
                return points;
            }
            set {
                if((value as List<SeriesPoint>) != null) {
                    points = value;
                }
            }
        }
        protected override int RowsCount { get { return points.Count; } }

        public DataGenerator(int count) {
            Random random = new Random();
            double price1 = GenerateStartValue(random);
            points.Add(new SeriesPoint(0, GenerateStartValue(random)));
            for (int i = 0; i < count; i++) {
                points.Add(new SeriesPoint(i, price1));
                price1 += GenerateAddition(random);
            }
        }
        double GenerateStartValue(Random random) {
            return random.NextDouble() * 100;
        }
        double GenerateAddition(Random random) {
            double factor = random.NextDouble();
            if (factor == 1)
                factor = 50;
            else if (factor == 0)
                factor = -50;
            return (factor - 0.5) * 50;
        }
        protected override DateTime GetDateTimeValue(int index, ChartDataMemberType dataMember) {
            return DateTime.MinValue;
        }
        protected override object GetKey(int index) {
            return null;
        }
        protected override double GetNumericalValue(int index, ChartDataMemberType dataMember) {
            if (dataMember == ChartDataMemberType.Argument)
                return points[index].Argument;
            else
                return points[index].Value;
        }
        protected override string GetQualitativeValue(int index, ChartDataMemberType dataMember) {
            return null;
        }
        protected override ActualScaleType GetScaleType(ChartDataMemberType dataMember) {
            return ActualScaleType.Numerical;
        }
    }

    class SeriesPoint{
        public int Argument{get; private set;}
        public double Value{get; private set;}

        public SeriesPoint(int argument, double value) {
            Argument = argument;
            Value = value;
        }
    }
}
