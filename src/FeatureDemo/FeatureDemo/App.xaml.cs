using DevExpress.WinUI.Core.Internal;
using FeatureDemo.View;
using Microsoft.UI.Xaml;
using System;
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
        Task<IUICommand> task;
        void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e) {
            e.Handled = true;
            if(task != null && !task.IsCompleted)
                return;
            var dialog = new Windows.UI.Popups.MessageDialog(e.Exception.ToString());
            task = dialog.ShowAsync().AsTask();
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args) {
            m_window = new Window() { 
                Title = "DevExpress WinUI Demos " + DevExpress.WinUI.Core.Internal.AssemblyInfo.VersionShort,
                Content = new MainPage(),
            };
            CurrentWindowHelper.SetCurrentWindow(m_window);
            m_window.Activate();
        }
        private Window m_window;
    }
}
