using DevExpress.WinUI.Mvvm.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI.ViewManagement;

namespace FeatureDemo.Common {
    public interface IAppThemeService {
        void Toggle();
    }
    public class AppThemeService : ServiceBase, IAppThemeService {
        public SolidColorBrush WindowTitleButtonBackgroundBrush {
            get { return (SolidColorBrush)GetValue(WindowTitleButtonBackgroundBrushProperty); }
            set { SetValue(WindowTitleButtonBackgroundBrushProperty, value); }
        }

        public static readonly DependencyProperty WindowTitleButtonBackgroundBrushProperty =
            DependencyProperty.Register(nameof(WindowTitleButtonBackgroundBrush), typeof(SolidColorBrush), typeof(AppThemeService), new PropertyMetadata(null, OnWindowTitleButtonBackgroundBrushChanged));

        private static void OnWindowTitleButtonBackgroundBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if(titleBar == null) return;
            titleBar.ButtonBackgroundColor = ((AppThemeService)d).WindowTitleButtonBackgroundBrush.Color;
            titleBar.ButtonInactiveBackgroundColor = ((AppThemeService)d).WindowTitleButtonBackgroundBrush.Color;            
        }
        public void Toggle() {
            var element = Window.Current.Content as FrameworkElement;
            if(element == null) return;            
            element.RequestedTheme = element.RequestedTheme == ElementTheme.Default || element.RequestedTheme == ElementTheme.Light  ? ElementTheme.Dark: ElementTheme.Light;
        }
    }
}