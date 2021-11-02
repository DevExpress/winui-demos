using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Ribbon;
using Microsoft.UI.Text;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Designer = DevExpress.WinUI.Core.Internal.UIElementExtensions;
using DevExpress.Mvvm.CodeGenerators;

namespace RibbonDemo {
    [GenerateViewModel]
    public partial class RibbonToolBarViewModel {
        public RibbonToolBarViewModel() {
            FontViewModel = new FontStyleGroupViewModel(this);
            ParagraphViewModel = new ParagraphStyleGroupViewModel(this);
            InsertViewModel = new InsertGroupViewModel(this);
            Fonts = PopulateFonts();
            IsTextEditingAllowed = true;
            DocumentColors = new ColorPalette() { Title = "Document Colors" };
            ReadContentFromFile("ms-appx:///RibbonDemo/Templates/Inital.rtf", "Alice's Adventures in Wonderland");
        }

        public FontStyleGroupViewModel FontViewModel { get; }
        public ParagraphStyleGroupViewModel ParagraphViewModel { get; }
        public InsertGroupViewModel InsertViewModel { get; }
        public List<string> Fonts { get; }

        [GenerateProperty]
        bool _IsTextEditingAllowed;

        [GenerateProperty]
        bool _IsBackstageOpen;

        [GenerateProperty]
        string _CurrentDocumentText;

        [GenerateProperty]
        string _DefaultContent;

        [GenerateProperty]
        string _DocumentName;

        [GenerateProperty]
        ParagraphAlignment _CurrentSelectionAlignment;

        [GenerateProperty]
        bool _AlignLeft;
        void OnAlignLeftChanged() => CurrentSelectionAlignment = (AlignLeft ? ParagraphAlignment.Left : CurrentSelectionAlignment);

        [GenerateProperty]
        bool _AlignRight;
        void OnAlignRightChanged() => CurrentSelectionAlignment = (AlignRight ? ParagraphAlignment.Right : CurrentSelectionAlignment);

        [GenerateProperty]
        bool _AlignCenter;
        void OnAlignCenterChanged() => CurrentSelectionAlignment = (AlignCenter ? ParagraphAlignment.Center : CurrentSelectionAlignment);

        [GenerateProperty]
        bool _AlignJustify;
        void OnAlignJustifyChanged() => CurrentSelectionAlignment = (AlignJustify ? ParagraphAlignment.Center : CurrentSelectionAlignment);

        public ColorPalette DocumentColors { get; }
        public object Service { get; set; }
        protected IRichEditorCommonActionsService RichEditorService => Service as IRichEditorCommonActionsService;
        protected IRichEditorContentService RichEditorContentService => Service as IRichEditorContentService;
       

        List<string> PopulateFonts() {
            return new List<string> {
                                      "Arial", "Calibri", "Cambria", "Cambria Math", "Comic Sans MS", "Courier New",
                                      "Ebrima", "Gadugi", "Georgia", "Leelawadee UI",
    "Lucida Console", "Malgun Gothic", "Microsoft Himalaya", "Microsoft JhengHei",
    "Microsoft JhengHei UI", "Microsoft New Tai Lue", "Microsoft PhagsPa",
    "Microsoft Tai Le", "Microsoft YaHei", "Microsoft YaHei UI",
    "Microsoft Yi Baiti", "Mongolian Baiti", "MV Boli", "Myanmar Text",
    "Nirmala UI", "Segoe Print", "Segoe UI",
    "Segoe UI Historic", "Segoe UI Symbol", "SimSun", "Times New Roman",
    "Trebuchet MS", "Verdana", "Yu Gothic",
    "Yu Gothic UI"
            };

        }
        protected virtual void OnCurrentDocumentTextChanged() {
            UndoCommand.RaiseCanExecuteChanged();
            RedoCommand.RaiseCanExecuteChanged();
        }
        async void ReadContentFromFile(string filePath, string fileName) {
            if (Designer.IsInDesignMode())
                return;

            var uri = new Uri(filePath);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            CurrentDocumentText = await FileIO.ReadTextAsync(file);
            if (string.IsNullOrEmpty(fileName))
                DocumentName = file.DisplayName;
            else
                DocumentName = fileName;
            await Task.Yield();
        }

        [GenerateCommand]
        void Redo() {
            RichEditorService.Redo();
            RedoCommand.RaiseCanExecuteChanged();
            UndoCommand.RaiseCanExecuteChanged();
        }
        bool CanRedo() => (RichEditorService != null) ? RichEditorService.CanRedo() : false;

        [GenerateCommand]
        void Undo() {
            RichEditorService.Undo();
            RedoCommand.RaiseCanExecuteChanged();
            (UndoCommand as DelegateCommand<object>).Do(x => x.RaiseCanExecuteChanged());
        }
        bool CanUndo() => (RichEditorService != null) ? RichEditorService.CanUndo() : false;

        [GenerateCommand]
        void Cut() => RichEditorService.Cut();

        [GenerateCommand]
        void Paste() => RichEditorService.Paste();

        [GenerateCommand]
        void Copy() => RichEditorService.Copy();
    }
}
