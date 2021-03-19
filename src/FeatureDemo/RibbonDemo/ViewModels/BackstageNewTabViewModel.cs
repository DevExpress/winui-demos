using System;
using System.Collections.Generic;
using DevExpress.Mvvm;
using Windows.Storage;
using Designer = DevExpress.WinUI.Core.Extensions.UIElementExtensions;

namespace RibbonDemo {
    public class DocumentTemplatePreviewViewModel : ViewModelBase {
        public string DocumentContent {
            get { return GetProperty(() => DocumentContent); }
            set { SetProperty(() => DocumentContent, value); }
        }
        public string PreviewTitle {
            get { return GetProperty(() => PreviewTitle); }
            set { SetProperty(() => PreviewTitle, value); }
        }
        public ICommand<string> NewFileCommand { get; private set; }
        public DocumentTemplatePreviewViewModel(BackstageNewTabViewModel owner) {
            Owner = owner;
            NewFileCommand = new DelegateCommand<string>(OnCreateNewFile);
        }
        public DocumentTemplatePreviewViewModel(BackstageNewTabViewModel owner, string fileName) {
            ReadContentFromFile(fileName);
            Owner = owner;
            NewFileCommand = new DelegateCommand<string>(OnCreateNewFile);
        }

        BackstageNewTabViewModel Owner { get; set; }

        void OnCreateNewFile(string obj) {
            Owner.CreateNewFile(obj);
        }

        async void ReadContentFromFile(string fileName) {
            if(Designer.IsInDesignMode())
                return;
            var uri = new Uri(fileName);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            DocumentContent = await FileIO.ReadTextAsync(file);
        }
    }
    public class BackstageNewTabViewModel : ViewModelBase {
        public IList<DocumentTemplatePreviewViewModel> DocumentTemplates { get; private set; }
        public IRichEditorContentService RichEditorService { get { return GetService<IRichEditorContentService>(); } }
        public BackstageNewTabViewModel() {
            DocumentTemplates = new List<DocumentTemplatePreviewViewModel>() {
                new DocumentTemplatePreviewViewModel(this) {
                    DocumentContent ="",
                    PreviewTitle = "Blank Document"
                },
                new DocumentTemplatePreviewViewModel(this, "ms-appx:///RibbonDemo/Templates/Inital.rtf") {
                    PreviewTitle = "Alice's Adventures"
                }
            };
        }
        public void CreateNewFile(string content) {
            RichEditorService.SetRawContent(content);
            RichEditorService.FileName = "New Document";
        }
    }
}