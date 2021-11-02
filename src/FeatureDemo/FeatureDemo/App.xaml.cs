using DevExpress.WinUI.Core.Internal;
using FeatureDemo.View;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;

namespace FeatureDemo {
    public partial class App : Application {
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

        public Window MainWindow { get; private set; }

        Task<IUICommand> task;
        void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e) {
            e.Handled = true;
            if(task != null && !task.IsCompleted)
                return;
            var dialog = new Windows.UI.Popups.MessageDialog(e.Exception.ToString());
            task = dialog.ShowAsync().AsTask();
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) {
            MainWindow = new Window() { 
                Title = "DevExpress WinUI Demos " + DevExpress.WinUI.Core.Internal.AssemblyInfo.VersionShort,
                Content = new MainPage() { Args = string.Join(" ", Environment.GetCommandLineArgs().Skip(1)) }
            };
            WindowIconHelper.AttachIcon(MainWindow, "app-icon-active-dark.ico", "app-icon-active-light.ico", "app-icon-inactive.ico");
            MainWindow.Activate();
        }       
    }
}
