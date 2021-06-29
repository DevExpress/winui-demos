using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using Microsoft.UI;
using Windows.UI.Text;
using Microsoft.UI.Xaml.Media;
using DevExpress.WinUI.Core;
using Microsoft.UI.Xaml;
using DevExpress.WinUI.Ribbon;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using DevExpress.Mvvm.Native;
using Windows.Storage.Pickers;
using DevExpress.Mvvm.UI;
using DevExpress.WinUI.Core.Internal;
using Microsoft.UI.Xaml.Controls;
using System.Runtime.InteropServices;
using WinRT;

namespace RibbonDemo {
    public class InsertGroupViewModel : ViewModelBase {
        public InsertGroupViewModel(RibbonToolBarViewModel parent){
            ((ISupportParentViewModel)this).ParentViewModel = parent;
            InsertImageCommand = new DelegateCommand(OnInsertImage);
        }

        public ICommand InsertImageCommand { get; }
        RibbonToolBarViewModel Parent => ((ISupportParentViewModel)this).ParentViewModel as RibbonToolBarViewModel;
        public IRichEditorInsertService RichEditorService => Parent.Service as IRichEditorInsertService;

        
        [ComImport, System.Runtime.InteropServices.Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow {
            void Initialize([In] IntPtr hwnd);
        }
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto, PreserveSig = true, SetLastError = false)]
        public static extern IntPtr GetActiveWindow();

        async void OnInsertImage() {
            FileOpenPicker filePicker = new FileOpenPicker();
            if(Window.Current == null) {
                var wrapper = filePicker.As<IInitializeWithWindow>();
                IntPtr hwnd = GetActiveWindow();
                wrapper.Initialize(hwnd);
            }

            foreach(string ext in new[] { ".jpeg", ".jpg", ".png", ".bmp" })
                filePicker.FileTypeFilter.Add(ext);

            filePicker.ViewMode = PickerViewMode.Thumbnail;
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.SettingsIdentifier = "ImageFilePicker";
            filePicker.CommitButtonText = "Insert";
            StorageFile file = await filePicker.PickSingleFileAsync();
            if(null != file) {
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                RichEditorService.Do(x => x.InsertImage(fileStream as FileRandomAccessStream));
            }
        }
    }
}