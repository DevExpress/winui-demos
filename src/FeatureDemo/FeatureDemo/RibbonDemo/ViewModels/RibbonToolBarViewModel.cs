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

namespace RibbonDemo {
    public class RibbonToolBarViewModel : ViewModelBase {

        public RibbonToolBarViewModel() {
            FontViewModel = new FontStyleGroupViewModel(this);
            ParagraphViewModel = new ParagraphStyleGroupViewModel(this);
            InsertViewModel = new InsertGroupViewModel(this);
            Fonts = PopulateFonts();
            CopyCommand = new DelegateCommand(OnCopy);
            PasteCommand = new DelegateCommand(OnPaste);
            CutCommand = new DelegateCommand(OnCut);
            UndoCommand = new DelegateCommand(OnUndo, CanUndo);
            RedoCommand = new DelegateCommand(OnRedo, CanRedo);
            IsTextEditingAllowed = true;
            RibbonViewMode = RibbonViewMode.Normal;
            DocumentColors = new ColorPalette() { Title = "Document Colors" };
            ReadContentFromFile("ms-appx:///RibbonDemo/Templates/Inital.rtf", "Alice's Adventures in Wonderland");
        }

        public FontStyleGroupViewModel FontViewModel { get; }
        public ParagraphStyleGroupViewModel ParagraphViewModel { get; }
        public InsertGroupViewModel InsertViewModel { get; }
        public List<string> Fonts { get; }
        public bool IsTextEditingAllowed {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public RibbonViewMode RibbonViewMode {
            get => GetValue<RibbonViewMode>();
            set => SetValue(value);
        }
        public bool IsBackstageOpen {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public string CurrentDocumentText {
            get => GetValue<string>();
            set => SetValue(value, () => OnCurrentDocumentTextChanged());
        }
        public string DefaultContent {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string DocumentName {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public ParagraphAlignment CurrentSelectionAlignment{
            get => GetValue<ParagraphAlignment>();
            set => SetValue(value);
        }
        public bool AlignLeft {
            get => GetValue<bool>();
            set => SetValue(value, () => CurrentSelectionAlignment = value ? ParagraphAlignment.Left : CurrentSelectionAlignment);
        }
        public bool AlignRight {
            get => GetValue<bool>();
            set => SetValue(value, () => CurrentSelectionAlignment = value ? ParagraphAlignment.Right : CurrentSelectionAlignment);
        }
        public bool AlignCenter {
            get => GetValue<bool>();
            set => SetValue(value, () => CurrentSelectionAlignment = value ? ParagraphAlignment.Center : CurrentSelectionAlignment);
        }
        public bool AlignJustify {
            get => GetValue<bool>();
            set => SetValue(value, () => CurrentSelectionAlignment = value ? ParagraphAlignment.Justify : CurrentSelectionAlignment);
        }
        public ColorPalette DocumentColors { get; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
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
            (UndoCommand as DelegateCommand).RaiseCanExecuteChanged();
            (RedoCommand as DelegateCommand).RaiseCanExecuteChanged();
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

        bool CanRedo() => (RichEditorService != null) ? RichEditorService.CanRedo() : false;
        bool CanUndo() => (RichEditorService != null) ? RichEditorService.CanUndo() : false;
        void OnRedo() {
            RichEditorService.Redo();
            (RedoCommand as DelegateCommand<object>).Do(x => x.RaiseCanExecuteChanged());
            (UndoCommand as DelegateCommand<object>).Do(x => x.RaiseCanExecuteChanged());
        }
        void OnUndo() {
            RichEditorService.Undo();
            (RedoCommand as DelegateCommand<object>).Do(x => x.RaiseCanExecuteChanged());
            (UndoCommand as DelegateCommand<object>).Do(x => x.RaiseCanExecuteChanged());
        }
        void OnCut() { RichEditorService.Cut(); }
        void OnPaste() { RichEditorService.Paste(); }
        void OnCopy() { RichEditorService.Copy(); }
    }
}
