using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using FeatureDemo.ViewModel;
using DevExpress.WinUI.Diagnostic.DXVisualizer;
using Windows.UI.ViewManagement;
using Microsoft.UI;

namespace FeatureDemo.View {
    public sealed partial class HamburgerMenuPage : Page {
        public MainViewModel MainViewModel { get; set; }
        public HamburgerMenuPage() {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {           
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
            if(args.InvokedItem == null) return;
            MainViewModel.MenuItemClickCommand.Execute((MenuItemViewModelBase)args.InvokedItemContainer.Tag);
        }

        private void NavigationView_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args) {
            UpdateTitleBarPos(sender, sender.IsPaneOpen);
            UpdateIsHamburgerMenuExpanded(sender);
            MainViewModel.IsHamburgerMenuMinimized = sender.DisplayMode == NavigationViewDisplayMode.Minimal;            
        }
        NavigationViewDisplayMode GetDisplayMode(NavigationView  navView, bool isPaneOpen) {
            if(navView.DisplayMode == NavigationViewDisplayMode.Compact || navView.DisplayMode == NavigationViewDisplayMode.Expanded) {
                if(isPaneOpen) return NavigationViewDisplayMode.Expanded;
                else return NavigationViewDisplayMode.Compact;
            }
            return navView.DisplayMode;
        }
        void UpdateTitleBarPos(NavigationView navView, bool isPaneOpen) {
            var mode = GetDisplayMode(navView, isPaneOpen);
            if(mode == NavigationViewDisplayMode.Expanded || mode == NavigationViewDisplayMode.Minimal) {
                WindowTitleBar.Translation = new System.Numerics.Vector3(0, 0, 0);
            }
            else {
                WindowTitleBar.Translation = new System.Numerics.Vector3(34, 0, 0);
            }
        }

        private void NavigationView_PaneClosing(NavigationView sender, NavigationViewPaneClosingEventArgs args) {
            UpdateTitleBarPos(sender, false);
            UpdateIsHamburgerMenuExpanded(sender);
        }

        private void NavigationView_PaneOpening(NavigationView sender, object args) {
            UpdateTitleBarPos(sender, true);
            UpdateIsHamburgerMenuExpanded(sender);
        }
        private void UpdateIsHamburgerMenuExpanded(NavigationView navView) {
            MainViewModel.IsHamburgerMenuExpanded = navView.IsPaneOpen &&  navView.DisplayMode == NavigationViewDisplayMode.Expanded;
        }
    }
}
