using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Mvvm;
using Windows.Storage;
using Windows.Storage.Streams;
using DevExpress.Mvvm.Native;
using Windows.Storage.Pickers;
using ICommand = Microsoft.UI.Xaml.Input.ICommand;

namespace RibbonDemo {
    public class RecentDocsList : ObservableCollection<RecentDocumentViewModel> {
    }
    public class RecentDocumentViewModel : ViewModelBase {
        public string Title {
            get { return GetProperty(() => Title); }
            set { SetProperty(() => Title, value); }
        }
        public DateTime DateCreate {
            get { return GetProperty(() => DateCreate); }
            set { SetProperty(() => DateCreate, value); }
        }
        public string Path {
            get { return GetProperty(() => Path); }
            set { SetProperty(() => Path, value); }
        }
    }
    public class RecentDocumentsGroupViewModel : ViewModelBase {
        public RecentDocsList Documents { get; set; }

        public string Title {
            get { return GetProperty(() => Title); }
            set { SetProperty(() => Title, value); }
        }

        public bool IsCollapsed {
            get { return GetProperty(() => IsCollapsed); }
            set { SetProperty(() => IsCollapsed, value); }
        }
    }
    public class BackstageOpenTabViewModel : ViewModelBase {
        public IList<RecentDocumentsGroupViewModel> RecentGroups { get; private set; }
        public ICommand OpenFileCommand { get; private set; }
        public IRichEditorContentService RichEditorService { get { return GetService<IRichEditorContentService>(); } }
        public BackstageOpenTabViewModel() {
            OpenFileCommand = new DelegateCommand(OnOpenFile);
            RecentGroups = new List<RecentDocumentsGroupViewModel>() {
                new RecentDocumentsGroupViewModel() {
                    Documents = new RecentDocsList() {
                        new RecentDocumentViewModel() {
                            DateCreate = DateTime.Now,
                            Path = "C:\\users\\UserName\\Document.rtf",
                            Title = "Document"
                        },
                        new RecentDocumentViewModel() {
                            DateCreate = DateTime.Now,
                            Path = "C:\\users\\UserName\\Document1.rtf",
                            Title = "Document1"
                        },
                    },
                    Title = "Today",
                },
                new RecentDocumentsGroupViewModel() {
                    Documents = new RecentDocsList() {
                        new RecentDocumentViewModel() {
                            DateCreate = DateTime.Now,
                            Path = "C:\\users\\UserName\\Document2.rtf",
                            Title = "Document2"
                        },
                        new RecentDocumentViewModel() {
                            DateCreate = DateTime.Now,
                            Path = "C:\\users\\UserName\\Document3.rtf",
                            Title = "Document3"
                        },
                    },
                    Title = "Older",
                },
            };
        }

        async void OnOpenFile() {
            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            open.FileTypeFilter.Add(".rtf");
            StorageFile file = await open.PickSingleFileAsync();
            if(file != null) {
                IRandomAccessStream randAccStream = await file.OpenAsync(FileAccessMode.Read);
                RichEditorService.Do(x => x.OpenFileFromStream(randAccStream));
                RichEditorService.FileName = file.DisplayName;
                RichEditorService.FilePath = file.Path;
            }
        }
    }
}