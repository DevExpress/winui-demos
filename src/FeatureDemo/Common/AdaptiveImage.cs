using System;
using Windows.Foundation;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FeatureDemo.Common {
    public class AdaptiveImage : Panel {
        public Uri SourceUri {
            get { return (Uri)GetValue(SourceUriProperty); }
            set { SetValue(SourceUriProperty, value); }
        }

        public static readonly DependencyProperty SourceUriProperty =
            DependencyProperty.Register(nameof(SourceUri), typeof(Uri), typeof(AdaptiveImage), new PropertyMetadata(null, OnSourceUriPropertyChanged));

        private static void OnSourceUriPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((AdaptiveImage)d).OnSourceUriChanged();
        }
        public AdaptiveImage() {
            SizeChanged += ImageContainer_SizeChanged;
        }

        private void ImageContainer_SizeChanged(object sender, SizeChangedEventArgs e) {
            if(Background is ImageBrush) {
                ((BitmapImage)((ImageBrush)Background).ImageSource).DecodePixelWidth = (int)e.NewSize.Width;
                ((BitmapImage)((ImageBrush)Background).ImageSource).DecodePixelHeight = (int)e.NewSize.Height;
            }
        }
        protected override Size MeasureOverride(Size availableSize) {
            return new Size(availableSize.Width, Math.Round(availableSize.Width * 9 / 16));
        }
        private void OnSourceUriChanged() {
            Background = new ImageBrush() { ImageSource = new BitmapImage(SourceUri) { DecodePixelType = DecodePixelType.Logical } };
        }
    }
}