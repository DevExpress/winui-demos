using System.Collections.Generic;
using DevExpress.WinUI.Core.Internal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace FeatureDemo.Common.Internal {
    [TemplatePart(Name = "InnerPresenter", Type = typeof(RichTextBlock))]
    public class RichTextPresenter : Control, IRichTextPresenter {
        public static readonly DependencyProperty TextWrappingProperty =
            DependencyPropertyManager.Register<RichTextPresenter, TextWrapping>(d => d.TextWrapping, TextWrapping.Wrap);

        List<Block> savedBlocks;

        public RichTextPresenter() {
            InnerPresenter = new RichTextBlock();
            DefaultStyleKey = typeof(RichTextPresenter);
        }
        public RichTextBlock InnerPresenter { get; private set; }
        public TextWrapping TextWrapping { get { return (TextWrapping)GetValue(TextWrappingProperty); } set { SetValue(TextWrappingProperty, value); } }
        public TextPointer ContentStart {
            get { return InnerPresenter.ContentStart; }
        }
        public TextPointer ContentEnd {
            get { return InnerPresenter.ContentEnd; }
        }
        public TextPointer SelectionStart {
            get { return InnerPresenter.SelectionStart; }
        }
        public TextPointer SelectionEnd {
            get { return InnerPresenter.SelectionEnd; }
        }
        public void Select(TextPointer start, TextPointer end) {
            InnerPresenter.Select(start, end);
        }
        public ICollection<Block> Blocks {
            get { return savedBlocks != null ? (ICollection<Block>)savedBlocks : InnerPresenter.Blocks; }
        }
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            SaveBlocks();
            InnerPresenter = (RichTextBlock)GetTemplateChild("InnerPresenter");
            RestoreBlocks();
        }
        void SaveBlocks() {
            if(InnerPresenter == null) return;
            savedBlocks = new List<Block>();
            foreach(Block block in InnerPresenter.Blocks)
                savedBlocks.Add(block);
            InnerPresenter.Blocks.Clear();
        }
        void RestoreBlocks() {
            if(savedBlocks == null || InnerPresenter == null) return;
            foreach(Block block in savedBlocks)
                InnerPresenter.Blocks.Add(block);
            savedBlocks = null;
        }
    }
}
