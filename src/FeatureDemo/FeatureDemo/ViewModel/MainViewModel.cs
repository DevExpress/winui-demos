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

namespace FeatureDemo.ViewModel {
    public class MainViewModel : NavigationViewModelBase {
        public static MainViewModel Instance { get; } = new MainViewModel();
        #region services
        public IDemoNavigationService NavigationService { get; set; }
        #endregion
        #region commands
        public ICommand ShowPrevCommand { get; }
        public ICommand ShowNextCommand { get; }
        public ICommand MenuItemClickCommand { get; }
        #endregion
        #region properties
        private bool isHamburgerMenuPaneMinimized = false;
        
        private double windowTitleOpacity = 0;
        public double WindowTitleOpacity {
            get { return windowTitleOpacity; }
            set { SetProperty(ref windowTitleOpacity, value, nameof(WindowTitleOpacity)); }
        }
        public bool PrevNextEnabled {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public Action<DemoModuleGroupMenuItem> ExpandGroupCallback { get; set; }
        public List<ProductGroupViewModel> ProductGroups { get; }
        public List<MenuItemViewModelBase> MenuItems { get; }
        public MenuItemViewModelBase SelectedMenuItem {
            get => GetValue<MenuItemViewModelBase>();
            set => SetValue(value, OnSelectedMenuItemChanged);
        }
        public DemoModuleViewModel SelectedDemo {
            get => GetValue<DemoModuleViewModel>();
            set => SetValue(value);
        }
        public DemoModuleGroupMenuItem SelectedDemoModuleGroupMenuItem {
            get => GetValue<DemoModuleGroupMenuItem>();
            set => SetValue(value);
        }
        
        
        
        
        public bool IsHamburgerMenuMinimized {
            get { return isHamburgerMenuPaneMinimized; }
            set { SetProperty(ref isHamburgerMenuPaneMinimized, value, nameof(IsHamburgerMenuMinimized), OnIsHamburgerMenuMinimizedChanged); }
        }
        public List<DemoModuleViewModel> NavigationGroup {
            get => GetValue<List<DemoModuleViewModel>>();
            private set => SetValue(value);
        }
        #endregion
        MainViewModel() {
            MenuItems = new List<MenuItemViewModelBase>();
            ProductGroups = new List<ProductGroupViewModel>();
            ShowNextCommand = new DelegateCommand<object>(ShowNextCommandExecute);
            ShowPrevCommand = new DelegateCommand<object>(ShowPrevCommandExecute);
            MenuItemClickCommand = new DelegateCommand<object>(MenuItemClickCommandExecute);
            InitializeDemoModulesAndHamburgerMenuItems();
            InitializeProductGroups();
            SelectedMenuItem = MenuItems[0];            
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
        void ShowNextCommandExecute(object parameter) {
            SelectedDemoModuleGroupMenuItem?.SetNextSelectedIndex();
            SelectedMenuItem = SelectedDemoModuleGroupMenuItem.SelectedItem;
        }
        void ShowPrevCommandExecute(object parameter) {
            SelectedDemoModuleGroupMenuItem?.SetPrevSelectedIndex();
            SelectedMenuItem = SelectedDemoModuleGroupMenuItem.SelectedItem;
        }
            
        void MenuItemClickCommandExecute(object parameter) {
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

