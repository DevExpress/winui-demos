using DevExpress.Mvvm;
using FeatureDemo.DataModel;
using System.Collections.ObjectModel;
using System.Linq;


namespace FeatureDemo.ViewModel {
    public class DemoModuleViewModel : NavigationViewModelBase {
        #region properties
        public DemoModuleGroup Group { get; protected set; }
        public DemoModule DemoModule { get; protected set; }
        public MainViewModel MainViewModel { get; protected set; }
        public ObservableCollection<SourceFileViewModel> SourceList { get; } = new ObservableCollection<SourceFileViewModel>();
        public SourceFileViewModel SelectedSource {
            get => GetValue<SourceFileViewModel>();
            set => SetValue(value);
        }
        public bool IsVisible{
            get => GetValue<bool>();
            set => SetValue(value);
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