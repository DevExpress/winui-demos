using FeatureDemo.Common;
using Microsoft.UI.Xaml.Controls;
using System.ComponentModel;

namespace EditorsDemo {
    public sealed partial class MasksModule : DemoModuleView, INotifyPropertyChanged {
        public MasksModule() {
            this.InitializeComponent();
            SelectedModule = ((NavigationViewItem)navigation.MenuItems[0]).Tag as DemoSubModuleView;
        }

        DemoSubModuleView selectedModule;
        public DemoSubModuleView SelectedModule {
            get => selectedModule;
            set {
                if (selectedModule != value) {
                    selectedModule = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedModule)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        void pivot_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args) {
            if(args.InvokedItemContainer.Tag is DemoSubModuleView view)
                SelectedModule = view;
        }
    }
}
