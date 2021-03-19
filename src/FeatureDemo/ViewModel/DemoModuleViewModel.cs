using DevExpress.Mvvm;
using FeatureDemo.DataModel;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using System.Linq;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;
using FeatureDemo.Common;
using Windows.Phone.UI.Input;
using Windows.ApplicationModel.Resources.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using DevExpress.WinUI.Core.Collections.Internal;

namespace FeatureDemo.ViewModel {
    public class DemoModuleViewModel : NavigationViewModelBase {
        #region properties
        public DemoModuleGroup Group { get; protected set; }
        public DemoModule DemoModule { get; protected set; }
        public MainViewModel MainViewModel { get; protected set; }
        public WorkaroundObservableCollection<SourceFileViewModel> SourceList { get; } = new WorkaroundObservableCollection<SourceFileViewModel>();
        public SourceFileViewModel SelectedSource {
            get => GetProperty<SourceFileViewModel>();
            set => SetProperty(value);
        }
        public bool IsVisible{
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        #endregion
        public DemoModuleViewModel(MainViewModel viewModel, DemoModule demoModule) {
            ((DevExpress.Mvvm.ISupportParentViewModel)this).ParentViewModel = viewModel;
            MainViewModel = viewModel;
            Group = demoModule.Group;
            DemoModule = demoModule;
        }        
        public void InitializeSourceList() {
            if(SourceList.Count != 0) {
                if(SelectedSource == null)
                    SelectedSource = SourceList[0];
                return;
            }
            
            SourceList.Add(new SourceFileViewModel(DemoModule.ViewTypeName + ".xaml"));
            SourceList.Add(new SourceFileViewModel(DemoModule.ViewTypeName + ".xaml.cs"));
            if(DemoModule.Sources != null) {
                foreach(string source in DemoModule.Sources)
                    SourceList.Add(new SourceFileViewModel(source));
            }
            
            SelectedSource = SourceList[0];
        }
    }
}