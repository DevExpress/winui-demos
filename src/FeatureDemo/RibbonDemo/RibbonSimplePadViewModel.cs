using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core;
using DevExpress.WinUI.Ribbon;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Text;
using Designer = DevExpress.WinUI.Core.Extensions.UIElementExtensions;
using ICommand = Microsoft.UI.Xaml.Input.ICommand;

namespace RibbonDemo {
    public class AlignmentEnumToBooleanConverter : EnumToBooleanConverter<ParagraphAlignment> { }
    public class RibbonViewModeToBooleanConverter : EnumToBooleanConverter<RibbonViewMode> { }
    public class RibbonViewModel : ViewModelBase {
        public FontStyleGroupViewModel FontViewModel { get; }
        public ParagraphStyleGroupViewModel ParagraphViewModel { get; }
        public InsertGroupViewModel InsertViewModel { get; }
        public List<string> Fonts { get; }
        public bool IsTextEditingAllowed {
            get { return GetProperty(() => IsTextEditingAllowed); }
            set { SetProperty(() => IsTextEditingAllowed, value); }
        }
        public RibbonViewMode RibbonViewMode {
            get { return GetProperty(() => RibbonViewMode); }
            set { SetProperty(() => RibbonViewMode, value); }
        }
        public bool IsBackstageOpen {
            get { return GetProperty(() => IsBackstageOpen); }
            set { SetProperty(() => IsBackstageOpen, value); }
        }
        public string CurrentDocumentText {
            get { return GetProperty(() => CurrentDocumentText); }
            set { SetProperty(() => CurrentDocumentText, value, () => OnCurrentDocumentTextChanged()); }
        }
        public string DefaultContent {
            get { return GetProperty(() => DefaultContent); }
            set { SetProperty(() => DefaultContent, value); }
        }
        public string DocumentName {
            get { return GetProperty(() => DocumentName); }
            set { SetProperty(() => DocumentName, value); }
        }

        public ColorPalette DocumentColors { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand PasteCommand { get; private set; }
        public ICommand CutCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand<bool> SaveFileCommand { get; private set; }
        protected IRichEditorCommonActionsService RichEditorService { get { return GetService<IRichEditorCommonActionsService>(); } }
        protected IRichEditorContentService RichEditorContentService { get { return GetService<IRichEditorContentService>(); } }

        List<string> PopulateFonts()
        {
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

        public RibbonViewModel() {
            FontViewModel = new FontStyleGroupViewModel(this);
            ParagraphViewModel = new ParagraphStyleGroupViewModel(this);
            InsertViewModel = new InsertGroupViewModel(this);
            Fonts = PopulateFonts();
            CopyCommand = new DelegateCommand(OnCopy);
            PasteCommand = new DelegateCommand(OnPaste);
            CutCommand = new DelegateCommand(OnCut);
            UndoCommand = new DelegateCommand(OnUndo, CanUndo);
            RedoCommand = new DelegateCommand(OnRedo, CanRedo);
            SaveFileCommand = new DelegateCommand<bool>(OnSave);
            IsTextEditingAllowed = true;
            RibbonViewMode = RibbonViewMode.Normal;
            DocumentColors = new ColorPalette() { Title = "Document Colors" };
            ReadContentFromFile("ms-appx:///RibbonDemo/Templates/Inital.rtf", "Alice's Adventures in Wonderland");
        }

        protected virtual void OnCurrentDocumentTextChanged() {
            (UndoCommand as DelegateCommand).RaiseCanExecuteChanged();
            (RedoCommand as DelegateCommand).RaiseCanExecuteChanged();
        }
        async void ReadContentFromFile(string filePath, string fileName) {
            if(Designer.IsInDesignMode())
                return;

            var uri = new Uri(filePath);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            CurrentDocumentText = await FileIO.ReadTextAsync(file);
            if(string.IsNullOrEmpty(fileName))
                DocumentName = file.DisplayName;
            else
                DocumentName = fileName;
        }

        async void OnSave(bool saveAs) {
            StorageFile File = null;
            if (saveAs || RichEditorContentService.FilePath == string.Empty) {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                savePicker.FileTypeChoices.Add("Rich Text", new List<string>() { ".rtf" });
                savePicker.SuggestedFileName = RichEditorContentService.FileName;
                File = await savePicker.PickSaveFileAsync();
            } else {
                File = await StorageFile.GetFileFromPathAsync(RichEditorContentService.FilePath);
            }
            if(File != null) {
                RichEditorContentService.FileName = File.DisplayName;
                RichEditorContentService.FilePath = File.Path;
                CachedFileManager.DeferUpdates(File);
                using (var outStream = await File.OpenAsync(FileAccessMode.ReadWrite)) {
                    RichEditorContentService.Do(x => x.SaveFileToStream(outStream));
                }
                await CachedFileManager.CompleteUpdatesAsync(File);
            }
        }

        bool CanRedo() {
            return (RichEditorService != null) ? RichEditorService.CanRedo() : false;
        }
        bool CanUndo() {
            return (RichEditorService != null) ? RichEditorService.CanUndo() : false;
        }
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
