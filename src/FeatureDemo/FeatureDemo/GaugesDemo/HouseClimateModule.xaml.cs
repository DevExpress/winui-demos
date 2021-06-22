using DevExpress.Mvvm;
using DevExpress.WinUI.Gauges;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using System;

namespace GaugesDemo {
    public sealed partial class HouseClimateModule : DemoModuleView {
        const double maxPosiibleInnerTemp = 35.0;
        const double minPossibleInnerTemp = 15.0;
        public HouseClimateViewModel ViewModel { get; } = new HouseClimateViewModel();
        public HouseClimateModule() {
            this.InitializeComponent();
            Unloaded += OnUnloaded;
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            ViewModel.StopTimer();
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

    public class HouseClimateViewModel : BindableBase {
        public double OuterTemp { get => GetValue<double>(); set => SetValue(value); }
        public double PowerUsage { get => GetValue<double>(); set => SetValue(value); }
        public double KWHUsed { get => GetValue<double>(); set => SetValue(value); }
        public double GasUsage { get => GetValue<double>(); set => SetValue(value); }
        public double CBMUsed { get => GetValue<double>(); set => SetValue(value); }
        public InnerState InnerStateFloor1 { get => GetValue<InnerState>(); set => SetValue(value); }
        public InnerState InnerStateFloor2 { get => GetValue<InnerState>(); set => SetValue(value); }

        const double tempDelta = 3.0;
        const double updateInterval = 1.0;

        DispatcherTimer updateTimer = new DispatcherTimer();
        DispatcherTimer outerTempTimer = new DispatcherTimer();

        public HouseClimateViewModel() {
            OuterTemp = 27;
            PowerUsage = 6;
            KWHUsed = 10;
            GasUsage = 1.2;
            CBMUsed = 3;
            InnerStateFloor1 = new InnerState();
            InnerStateFloor2 = new InnerState();

            Update();
            StartTimer();
        }
        public void StartTimer() {
            outerTempTimer.Interval = TimeSpan.FromSeconds(5);
            outerTempTimer.Tick += OuterTempTimerTick;
            outerTempTimer.Start();
            updateTimer.Interval = TimeSpan.FromSeconds(updateInterval);
            updateTimer.Tick += UpdateTimerTick;
            updateTimer.Start();
        }
        public void StopTimer() {
            outerTempTimer.Stop();
            updateTimer.Stop();
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
    public class InnerState : BindableBase {
        const double heaterPower = 3.0;
        const double heaterGasUsage = 0.6;
        const double coolerPower = 7.0;
        const double staticPower = 1.0;
        const double staticGasUsage = 0.0;

        public double InnerTemp { get => GetValue<double>(); set => SetValue(value); }
        public double DesiredTemp { get => GetValue<double>(); set => SetValue(value); }
        public bool IndicatorVisible { get => GetValue<bool>(); set => SetValue(value); }
        public string IndicatorText { get => GetValue<string>(); set => SetValue(value); }
        public double GasUsage { get => GetValue<double>(); set => SetValue(value); }
        public double PowerUsage { get => GetValue<double>(); set => SetValue(value); }

        bool tempReady = false;
        public InnerState() {
            InnerTemp = 20;
            DesiredTemp = 25;
            IndicatorVisible = true;
            IndicatorText = "Heating";
        }

        bool TempNormal() {
            if(tempReady) {
                if(!((InnerTemp <= DesiredTemp + 2.0) && (InnerTemp >= DesiredTemp - 2.0)))
                    tempReady = false;
            } else {
                if((InnerTemp <= DesiredTemp + 0.5) && (InnerTemp >= DesiredTemp - 0.5))
                    tempReady = true;
            }
            return tempReady;
        }
        public void Update(double outerTemp) {
            if(TempNormal()) {
                IndicatorVisible = false;
                PowerUsage = staticPower;
                GasUsage = staticGasUsage;
                InnerTemp += InnerTemp > outerTemp ? -0.25 : InnerTemp < outerTemp ? 0.25 : 0.0;
            } else {
                if(InnerTemp > DesiredTemp) {
                    InnerTemp -= 1.0;
                    IndicatorVisible = true;
                    IndicatorText = "Cooling";
                    PowerUsage = staticPower + coolerPower;
                    GasUsage = staticGasUsage;
                } else {
                    if(InnerTemp < DesiredTemp) {
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
}
