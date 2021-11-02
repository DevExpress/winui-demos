using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using FeatureDemo.Common;
using FeatureDemo.DataModel;
using DevExpress.WinUI.Core.Internal;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using System;
using DevExpress.Mvvm.CodeGenerators;

namespace FeatureDemo.ViewModel {
    [GenerateViewModel]
    public partial class MainViewModel : NavigationViewModelBase {
        const string UriScheme = "dx-winui-demo://";
        public static MainViewModel Instance { get; } = new MainViewModel();

        #region services
        public IDemoNavigationService NavigationService { get; set; }
        #endregion

        #region properties
        
        [GenerateProperty]
        double windowTitleOpacity;

        [GenerateProperty]
        bool _PrevNextEnabled;

        public Action<DemoModuleGroupMenuItem> ExpandGroupCallback { get; set; }
        public List<ProductGroupViewModel> ProductGroups { get; }
        public List<MenuItemViewModelBase> MenuItems { get; }

        [GenerateProperty]
        MenuItemViewModelBase _SelectedMenuItem;

        [GenerateProperty]
        DemoModuleViewModel _SelectedDemo;

        [GenerateProperty]
        DemoModuleGroupMenuItem _SelectedDemoModuleGroupMenuItem;

        [GenerateProperty]
        bool isHamburgerMenuPaneMinimized = false;

        [GenerateProperty(SetterAccessModifier = DevExpress.Mvvm.CodeGenerators.AccessModifier.Private)]
        List<DemoModuleViewModel> _NavigationGroup;

        #endregion

        MainViewModel() {
            MenuItems = new List<MenuItemViewModelBase>();
            ProductGroups = new List<ProductGroupViewModel>();
            InitializeDemoModulesAndHamburgerMenuItems();
            InitializeProductGroups();
            SelectedMenuItem = MenuItems[0];            
        }
        public void Init(string args) {
            if(TryInitFromUriScheme(args))
                return;
            NavigationService.Navigate("GetStartedPage", null);
        }
        bool TryInitFromUriScheme(string args) {
            if(!args.StartsWith(UriScheme)) return false;
            string[] pars = args.Remove(0, UriScheme.Length).Replace("%20"," ").Split('/');
            if(pars.Length != 3) return false;
            if(pars[0] != "demos") return false;
            DemoModuleGroupMenuItem group = MenuItems.OfType<DemoModuleGroupMenuItem>().FirstOrDefault(x => x.Title.ToLower() == pars[1].ToLower());
            if(group == null) return false;
            DemoModuleMenuItem item = group.Items.FirstOrDefault(x => x.Title.ToLower() == pars[2].ToLower());
            if(item == null) return false;
            MenuItemClick(item);
            return true;
        }
        void InitializeDemoModulesAndHamburgerMenuItems() {            
            MenuItems.Add(new GetStartedMenuItem(this,
                                                         "Get Started",
                                                          "M 64 16 L 0 128 L 64 240 L 192 240 L 256 128 L 192 16 L 64 16 z M 73.289062 32 L 182.71094 32 L 237.57031 128 L 182.71094 224 L 73.289062 224 L 18.429688 128 L 73.289062 32 z M 80 48 L 34 128 L 80 208 L 112 208 L 66 128 L 112 48 L 80 48 z M 176.91016 49.5 L 160.91016 77.410156 L 190 128 L 160.85938 178.58984 L 176.85938 206.5 L 222 128 L 176.91016 49.5 z"
                                   ));
                        
            foreach(DemoModuleGroup demoModuleGroup in DataModel.DataModel.Current.DemoModuleGroups) {
                var groupItem = new DemoModuleGroupMenuItem(this, demoModuleGroup);
                groupItem.Items.AddRange(demoModuleGroup.DemoModules.Select(x=>new DemoModuleMenuItem(this, groupItem, new DemoModuleViewModel(this, x))));
                MenuItems.Add(groupItem);
            }
        }
        void InitializeProductGroups() {
            foreach(var group in DataModel.DataModel.Current.ProductGroups) {
                ProductGroups.Add(new ProductGroupViewModel(this, group));
            }
        }
        void Navigate(string pageName, string navigationParameter) {
            NavigationService.Navigate(pageName, navigationParameter);
        }
        void UpdateWindowTitleOpacity() => WindowTitleOpacity = SelectedMenuItem is GetStartedMenuItem ? 0 : 1;
        #region command handlers
        [GenerateCommand]
        void ShowNext(object parameter) {
            SelectedDemoModuleGroupMenuItem?.SetNextSelectedIndex();
            SelectedMenuItem = SelectedDemoModuleGroupMenuItem.SelectedItem;
        }
        [GenerateCommand]
        void ShowPrev(object parameter) {
            SelectedDemoModuleGroupMenuItem?.SetPrevSelectedIndex();
            SelectedMenuItem = SelectedDemoModuleGroupMenuItem.SelectedItem;
        }
        [GenerateCommand]
        void MenuItemClick(object parameter) {
            if(parameter == SelectedMenuItem || parameter is DemoModuleGroupMenuItem) return;
            if(parameter is GetStartedMenuItem) {
                SelectedMenuItem = (GetStartedMenuItem)parameter;
                Navigate("GetStartedPage", null);
                return;
            }
            DemoModuleMenuItem moduleItem = parameter as DemoModuleMenuItem;
            if(moduleItem == null) return;

            moduleItem.GroupItem.SelectedIndex = moduleItem.GroupItem.Items.IndexOf(moduleItem);
            
                ExpandGroupCallback?.Invoke(moduleItem.GroupItem);
            if(SelectedMenuItem is GetStartedMenuItem) {
                Navigate("DemoModulePage", null);
            }
            SelectedMenuItem = moduleItem;
            SelectedDemoModuleGroupMenuItem = moduleItem.GroupItem;
            PrevNextEnabled = SelectedDemoModuleGroupMenuItem.Items.Count > 1;
        }
        #endregion
        #region property change handlers
        protected virtual void OnSelectedMenuItemChanged(MenuItemViewModelBase oldValue) {
            UpdateWindowTitleOpacity();
            SelectedDemo = (SelectedMenuItem as DemoModuleMenuItem)?.DemoModuleViewModel;
        }
        protected virtual void OnIsHamburgerMenuMinimizedChanged() {

        }
        #endregion
    }

