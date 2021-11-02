using DevExpress.Mvvm;
using DevExpress.WinUI.Gauges;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using System;
using DevExpress.Mvvm.CodeGenerators;

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

    [GenerateViewModel]
    public partial class HouseClimateViewModel {
        [GenerateProperty]
        double _OuterTemp;
        [GenerateProperty]
        double _PowerUsage;
        [GenerateProperty]
        double _KWHUsed;
        [GenerateProperty]
        double _GasUsage;
        [GenerateProperty]
        double _CBMUsed;
        [GenerateProperty]
        InnerState _InnerStateFloor1;
        [GenerateProperty]
        InnerState _InnerStateFloor2;

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

    [GenerateViewModel]
    public partial class InnerState {
        const double heaterPower = 3.0;
        const double heaterGasUsage = 0.6;
        const double coolerPower = 7.0;
        const double staticPower = 1.0;
        const double staticGasUsage = 0.0;

        [GenerateProperty]
        double _InnerTemp;
        [GenerateProperty]
        double _DesiredTemp;
        [GenerateProperty]
        bool _IndicatorVisible;
        [GenerateProperty]
        string _IndicatorText;
        [GenerateProperty]
        double _GasUsage;
        [GenerateProperty]
        double _PowerUsage;

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
