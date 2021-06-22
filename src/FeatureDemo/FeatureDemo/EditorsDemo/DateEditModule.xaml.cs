using FeatureDemo.Common;
using System;
using Microsoft.UI.Xaml.Controls;

namespace EditorsDemo {
    public sealed partial class DateEditModule : DemoModuleView {
        public DateEditModule() {
            this.InitializeComponent();
        }
        public DateTime DateTimeNow { get; } = DateTime.Now;
    }
}
