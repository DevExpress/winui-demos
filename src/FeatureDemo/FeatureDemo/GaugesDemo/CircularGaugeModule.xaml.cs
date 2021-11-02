﻿using DevExpress.Mvvm;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using System;
using System.ComponentModel;
using DevExpress.Mvvm.CodeGenerators;

namespace GaugesDemo {
    public sealed partial class CircularGaugeModule : DemoModuleView {
        public WorldTimeInfo ViewModel { get; } = new WorldTimeInfo();
        public CircularGaugeModule() {
            this.InitializeComponent();
            Unloaded += OnUnloaded;
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            ViewModel.StopTimer();
        }
    }

    [GenerateViewModel]
    public partial class WorldTimeInfo {
        [GenerateProperty]
        double _NewYorkHours;
        [GenerateProperty]
        double _LondonHours;
        [GenerateProperty]
        double _MoscowHours;
        [GenerateProperty]
        double _Minutes;
        [GenerateProperty]
        double _Seconds;

        DispatcherTimer timer = new DispatcherTimer();

        public WorldTimeInfo() {
            timer.Tick += OnTimedEvent;
            timer.Interval = new TimeSpan(0, 0, 1);
            UpdateTime();
            StartTimer();
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
    }
}
