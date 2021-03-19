using DevExpress.WinUI.Gauges;
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
    public sealed partial class HouseClimateDashboardModule : DemoModuleView {
        const double maxPosiibleInnerTemp = 35.0;
        const double minPossibleInnerTemp = 15.0;

        public HouseClimateDashboardModule() {
            this.InitializeComponent();
            this.DataContext = new ResourcesInfo();
        }

        void LinearScaleMarker_ValueChanged(object sender, ValueChangedEventArgs e) {
            LinearScaleMarker marker = sender as LinearScaleMarker;
            if (marker != null) {
                if (e.NewValue > maxPosiibleInnerTemp)
                    marker.Value = maxPosiibleInnerTemp;
                if (e.NewValue < minPossibleInnerTemp)
                    marker.Value = minPossibleInnerTemp;
            }
        }
    }

    public class NotifyPropertyChangedObject : DependencyObject, INotifyPropertyChanged {
        protected static void NotifyPropertyChanged(DependencyObject d, string propertyName) {
            NotifyPropertyChangedObject obj = d as NotifyPropertyChangedObject;
            if (obj != null)
                obj.NotifyPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InnerState : NotifyPropertyChangedObject {
        public static readonly DependencyProperty InnerTempProperty = DependencyProperty.Register("InnerTemp",
            typeof(double), typeof(InnerState), new PropertyMetadata(20.0, (d, e) => { NotifyPropertyChanged(d, "InnerTemp"); }));
        public static readonly DependencyProperty DesiredTempProperty = DependencyProperty.Register("DesiredTemp",
            typeof(double), typeof(InnerState), new PropertyMetadata(25.0, (d, e) => { NotifyPropertyChanged(d, "DesiredTemp"); }));
        public static readonly DependencyProperty IndicatorVisibleProperty = DependencyProperty.Register("IndicatorVisible",
            typeof(bool), typeof(InnerState), new PropertyMetadata(true, (d, e) => { NotifyPropertyChanged(d, "IndicatorVisible"); }));
        public static readonly DependencyProperty IndicatorTextProperty = DependencyProperty.Register("IndicatorText",
            typeof(string), typeof(InnerState), new PropertyMetadata("Heating", (d, e) => { NotifyPropertyChanged(d, "IndicatorText"); }));

        public double InnerTemp {
            get { return (double)GetValue(InnerTempProperty); }
            set { SetValue(InnerTempProperty, value); }
        }
        public double DesiredTemp {
            get { return (double)GetValue(DesiredTempProperty); }
            set { SetValue(DesiredTempProperty, value); }
        }
        public bool IndicatorVisible {
            get { return (bool)GetValue(IndicatorVisibleProperty); }
            set { SetValue(IndicatorVisibleProperty, value); }
        }
        public string IndicatorText {
            get { return (string)GetValue(IndicatorTextProperty); }
            set { SetValue(IndicatorTextProperty, value); }
        }

        const double heaterPower = 3.0;
        const double heaterGasUsage = 0.6;
        const double coolerPower = 7.0;
        const double staticPower = 1.0;
        const double staticGasUsage = 0.0;

        double gasUsage;
        double powerUsage;
        bool tempReady = false;

        public double GasUsage {
            get { return gasUsage; }
            set { gasUsage = value; }
        }
        public double PowerUsage {
            get { return powerUsage; }
            set { powerUsage = value; }
        }

        bool TempNormal() {
            if (tempReady) {
                if (!((InnerTemp <= DesiredTemp + 2.0) && (InnerTemp >= DesiredTemp - 2.0)))
                    tempReady = false;
            }
            else {
                if ((InnerTemp <= DesiredTemp + 0.5) && (InnerTemp >= DesiredTemp - 0.5))
                    tempReady = true;
            }
            return tempReady;
        }
        public void Update(double outerTemp) {
            if (TempNormal()) {
                IndicatorVisible = false;
                PowerUsage = staticPower;
                GasUsage = staticGasUsage;
                InnerTemp += InnerTemp > outerTemp ? -0.25 : InnerTemp < outerTemp ? 0.25 : 0.0;
            }
            else {
                if (InnerTemp > DesiredTemp) {
                    InnerTemp -= 1.0;
                    IndicatorVisible = true;
                    IndicatorText = "Cooling";
                    PowerUsage = staticPower + coolerPower;
                    GasUsage = staticGasUsage;
                }
                else {
                    if (InnerTemp < DesiredTemp) {
                        InnerTemp += 1.0;
                        IndicatorVisible = true;
                        IndicatorText = "Heating";
                        PowerUsage = staticPower + heaterPower;
                        GasUsage = staticGasUsage + heaterGasUsage;
                    }
                }
            }
        }
    }

    public class ResourcesInfo : NotifyPropertyChangedObject {
        public static readonly DependencyProperty OuterTempProperty = DependencyProperty.Register("OuterTemp",
            typeof(double), typeof(ResourcesInfo), new PropertyMetadata(27.0, (d, e) => { NotifyPropertyChanged(d, "OuterTemp"); }));
        public static readonly DependencyProperty PowerUsageProperty = DependencyProperty.Register("PowerUsage",
            typeof(double), typeof(ResourcesInfo), new PropertyMetadata(6.0, (d, e) => { NotifyPropertyChanged(d, "PowerUsage"); }));
        public static readonly DependencyProperty KWHUsedProperty = DependencyProperty.Register("KWHUsed",
            typeof(double), typeof(ResourcesInfo), new PropertyMetadata(10.0, (d, e) => { NotifyPropertyChanged(d, "KWHUsed"); }));
        public static readonly DependencyProperty GasUsageProperty = DependencyProperty.Register("GasUsage",
            typeof(double), typeof(ResourcesInfo), new PropertyMetadata(1.2, (d, e) => { NotifyPropertyChanged(d, "GasUsage"); }));
        public static readonly DependencyProperty CBMUsedProperty = DependencyProperty.Register("CBMUsed",
            typeof(double), typeof(ResourcesInfo), new PropertyMetadata(3.0, (d, e) => { NotifyPropertyChanged(d, "CBMUsed"); }));
        public static readonly DependencyProperty InnerStateFloor1Property = DependencyProperty.Register("InnerStateFloor1",
            typeof(object), typeof(ResourcesInfo), new PropertyMetadata(new InnerState(), (d, e) => { NotifyPropertyChanged(d, "InnerStateFloor1"); }));
        public static readonly DependencyProperty InnerStateFloor2Property = DependencyProperty.Register("InnerStateFloor2",
            typeof(object), typeof(ResourcesInfo), new PropertyMetadata(new InnerState(), (d, e) => { NotifyPropertyChanged(d, "InnerStateFloor2"); }));

        public double OuterTemp {
            get { return (double)GetValue(OuterTempProperty); }
            set { SetValue(OuterTempProperty, value); }
        }
        public double PowerUsage {
            get { return (double)GetValue(PowerUsageProperty); }
            set { SetValue(PowerUsageProperty, value); }
        }
        public double KWHUsed {
            get { return (double)GetValue(KWHUsedProperty); }
            set { SetValue(KWHUsedProperty, value); }
        }
        public double GasUsage {
            get { return (double)GetValue(GasUsageProperty); }
            set { SetValue(GasUsageProperty, value); }
        }
        public double CBMUsed {
            get { return (double)GetValue(CBMUsedProperty); }
            set { SetValue(CBMUsedProperty, value); }
        }
        public InnerState InnerStateFloor1 {
            get { return (InnerState)GetValue(InnerStateFloor1Property); }
            set { SetValue(InnerStateFloor1Property, value); }
        }
        public InnerState InnerStateFloor2 {
            get { return (InnerState)GetValue(InnerStateFloor2Property); }
            set { SetValue(InnerStateFloor2Property, value); }
        }

        const double tempDelta = 3.0;
        const double updateInterval = 1.0;

        DispatcherTimer updateTimer = new DispatcherTimer();
        DispatcherTimer outerTempTimer = new DispatcherTimer();

        public ResourcesInfo() {
            Update();
            outerTempTimer.Interval = TimeSpan.FromSeconds(5);
            outerTempTimer.Tick += OuterTempTimerTick;
            outerTempTimer.Start();
            updateTimer.Interval = TimeSpan.FromSeconds(updateInterval);
            updateTimer.Tick += UpdateTimerTick;
            updateTimer.Start();
        }
        void OuterTempTimerTick(object sender, object e) {
            Random random = new Random();
            OuterTemp = random.Next((int)(OuterTemp - tempDelta), (int)(OuterTemp + tempDelta));
        }
        void UpdateTimerTick(object sender, object e) {
            Update();
        }
        void Update() {
            InnerStateFloor1.Update(OuterTemp);
            InnerStateFloor2.Update(OuterTemp);
            PowerUsage = InnerStateFloor1.PowerUsage + InnerStateFloor2.PowerUsage;
            GasUsage = InnerStateFloor1.GasUsage + InnerStateFloor2.GasUsage;
            KWHUsed += (updateInterval * PowerUsage / 3600.0);
            CBMUsed += (updateInterval * GasUsage / 3600.0);
        }
    }
}
