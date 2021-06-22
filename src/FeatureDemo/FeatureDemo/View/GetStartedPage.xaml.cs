using DevExpress.WinUI.Core.Internal;
using FeatureDemo.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Hosting;
using System.Linq;

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
