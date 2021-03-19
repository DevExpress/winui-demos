using DevExpress.WinUI.Data.Async;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace FeatureDemo.Common {
    public class TitleBarSettings {
        public Color? ButtonBackgroundColor { get; set; }
        public Color? ButtonHoverBackgroundColor { get; set; }
        public Color? ButtonPressedBackgroundColor { get; set; }
        public Color? ButtonForegroundColor { get; set; }
        public Color? ButtonHoverForegroundColor { get; set; }
        public Color? ButtonPressedForegroundColor { get; set; }
        public Color? ButtonInactiveBackgroundColor { get; set; }
        public static TitleBarSettings FromTitleBar() {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            TitleBarSettings res = new TitleBarSettings();
            res.ButtonBackgroundColor = titleBar.ButtonBackgroundColor;
            res.ButtonHoverBackgroundColor = titleBar.ButtonHoverBackgroundColor;
            res.ButtonPressedBackgroundColor = titleBar.ButtonPressedBackgroundColor;
            res.ButtonForegroundColor = titleBar.ButtonForegroundColor;
            res.ButtonHoverForegroundColor = titleBar.ButtonHoverForegroundColor;
            res.ButtonPressedForegroundColor = titleBar.ButtonPressedForegroundColor;
            res.ButtonInactiveBackgroundColor = titleBar.ButtonInactiveBackgroundColor;
            return res;
        }
        public void Apply() {
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = ButtonBackgroundColor;
            titleBar.ButtonHoverBackgroundColor = ButtonHoverBackgroundColor;
            titleBar.ButtonPressedBackgroundColor = ButtonPressedBackgroundColor;
            titleBar.ButtonForegroundColor = ButtonForegroundColor;
            titleBar.ButtonHoverForegroundColor = ButtonHoverForegroundColor;
            titleBar.ButtonPressedForegroundColor = ButtonPressedForegroundColor;
            titleBar.ButtonInactiveBackgroundColor = ButtonInactiveBackgroundColor;
        }
    }
}