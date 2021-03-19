using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Printing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Printing;
using System.Runtime.InteropServices.WindowsRuntime;
using CoreDispatcherPriority = Windows.UI.Core.CoreDispatcherPriority;
using DevExpress.WinUI.Mvvm.UI.Internal;

namespace FeatureDemo.Common {
    public class PrintFrameworkElementBehavior : DevExpress.WinUI.Mvvm.UI.Interactivity.Behavior<FrameworkElement> {
        PrintDocument printDocument;
        public IPrintDocumentSource DocumentSource {
            get { return (IPrintDocumentSource)GetValue(DocumentSourceProperty); }
            set { SetValue(DocumentSourceProperty, value); }
        }
        public static readonly DependencyProperty DocumentSourceProperty =
            DependencyProperty.Register("DocumentSource", typeof(IPrintDocumentSource), typeof(PrintFrameworkElementBehavior), new PropertyMetadata(null));
        protected override void OnAttached() {
            base.OnAttached();
            if(!LayoutHelper.IsElementLoaded(AssociatedObject))
                AssociatedObject.Loaded += AssociatedObjectLoaded;
            else
                UpdateDocumentSource();
        }
        
        void AssociatedObjectLoaded(object sender, RoutedEventArgs e) {
            UpdateDocumentSource();
        }
        void UpdateDocumentSource() {
            ReleaseDocumentSource(false);
            printDocument = new PrintDocument();
            printDocument.Paginate += OnPaginate;
            printDocument.GetPreviewPage += OnGetPreviewPage;
            printDocument.AddPages += OnAddPages;
            DocumentSource = printDocument.DocumentSource;
        }
        void ReleaseDocumentSource(bool releaseDocumentSource) {
            if(printDocument == null)
                return;

            printDocument.Paginate -= OnPaginate;
            printDocument.GetPreviewPage -= OnGetPreviewPage;
            printDocument.AddPages -= OnAddPages;
            printDocument = null;
            if(releaseDocumentSource)
                DocumentSource = null;
        }

        protected override void OnDetaching() {
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
            ReleaseDocumentSource(true);
            base.OnDetaching();
        }
        async Task<Image> RenderPageForPrinting(FrameworkElement fe) {
            var oldTheme = AssociatedObject.RequestedTheme;
            fe.RequestedTheme = ElementTheme.Light;
            try {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(fe);
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();
                var pixels = pixelBuffer.ToArray();

                WriteableBitmap bitmap = new WriteableBitmap(renderTargetBitmap.PixelWidth, renderTargetBitmap.PixelHeight);
                using(Stream stream = bitmap.PixelBuffer.AsStream()) {
                    await stream.WriteAsync(pixels, 0, pixels.Length);
                }

                return new Image() { Source = bitmap };
            } finally {
                AssociatedObject.RequestedTheme = oldTheme;
            }
        }

        async void OnAddPages(object sender, AddPagesEventArgs e) {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {
                var image = await RenderPageForPrinting(AssociatedObject);
                printDocument.AddPage(image);
                printDocument.AddPagesComplete();
            });
        }
        async void OnGetPreviewPage(object sender, GetPreviewPageEventArgs e) {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () => {
                var image = await RenderPageForPrinting(AssociatedObject);
                printDocument.SetPreviewPage(e.PageNumber, image);
            });
        }
        async void OnPaginate(object sender, PaginateEventArgs e) {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                printDocument.SetPreviewPageCount(1, PreviewPageCountType.Final);
            });
        }
    }
}