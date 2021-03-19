using System;
using System.Threading.Tasks;
using FeatureDemo.View;
using FeatureDemo.ViewModel;
using Windows.ApplicationModel.Activation;
using Microsoft.UI.Xaml;
using DevExpress.Mvvm.Native;
using Windows.ApplicationModel.Core;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;
using Windows.UI.ViewManagement;
namespace FeatureDemo {
    sealed partial class App : Application {
        public App() {
            this.UnhandledException += App_UnhandledException;
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if(localSettings.Values["IsNotFirstRun"] == null) {
                ApplicationView.PreferredLaunchViewSize = new Size(1300, 900);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
                localSettings.Values["IsNotFirstRun"] = true;
            } 
            
            this.InitializeComponent();
        }
        Task<Windows.UI.Popups.IUICommand> task;
        void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e) {
            e.Handled = true;
            if(task != null && !task.IsCompleted)
                return;
            var dialog = new Windows.UI.Popups.MessageDialog(e.Exception.ToString());
            task = dialog.ShowAsync().AsTask();
        }
        MainViewModel viewModel;
        Frame rootFrame;
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) {
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            if(args.UWPLaunchActivatedEventArgs.PreviousExecutionState == ApplicationExecutionState.Running) {
                Window.Current.Activate();                
                return;
            
            }

            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += OnTitleBarLayoutMetricsChanged;
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                        
            viewModel = new MainViewModel();
            
            HamburgerMenuPage rootElement = new HamburgerMenuPage();
            rootFrame = rootElement.RootFrame;
            rootElement.MainViewModel = viewModel;
            rootElement.DataContext = viewModel;
            rootElement.UseLayoutRounding = true;


            if(rootFrame.Content == null) {
                if(!rootFrame.Navigate(typeof(GetStartedPage))) {
                    throw new Exception("Failed to create initial page");
                }
            }
            Window.Current.Content = rootElement;
            Window.Current.Activate();
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetPreferredMinSize(new Windows.Foundation.Size { Width = 500, Height = 500 });            
        }

        private void OnTitleBarLayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args) {
            rootFrame.Margin = new Thickness() { Top = -sender.Height };
        }
    }
}