    public class ProductGroupViewModel {
        public MainViewModel MainViewModel { get; }
        public string Name { get; }
        public string Description { get;  }
        public DemoModuleMenuItem MenuItem { get; }

        public ProductGroupViewModel(MainViewModel viewModel, ProductGroup productGroup) {
            MainViewModel = viewModel;
            Name = productGroup.Name;
            Description = productGroup.Description;
            MenuItem = viewModel.MenuItems.OfType<DemoModuleMenuItem>().Union(viewModel.MenuItems.OfType<DemoModuleGroupMenuItem>().SelectMany(x=>x.Items)).FirstOrDefault(x => x.DemoModuleViewModel.DemoModule.ViewTypeName == productGroup.ViewTypeName);
            if(MenuItem == null) {
                throw new KeyNotFoundException($"Could not found hamburger menu item with demo module name '{productGroup.ViewTypeName}'");
            }
        }
    }
    public class MenuItemViewModelBase : ViewModelBase {
        public MainViewModel MainViewModel { get; }
        public string Title { get; set; }
        public string IconData { get; set; }
        public MenuItemViewModelBase(MainViewModel mainViewModel, string title, string iconData) {
            MainViewModel = mainViewModel;
            Title = title;
            IconData = iconData;
        }
    }
    public class DemoModuleMenuItem : MenuItemViewModelBase {
        public DemoModuleGroupMenuItem GroupItem { get; }
        public DemoModuleViewModel DemoModuleViewModel { get; }
        public DemoModuleMenuItem(MainViewModel viewModel, DemoModuleGroupMenuItem groupItem, DemoModuleViewModel demoModuleViewModel) : base(viewModel, demoModuleViewModel.DemoModule.Title, null) {
            GroupItem = groupItem;
            DemoModuleViewModel = demoModuleViewModel;
        }
    }
    public class DemoModuleGroupMenuItem : MenuItemViewModelBase {
        
        public DemoModuleGroupMenuItem(MainViewModel viewModel, DemoModuleGroup group) : base(viewModel, group.Title, group.Icon) { }

        public int SelectedIndex {
            get => GetValue<int>();
            set => SetValue(value);
        }
        public List<DemoModuleMenuItem> Items { get; } = new List<DemoModuleMenuItem>();
        public DemoModuleMenuItem SelectedItem { get => Items[SelectedIndex]; }

        public void SetNextSelectedIndex() => SelectedIndex = (SelectedIndex + 1) % Items.Count;
        public void SetPrevSelectedIndex() => SelectedIndex = (SelectedIndex - 1 + Items.Count) % Items.Count;
    }
    public class GetStartedMenuItem : MenuItemViewModelBase {
        public GetStartedMenuItem(MainViewModel viewModel, string title, string iconData) : base(viewModel, title, iconData)  {
        }
    }
    public class HamburgerMenuSeparator  {
    }
    public class HamburgerMenuHyperLink {
        public string Title { get; set; }
        public string Uri { get; set; }
        public HamburgerMenuHyperLink() {
        }
    }
}

