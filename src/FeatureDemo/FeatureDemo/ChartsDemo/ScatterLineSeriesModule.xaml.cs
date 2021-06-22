using FeatureDemo.Common;
using System;

namespace ChartsDemo {
    public sealed partial class ScatterLineSeriesModule : DemoModuleView {
        const int a = 10;

        public ScatterLineSeriesModule() {
            this.InitializeComponent();
            CreateArchimedianSpiralPoints();
            CreateCardioidPoints();
            CreateCartesianFoliumPoints();
        }

        void CreateArchimedianSpiralPoints() {
            for (int i = 0; i < 720; i += 15) {
                double t = (double)i / 180 * Math.PI;
                double x = t * Math.Cos(t);
                double y = t * Math.Sin(t);
                archimedianSpiralData.Add(new DevExpress.WinUI.Charts.DataPoint(x, y));
            }
        }
        void CreateCardioidPoints() {
            for (int i = 0; i < 360; i += 10) {
                double t = (double)i / 180 * Math.PI;
                double x = a * (2 * Math.Cos(t) - Math.Cos(2 * t));
                double y = a * (2 * Math.Sin(t) - Math.Sin(2 * t));
                cardioidData.Add(new DevExpress.WinUI.Charts.DataPoint(x, y));
            }
        }
        void CreateCartesianFoliumPoints() {
            for (int i = -30; i < 125; i += 5) {
                double t = Math.Tan((double)i / 180 * Math.PI);
                double x = 3 * (double)a * t / (t * t * t + 1);
                double y = x * t;
                cartesianFoliumData.Add(new DevExpress.WinUI.Charts.DataPoint(x, y));
            }
        }
    }
}
