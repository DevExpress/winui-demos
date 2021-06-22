using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using DevExpress.WinUI.Core.Internal;
using Windows.Foundation;
using Windows.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using FeatureDemo.Common.Internal;
using FeatureDemo.ViewModel;
using TextRange = FeatureDemo.Common.Internal.TextRange;

namespace FeatureDemo.Common {
    public class CustomTextPointer {
        internal TextPointer TextPointer { get; set; }
        public CustomTextPointer(TextPointer textPointer) {
            this.TextPointer = textPointer;
        }
        public LogicalDirection LogicalDirection { get { return TextPointer.LogicalDirection; } }
        public int Offset { get { return TextPointer.Offset; } }
        public DependencyObject Parent { get { return TextPointer.Parent; } }
        public FrameworkElement VisualParent { get { return TextPointer.VisualParent; } }
        public Rect GetCharacterRect(LogicalDirection direction) {
            return TextPointer.GetCharacterRect(direction);
        }
        public TextPointer GetPositionAtOffset(int offset,LogicalDirection direction) {
            return TextPointer.GetPositionAtOffset(offset,direction);
        }
        public static implicit operator CustomTextPointer(TextPointer textPointer) {
            return new CustomTextPointer(textPointer);
        }
        public static implicit operator TextPointer(CustomTextPointer customTextPointer) {
            return customTextPointer != null ? customTextPointer.TextPointer : null;
        }
    }
    public class CodeViewer : Control {
        #region static
        public static readonly DependencyProperty SourceProperty;
        static CodeViewer() {
            SourceProperty = DependencyProperty.Register("Source", typeof(SourceFileViewModel), typeof(CodeViewer), new PropertyMetadata(null, OnSourcePropertyChanged));
        }
        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((CodeViewer)d).OnSourceChanged();
        #endregion
        #region Dep Props
        public SourceFileViewModel Source {
            get { return (SourceFileViewModel)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        #endregion

        Dictionary<TextRange, Rectangle> highlights = new Dictionary<TextRange, Rectangle>();

        public CodeViewer() {
            this.AddHandler(RichTextPresenter.KeyDownEvent, (KeyEventHandler)OnKeyDown, true);
            DefaultStyleKey = typeof(CodeViewer);
        }
        public IRichTextPresenter TextPresenter { get; private set; }
        protected ScrollViewer ScrollViewer { get; private set; }
        private void OnSourceChanged() {
            UpdateTextPresenter();
        }

        protected virtual void UpdateTextPresenter() {
            if(Source == null || TextPresenter == null) return;
            RichTextHelper.SetText(TextPresenter, Source.GetContent(), Source.Language);
        }

        void OnKeyDown(object sender, KeyRoutedEventArgs e) {
            const double scrollDiff = 15.0; 
            switch(e.Key) {
                case VirtualKey.Up:
                    ScrollViewer.ChangeView(null, ScrollViewer.VerticalOffset - scrollDiff, null, true);
                    break;
                case VirtualKey.Down:
                    ScrollViewer.ChangeView(null, ScrollViewer.VerticalOffset + scrollDiff, null, true);
                    break;
                case VirtualKey.Left:
                    ScrollViewer.ChangeView(ScrollViewer.HorizontalOffset - scrollDiff, null, null, true);
                    break;
                case VirtualKey.Right:
                    ScrollViewer.ChangeView(ScrollViewer.HorizontalOffset + scrollDiff, null, null, true);
                    break;
                case VirtualKey.Home:
                    ScrollViewer.ChangeView(null, 0.0, null, true);
                    break;
                case VirtualKey.End:
                    ScrollViewer.ChangeView(null, ScrollViewer.ExtentHeight, null, true);
                    break;
                case VirtualKey.PageUp:
                    ScrollViewer.ChangeView(null, ScrollViewer.VerticalOffset - ScrollViewer.ActualHeight, null, true);
                    break;
                case VirtualKey.PageDown:
                    ScrollViewer.ChangeView(null, ScrollViewer.VerticalOffset + ScrollViewer.ActualHeight, null, true);
                    break;
            }
        }

        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            TextPresenter = (RichTextPresenter)GetTemplateChild("TextPresenter");
            ScrollViewer = (ScrollViewer)GetTemplateChild("ScrollViewer");
            UpdateTextPresenter();
        }
        protected override Size ArrangeOverride(Size size) {
            size = base.ArrangeOverride(size);
            Clip = new RectangleGeometry() { Rect = new Rect(new Point(), size) };
            return size;
        }
    }
}
