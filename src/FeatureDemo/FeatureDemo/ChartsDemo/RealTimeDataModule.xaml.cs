using DevExpress.WinUI.Charts;
using DevExpress.WinUI.Core.Internal;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Security.Policy;
using System.Threading;
using Windows.Foundation;

namespace ChartsDemo {
    public sealed partial class RealTimeDataModule : DemoModuleView {
        readonly DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 20) };
        readonly SensorDataGenerator generator;

        public SensorDataAdapter Data1 { get; } = new SensorDataAdapter();
        public SensorDataAdapter Data2 { get; } = new SensorDataAdapter();

        public RealTimeDataModule() {
            InitializeComponent();
            generator = new SensorDataGenerator(Data1, Data2);
            generator.GenerateInitialData();
            timer.Tick += Timer_Tick;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        void Timer_Tick(object sender, object e) {
            generator.UpdateDataSource();
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            generator.Start(CurrentWindowHelper.CurrentWindow);
            timer.Start();
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            timer.Stop();
            generator.Stop();
        }
    }

    public class SensorIndicationItem {
        public DateTime TimeStamp { get; private set; }
        public double SensorIndication1 { get; private set; }
        public double SensorIndication2 { get; private set; }
        public double SensorIndication3 { get; private set; }
        public double SensorIndication4 { get; private set; }
        public double SensorIndication5 { get; private set; }
        public double SensorIndication6 { get; private set; }
        public double SensorIndication7 { get; private set; }
        public double SensorIndication8 { get; private set; }

        internal SensorIndicationItem(DateTime timeStamp, double sensorIndication1,
                                                        double sensorIndication2,
                                                        double sensorIndication3,
                                                        double sensorIndication4,
                                                        double sensorIndication5,
                                                        double sensorIndication6,
                                                        double sensorIndication7,
                                                        double sensorIndication8) {
            this.TimeStamp = timeStamp;
            this.SensorIndication1 = sensorIndication1;
            this.SensorIndication2 = sensorIndication2;
            this.SensorIndication3 = sensorIndication3;
            this.SensorIndication4 = sensorIndication4;
            this.SensorIndication5 = sensorIndication5;
            this.SensorIndication6 = sensorIndication6;
            this.SensorIndication7 = sensorIndication7;
            this.SensorIndication8 = sensorIndication8;
        }
    }

    public class SensorDataGenerator {
        const int InitialDataPointsCount = 2500;
        public const int DataGenerationIntervalMilliseconds = 15;

        readonly SensorDataAdapter adapter1;
        readonly SensorDataAdapter adapter2;
        readonly Random random = new Random(1);
        readonly List<SensorIndicationItem> buffer = new List<SensorIndicationItem>();
        readonly object sync = new object();
        int counter;
        double yAddition1 = 0;
        double yAddition2 = 0;
        double yAddition3 = 0;
        double yAddition4 = 0;
        double yAddition5 = 0;
        double yAddition6 = 0;
        double yAddition7 = 0;
        double yAddition8 = 0;
        bool generatingEnabled = false;
        Thread generatingThread;
        Window window;

        public SensorDataGenerator(SensorDataAdapter adapter1, SensorDataAdapter adapter2) {
            this.adapter1 = adapter1;
            this.adapter2 = adapter2;
        }
        SensorIndicationItem CreatePoint(DateTime timeStamp) {
            counter++;
            double arg = timeStamp.ToOADate();
            arg = arg * 250000d;
            if(counter % random.Next(300, 500) == 0)
                yAddition1 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition1 < -30)
                yAddition1 += 10;
            if(yAddition1 > 30)
                yAddition1 -= 10;
            double indication1 = 5 * Math.Sin(5d / 2d * Math.Cos(arg)) + 100 + (random.NextDouble() - 0.5) * 5 + yAddition1;
            if(counter % random.Next(100, 300) == 0)
                yAddition2 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition2 < -30)
                yAddition2 += 10;
            if(yAddition2 > 30)
                yAddition2 -= 10;
            double indication2 = 4 * Math.Sin(7 * Math.Cos(arg - 1.5)) + 90 + (random.NextDouble() - 0.5) * 7 + yAddition2;

            if(counter % random.Next(100, 500) == 0)
                yAddition3 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition3 < -30)
                yAddition3 += 10;
            if(yAddition3 > 30)
                yAddition3 -= 10;
            double indication3 = 10 * (Math.Sin(arg) + Math.Sin(arg / 1.2d) + Math.Sin(arg / 1.5d)) + 100 + random.NextDouble() * 12 + yAddition3;
            if(counter % random.Next(50, 100) == 0)
                yAddition4 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition4 < -30)
                yAddition4 += 10;
            if(yAddition4 > 30)
                yAddition4 -= 10;
            double indication4 = 10 * (Math.Cos(arg + 1.5) + Math.Sin(arg / 1.2d + 0.5) + Math.Cos(arg / 1.5d + 0.3)) + 120 + random.NextDouble() * 15 + yAddition4;

            if(counter % random.Next(300, 400) == 0)
                yAddition5 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition5 < -30)
                yAddition5 += 10;
            if(yAddition5 > 30)
                yAddition5 -= 10;
            double indication5 = 15 * Math.Cos(Math.Tan(arg + random.NextDouble() / 10)) + 500 + random.NextDouble() * 15 + yAddition5;
            if(counter % random.Next(400, 1000) == 0)
                yAddition6 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition6 < -30)
                yAddition6 += 10;
            if(yAddition6 > 30)
                yAddition6 -= 10;
            double indication6 = 20 * Math.Sin(Math.Tan(arg + 1)) + 450 + random.NextDouble() * 9 + yAddition6;

            if(counter % random.Next(200, 300) == 0)
                yAddition7 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition7 < -30)
                yAddition7 += 10;
            if(yAddition7 > 30)
                yAddition7 -= 10;
            double indication7 = 30 * Math.Abs(Math.Sin(Math.Tan(arg + 1))) + Math.Cos(arg) + Math.Sin(arg) + 750 + random.NextDouble() * 15 + yAddition7;
            if(counter % random.Next(300, 350) == 0)
                yAddition8 += random.Next(10, 20) * Math.Sign(random.NextDouble() - 0.5);
            if(yAddition8 < -30)
                yAddition8 += 10;
            if(yAddition8 > 30)
                yAddition8 -= 10;
            double indication8 = 30 * (1 - Math.Cos(arg)) / random.Next(1, 5) + 700 + random.NextDouble() * 15 + yAddition8;

            return new SensorIndicationItem(timeStamp, indication1, indication2, indication3, indication4, indication5, indication6, indication7, indication8);
        }
        void AddPoint(DateTime timeStamp) {
            SensorIndicationItem point = CreatePoint(timeStamp);
            lock(sync) {
                buffer.Add(point);
            }
        }
        void GeneratingLoop() {
            DateTime timeStamp = DateTime.Now;
            while(generatingEnabled) {
                DateTime newTimeStamp = timeStamp.AddMilliseconds(DataGenerationIntervalMilliseconds);
                TimeSpan span = newTimeStamp - DateTime.Now;
                if(span.Ticks > 0)
                    Thread.Sleep((int)span.TotalMilliseconds);
                timeStamp = newTimeStamp;
                AddPoint(timeStamp);
            }
        }
        internal void GenerateInitialData() {
            DateTime baseTimeStamp = DateTime.Now.AddMilliseconds(-InitialDataPointsCount * DataGenerationIntervalMilliseconds);
            DateTime argument = baseTimeStamp;
            for(int i = 0; i < InitialDataPointsCount - 1; i++) {
                argument = argument.AddMilliseconds(DataGenerationIntervalMilliseconds);
                SensorIndicationItem point = CreatePoint(argument);
                adapter1.Data.Add(new DataItem { Argument = argument, Value = point.SensorIndication1 });
                adapter2.Data.Add(new DataItem { Argument = argument, Value = point.SensorIndication2 });
            }
        }
        internal void UpdateDataSource() {
            lock(sync) {
                foreach(SensorIndicationItem item in buffer) {
                    adapter1.Data.Add(new DataItem { Argument = item.TimeStamp, Value = item.SensorIndication1 });
                    adapter2.Data.Add(new DataItem { Argument = item.TimeStamp, Value = item.SensorIndication2 });
                }
                if(adapter1.Data.Count > InitialDataPointsCount)
                    adapter1.Data.RemoveRange(0, buffer.Count);
                if(adapter2.Data.Count > InitialDataPointsCount)
                    adapter2.Data.RemoveRange(0, buffer.Count);
                buffer.Clear();
                adapter1.UpdateData();
                adapter2.UpdateData();
            }
        }
        

        internal void Start(Window wnd) {
            window = wnd;
            window.Closed += OnWindowClosed;
            if(generatingThread == null)
                generatingThread = new Thread(new ThreadStart(GeneratingLoop));
            generatingEnabled = true;
            generatingThread.Start();
        }

        private void OnWindowClosed(object sender, WindowEventArgs args) => Stop();

        internal void Stop() {
            window.Closed -= OnWindowClosed;
            generatingEnabled = false;
            if(generatingThread != null)
                generatingThread.Join();
            generatingThread = null;
        }
    }

    public struct DataItem {
        public DateTime Argument { get; set; }
        public double Value { get; set; }
    }

    public class SensorDataAdapter : ChartDataAdapter {
        readonly List<DataItem> data = new List<DataItem>();

        protected override int RowsCount { get { return data.Count; } }
        internal List<DataItem> Data { get { return data; } }

        protected override ActualScaleType GetScaleType(ChartDataMemberType dataMember) {
            return dataMember == ChartDataMemberType.Argument ? ActualScaleType.DateTime : ActualScaleType.Numerical;
        }
        protected override object GetKey(int index) {
            return data[index];
        }
        protected override double GetNumericalValue(int index, ChartDataMemberType dataMember) {
            return data[index].Value;
        }
        protected override string GetQualitativeValue(int index, ChartDataMemberType dataMember) {
            throw new NotImplementedException();
        }
        protected override DateTime GetDateTimeValue(int index, ChartDataMemberType dataMember) {
            return data[index].Argument;
        }
        internal void UpdateData() {
            OnDataChanged(ChartDataUpdateType.Reset, -1);
        }
    }
}
