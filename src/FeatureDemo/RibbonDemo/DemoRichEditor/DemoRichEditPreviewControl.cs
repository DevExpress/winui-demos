using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Mvvm.UI.Internal;

namespace RibbonDemo {
    public class RichEditPreviewControl : Control {
        public static readonly DependencyProperty RTFTextProperty;
        static RichEditPreviewControl() {
            RTFTextProperty = DependencyPropertyManager.Register<RichEditPreviewControl, string>("RTFText", string.Empty);
        }
        public string RTFText {
            get { return (string)GetValue(RTFTextProperty); }
            set { SetValue(RTFTextProperty, value); }
        }
        public RichEditPreviewControl() {
            DefaultStyleKey = typeof(RichEditPreviewControl);
            Loaded += OnLoaded;
        }

        ScrollViewer scroll;
        DemoRichEditBox editor;

        void OnLoaded(object sender, RoutedEventArgs e) {
            editor = LayoutHelper.FindElementByName<DemoRichEditBox>(this, "PART_editbox");
            if (editor != null)
                editor.Loaded += OnEditorLoaded;
        }
        void OnEditorLoaded(object sender, RoutedEventArgs e) {
            scroll = LayoutHelper.FindElementByName<ScrollViewer>(this, "PART_scroll");
            scroll.SizeChanged += OnSizeChanged;
            UpdateZoom();
        }
        void OnSizeChanged(object sender, SizeChangedEventArgs e) {
            if (editor != null && scroll != null)
                UpdateZoom();
        }
        void UpdateZoom() {
            float zoomFactor = (float)scroll.ActualWidth / (float)(editor.ActualWidth);
            if (!float.IsNaN(zoomFactor))
                scroll.Do(x => x.ChangeView(0, 0, zoomFactor));
        }
    }
}