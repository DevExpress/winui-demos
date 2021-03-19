using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using System;
using System.Numerics;

namespace FeatureDemo.Common {
    public class ShadowRect : FrameworkElement {
        #region static
        public static readonly DependencyProperty FillProperty;
        public static readonly DependencyProperty ShadowBrushProperty;
        public static readonly DependencyProperty RadiusXProperty;
        public static readonly DependencyProperty RadiusYProperty;
        public static readonly DependencyProperty ShadowBlurRadiusProperty;
        public static readonly DependencyProperty ShadowOpacityProperty;
        public static readonly DependencyProperty ShadowOffsetXProperty;
        public static readonly DependencyProperty ShadowOffsetYProperty;

        static ShadowRect() {
            FillProperty = DependencyProperty.Register(nameof(Fill), typeof(SolidColorBrush), typeof(ShadowRect), new PropertyMetadata(null, OnFillPropertyChanged));
            ShadowBrushProperty = DependencyProperty.Register(nameof(ShadowBrush), typeof(SolidColorBrush), typeof(ShadowRect), new PropertyMetadata(null, OnShadowBrushPropertyChanged));
            RadiusXProperty = DependencyProperty.Register(nameof(RadiusX), typeof(double), typeof(ShadowRect), new PropertyMetadata(0d, OnRadiusXPropertyChanged));
            RadiusYProperty = DependencyProperty.Register(nameof(RadiusY), typeof(double), typeof(ShadowRect), new PropertyMetadata(0d, OnRadiusYPropertyChanged));
            ShadowBlurRadiusProperty = DependencyProperty.Register(nameof(ShadowBlurRadius), typeof(double), typeof(ShadowRect), new PropertyMetadata(4d, OnShadowBlurRadiusPropertyChanged));
            ShadowOpacityProperty = DependencyProperty.Register(nameof(ShadowOpacity), typeof(double), typeof(ShadowRect), new PropertyMetadata(0.5d, OnShadowOpacityPropertyChanged));
            ShadowOffsetXProperty = DependencyProperty.Register(nameof(ShadowOffsetX), typeof(double), typeof(ShadowRect), new PropertyMetadata(0d, OnShadowOffsetXPropertyChanged));
            ShadowOffsetYProperty = DependencyProperty.Register(nameof(ShadowOffsetY), typeof(double), typeof(ShadowRect), new PropertyMetadata(2d, OnShadowOffsetYPropertyChanged));
        }
        private static void OnFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnFillChanged(e.OldValue as SolidColorBrush);
        private static void OnShadowBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnShadowBrushChanged(e.OldValue as SolidColorBrush);
        private static void OnRadiusXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnRadiusXChanged((double)e.OldValue);
        private static void OnRadiusYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnRadiusYChanged((double)e.OldValue);
        private static void OnShadowBlurRadiusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnShadowBlurRadiusChanged((double)e.OldValue);
        private static void OnShadowOpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnShadowOpacityChanged((double)e.OldValue);
        private static void OnShadowOffsetXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnShadowOffsetXChanged((double)e.OldValue);
        private static void OnShadowOffsetYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((ShadowRect)d).OnShadowOffsetYChanged((double)e.OldValue);
        #endregion
        #region dep props
        public SolidColorBrush Fill {
            get { return (SolidColorBrush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        public SolidColorBrush ShadowBrush {
            get { return (SolidColorBrush)GetValue(ShadowBrushProperty); }
            set { SetValue(ShadowBrushProperty, value); }
        }
        public double RadiusX {
            get { return (double)GetValue(RadiusXProperty); }
            set { SetValue(RadiusXProperty, value); }
        }
        public double RadiusY {
            get { return (double)GetValue(RadiusYProperty); }
            set { SetValue(RadiusYProperty, value); }
        }
        public double ShadowBlurRadius {
            get { return (double)GetValue(ShadowBlurRadiusProperty); }
            set { SetValue(ShadowBlurRadiusProperty, value); }
        }
        public double ShadowOpacity {
            get { return (double)GetValue(ShadowOpacityProperty); }
            set { SetValue(ShadowOpacityProperty, value); }
        }
        public double ShadowOffsetX {
            get { return (double)GetValue(ShadowOffsetXProperty); }
            set { SetValue(ShadowOffsetXProperty, value); }
        }
        public double ShadowOffsetY {
            get { return (double)GetValue(ShadowOffsetYProperty); }
            set { SetValue(ShadowOffsetYProperty, value); }
        }
        #endregion

        DropShadow dropShadow;
        SpriteVisual spriteVisual;
        Compositor compositor;
        CompositionBrush mask;

        public ShadowRect() {
            IsHitTestVisible = false;
            compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            dropShadow = compositor.CreateDropShadow();
            spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Shadow = dropShadow;
            Loaded += OnLoaded;
            SizeChanged += OnSizeChanged;
        }

        

        private void OnFillChanged(SolidColorBrush oldValue) {
            if(!isLoaded) return;
            UpdateSpriteVisualBrush();
        }
        private void OnShadowBrushChanged(SolidColorBrush oldValue) {
            if(!isLoaded) return;
            UpdateDropShadowColor();
        }
        private void OnRadiusXChanged(double oldValue) {
            if(!isLoaded) return;
            UpdateMask();
            UpdateSpriteVisualBrush();
            UpdateDropShadowMask();
        }
        private void OnShadowBlurRadiusChanged(double oldValue) {
            if(!isLoaded) return;
            UpdateDropShadowBlurRadius();
        }
        private void OnShadowOpacityChanged(double oldValue) {
            if(!isLoaded) return;
            UpdateDropShadowOpacity();
        }
        private void OnRadiusYChanged(double oldValue) {
            if(!isLoaded) return;
            UpdateMask();
            UpdateSpriteVisualBrush();
            UpdateDropShadowMask();
        }
        private void OnShadowOffsetXChanged(double oldValue) {
            if(!isLoaded) return;
            UpdateDropShadowOffset();
        }
        private void OnShadowOffsetYChanged(double oldValue) {
            if(!isLoaded) return;
            UpdateDropShadowOffset();
        }
        void UpdateMask() => mask = CreateMask(compositor, spriteVisual.Size, new Vector2((float)RadiusX, (float)RadiusY));
        void UpdateDropShadowMask() => dropShadow.Mask = mask;
        void UpdateDropShadowOffset() => dropShadow.Offset = new Vector3((float)ShadowOffsetX, (float)ShadowOffsetY, 0F);
        void UpdateDropShadowBlurRadius() => dropShadow.BlurRadius = (float)ShadowBlurRadius;
        void UpdateDropShadowOpacity() => dropShadow.Opacity = (float)ShadowOpacity;

        void UpdateSpriteVisualBrush() {
            if(Fill == null) {
                spriteVisual.Brush = null;
            }
            else {
                var maskBrush = compositor.CreateMaskBrush();
                maskBrush.Source = compositor.CreateColorBrush(Fill.Color);
                maskBrush.Mask = mask;
                spriteVisual.Brush = maskBrush;
            }
        }
        void UpdateDropShadowColor() {
            if(ShadowBrush == null) {
                dropShadow.Color = Colors.Black;
            }
            else {
                dropShadow.Color = ShadowBrush.Color;
            }

        }
        bool isLoaded;
        private void OnLoaded(object sender, RoutedEventArgs e) {
            UpdateDropShadowColor();
            UpdateDropShadowBlurRadius();
            UpdateDropShadowOpacity();
            UpdateDropShadowOffset();

            UpdateSpriteVisualSize();
            UpdateMask();
            UpdateSpriteVisualBrush();
            UpdateDropShadowMask();

            ElementCompositionPreview.SetElementChildVisual(this, spriteVisual);
            isLoaded = true;
        }
        private void OnSizeChanged(object sender, SizeChangedEventArgs e) {
            if(!IsLoaded) return;
            UpdateSpriteVisualSize();
            UpdateMask();
            UpdateSpriteVisualBrush();
            UpdateDropShadowMask();
        }
        void UpdateSpriteVisualSize() => spriteVisual.Size = new Vector2((float)this.ActualWidth, (float)this.ActualHeight);
        CompositionBrush CreateMask(Compositor compositor, Vector2 size, Vector2 cornerRadius) {
            CompositionRoundedRectangleGeometry roundedRectangle = compositor.CreateRoundedRectangleGeometry();
            roundedRectangle.Size = size;
            roundedRectangle.CornerRadius = cornerRadius;

            CompositionSpriteShape spriteShape = compositor.CreateSpriteShape(roundedRectangle);
            spriteShape.FillBrush = compositor.CreateColorBrush(Colors.Black);
            spriteShape.CenterPoint = new Vector2(size.X / 2, size.Y / 2);

            ShapeVisual spriteShapeVisual = compositor.CreateShapeVisual();
            spriteShapeVisual.BorderMode = CompositionBorderMode.Soft;
            spriteShapeVisual.Size = size;
            spriteShapeVisual.Shapes.Add(spriteShape);

            CompositionVisualSurface surface = compositor.CreateVisualSurface();
            surface.SourceSize = size;
            surface.SourceVisual = spriteShapeVisual;

            return compositor.CreateSurfaceBrush(surface);
        }
    }
}