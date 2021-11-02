using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace FeatureDemo.Common {
    public sealed partial class AlertControl : UserControl {

        public static readonly DependencyProperty MessageContentProperty = DependencyProperty.Register(nameof(MessageContent), typeof(object), typeof(AlertControl), new PropertyMetadata(null));
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(AlertControl), 
            new PropertyMetadata(true, (d,e) => ((AlertControl)d).OnIsVisibleChanged()));

        public AlertControl() {
            this.InitializeComponent();
        }

        public object MessageContent {
            get => (object)GetValue(MessageContentProperty);
            set => SetValue(MessageContentProperty, value);
        }
        public bool IsVisible {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        void OnIsVisibleChanged() => Visibility = IsVisible ? Visibility.Visible : Visibility.Collapsed;
        void OnCloseButtonClick(object sender, RoutedEventArgs e) => IsVisible = false;
    }
}
