using FeatureDemo.Common;
using System;
using Windows.UI.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
namespace RangeControlDemo {
    public sealed partial class AggregationModule : DemoModuleView {
        public AggregationModule() {
            DateTime visibleEnd = new DateTime(DateTime.Now.Year, 1, 1);
            ViewModel = new RangeControlViewModel(10000, 50000, TimeSpan.FromHours(3)) {
                                VisibleStart = visibleEnd.AddYears(-8),
                                VisibleEnd = visibleEnd,
                                SelectionStart = visibleEnd.AddYears(-2),
                                SelectionEnd = visibleEnd.AddYears(-1)
                            };
            this.InitializeComponent();
            Unloaded += OnUnload;
        }

        public RangeControlViewModel ViewModel { get; }

        void OnUnload(object sender, RoutedEventArgs e) {
            rangeControl.Content = null;
        }
    }
}
