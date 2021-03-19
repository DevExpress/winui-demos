using DevExpress.WinUI.Core.Extensions;
using DevExpress.WinUI.Diagnostic.DXVisualizer;
using FeatureDemo.Common;
using FeatureDemo.ViewModel;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Hosting;
using System.Linq;
using Windows.UI.ViewManagement;

namespace FeatureDemo.View {
    public sealed partial class GetStartedPage : DemoPageBase {

        public GetStartedPage() {
            InitializeComponent();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        TitleBarSettings TitleBarSettings { get; set; } = null;
        private void OnUnloaded(object sender, RoutedEventArgs e) {
            TitleBarSettings?.Apply();
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            InitTitleBar();
            InitComposition();
        }
        private void InitTitleBar() {
            TitleBarSettings = TitleBarSettings.FromTitleBar();

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonHoverBackgroundColor = Windows.UI.Color.FromArgb(0x30, 0xff, 0xff, 0xff);
            titleBar.ButtonPressedBackgroundColor = Windows.UI.Color.FromArgb(0x60, 0xff, 0xff, 0xff);
            titleBar.ButtonForegroundColor = Windows.UI.Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            titleBar.ButtonHoverForegroundColor = Windows.UI.Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            titleBar.ButtonPressedForegroundColor = Windows.UI.Color.FromArgb(0xff, 0xff, 0xff, 0xff);
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
        private void InitVerticalScrollBarOffset() {
            var scrollBar = MainScrollViewer.VisualChildren().OfType<ScrollBar>().Where(x => x?.Name == "VerticalScrollBar").FirstOrDefault();
            var margin = scrollBar.Margin;
            ((FrameworkElement)scrollBar).Margin = new Thickness() { Left = margin.Left, Top = 32, Right = margin.Right, Bottom = margin.Bottom };
        }
        private void InitComposition() {
            var _scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(MainScrollViewer);
            var _compositor = _scrollProperties.Compositor;
            var _expression = _compositor.CreateExpressionAnimation();
            _expression.SetReferenceParameter("ScrollManipulation", _scrollProperties);
            _expression.Expression = "Round(- Min(0, this.Target.Size.Y + ScrollManipulation.Translation.Y - 32))";
            var visual = ElementCompositionPreview.GetElementVisual(LogoMainGrid);
            visual.StartAnimation("Offset.Y", _expression);

        }
        private void MainScrollViewer_Loaded(object sender, RoutedEventArgs e) {
            InitVerticalScrollBarOffset();
        }
    }
    public sealed class DemoModuleTile : Button {
        public DemoModuleTile() {
            DefaultStyleKey = typeof(DemoModuleTile);
        }
    }
}
