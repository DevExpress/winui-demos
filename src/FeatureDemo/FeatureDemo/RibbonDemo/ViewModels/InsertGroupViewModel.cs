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

namespace RibbonDemo {
    public class InsertGroupViewModel : ViewModelBase {
        public InsertGroupViewModel(RibbonToolBarViewModel parent){
            ((ISupportParentViewModel)this).ParentViewModel = parent;
            InsertImageCommand = new DelegateCommand(OnInsertImage);
        }

        public ICommand InsertImageCommand { get; }
        RibbonToolBarViewModel Parent => ((ISupportParentViewModel)this).ParentViewModel as RibbonToolBarViewModel;
        public IRichEditorInsertService RichEditorService => Parent.Service as IRichEditorInsertService; 

        async void OnInsertImage() {
            var xamlRoot = CurrentWindowHelper.CurrentWindow?.Content?.XamlRoot;
            if(xamlRoot != null) {
                var dlg = new ContentDialog() {
                    Title = "Result Dialog",
                    Content = "The InsertImageCommand executed!",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                };
                await dlg.ShowAsync().AsTask();
            }

            
            

            
            

            
            
            
            
            
            
            
            
            
        }
    }
}