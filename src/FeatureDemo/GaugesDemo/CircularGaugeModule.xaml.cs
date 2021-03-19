using FeatureDemo.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using INotifyPropertyChanged = Microsoft.UI.Xaml.Data.INotifyPropertyChanged;
using PropertyChangedEventHandler = Microsoft.UI.Xaml.Data.PropertyChangedEventHandler;
using PropertyChangedEventArgs = Microsoft.UI.Xaml.Data.PropertyChangedEventArgs;

namespace GaugesDemo {
    public sealed partial class CircularGaugeModule : DemoModuleView {
        public CircularGaugeModule() {
            this.InitializeComponent();
            DataContext = new WorldTimeInfo();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            DataContext = new WorldTimeInfo();
            (DataContext as WorldTimeInfo).StartTimer();
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            (DataContext as WorldTimeInfo).StopTimer();
            DataContext = null;
        }
    }

    public class WorldTimeInfo : DependencyObject, INotifyPropertyChanged {
        public static readonly DependencyProperty NewYorkHoursProperty = DependencyProperty.Register("NewYorkHours",
            typeof(double), typeof(WorldTimeInfo), new PropertyMetadata(0.0, (d, e) => { NotifyPropertyChanged(d, "NewYorkHours"); }));
        public static readonly DependencyProperty LondonHoursProperty = DependencyProperty.Register("LondonHours",
           typeof(double), typeof(WorldTimeInfo), new PropertyMetadata(0.0, (d, e) => { NotifyPropertyChanged(d, "LondonHours"); }));
        public static readonly DependencyProperty MoscowHoursProperty = DependencyProperty.Register("MoscowHours",
           typeof(double), typeof(WorldTimeInfo), new PropertyMetadata(0.0, (d, e) => { NotifyPropertyChanged(d, "MoscowHours"); }));
        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register("Minutes",
           typeof(double), typeof(WorldTimeInfo), new PropertyMetadata(0.0, (d, e) => { NotifyPropertyChanged(d, "Minutes"); }));
        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register("Seconds",
           typeof(double), typeof(WorldTimeInfo), new PropertyMetadata(0.0, (d, e) => { NotifyPropertyChanged(d, "Seconds"); }));

        public double NewYorkHours {
            get { return (double)GetValue(NewYorkHoursProperty); }
            set { SetValue(NewYorkHoursProperty, value); }
        }
        public double LondonHours {
            get { return (double)GetValue(LondonHoursProperty); }
            set { SetValue(LondonHoursProperty, value); }
        }
        public double MoscowHours {
            get { return (double)GetValue(MoscowHoursProperty); }
            set { SetValue(MoscowHoursProperty, value); }
        }
        public double Minutes {
            get { return (double)GetValue(MinutesProperty); }
            set { SetValue(MinutesProperty, value); }
        }
        public double Seconds {
            get { return (double)GetValue(SecondsProperty); }
            set { SetValue(SecondsProperty, value); }
        }

        protected static void NotifyPropertyChanged(DependencyObject d, string propertyName) {
            WorldTimeInfo obj = d as WorldTimeInfo;
            if (obj != null)
                obj.NotifyPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        DispatcherTimer timer = new DispatcherTimer();

        public WorldTimeInfo() {
            timer.Tick += OnTimedEvent;
            timer.Interval = new TimeSpan(0, 0, 1);
            UpdateTime();
        }
        public void StartTimer() {
            timer.Start();
        }
        public void StopTimer() {
            timer.Stop();
        }
        void OnTimedEvent(object source, object e) {
            UpdateTime();
        }
        void UpdateTime() {
            var zones = TimeZoneInfo.GetSystemTimeZones();
            NewYorkHours = TimeZoneInfo.ConvertTime(DateTime.Now, zones[20]).Hour % 12 + DateTime.UtcNow.Minute / 60.0;
            LondonHours = TimeZoneInfo.ConvertTime(DateTime.Now, zones[46]).Hour % 12 + DateTime.UtcNow.Minute / 60.0;
            MoscowHours = TimeZoneInfo.ConvertTime(DateTime.Now, zones[70]).Hour % 12 + DateTime.UtcNow.Minute / 60.0;
            Minutes = ((DateTime.UtcNow.Minute + DateTime.UtcNow.Second / 60.0) / 60.0) * 12;
            Seconds = ((DateTime.UtcNow.Second) / 60.0) * 12;
        }
        protected void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
