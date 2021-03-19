using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Color = Windows.UI.Color;

namespace FeatureDemo.Common {
    public sealed class StockArrowControl : Control {
        public static readonly DependencyProperty IsUpProperty = DependencyProperty.Register("IsUp", typeof(bool), typeof(StockArrowControl), new PropertyMetadata(false));

        public bool IsUp {
            get { return (bool)GetValue(IsUpProperty); }
            set { SetValue(IsUpProperty, value); }
        }

        public StockArrowControl() {
            DefaultStyleKey = typeof(StockArrowControl);
        }
    }

    public class StockGrid : Grid {
        public static readonly DependencyProperty DeltaPriceProperty = DependencyProperty.Register("DeltaPrice", typeof(double), typeof(StockGrid), new PropertyMetadata(0.0, (d, e) => ((StockGrid)d).OnDeltaPriceChanged(e.OldValue, (double)e.NewValue)));

        public double DeltaPrice {
            get { return (double)GetValue(DeltaPriceProperty); }
            set { SetValue(DeltaPriceProperty, value); }
        }
        void OnDeltaPriceChanged(object oldValue, double newValue) {
            double old = Convert.ToDouble(oldValue);
            if(Math.Sign(old) == Math.Sign(newValue)) {
                Background = new SolidColorBrush(Colors.Transparent);
                return;
            }
            if(newValue < 0)
                Background = new SolidColorBrush(Color.FromArgb(230, 230, 210, 216));
            if(newValue > 0)
                Background = new SolidColorBrush(Color.FromArgb(230, 210, 227, 223));
        }
    }
}
