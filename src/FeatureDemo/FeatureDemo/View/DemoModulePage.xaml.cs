using DevExpress.WinUI.Core.Internal;
using FeatureDemo.Common;
using FeatureDemo.ViewModel;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FeatureDemo.View {
    public sealed partial class DemoModulePage : Page {
        public static readonly DependencyProperty DemoModuleViewProperty =
            DependencyProperty.Register("DemoModuleView", typeof(DemoModuleView), typeof(DemoModulePage), new PropertyMetadata(null));
        
        public static readonly DependencyProperty IsOptionsPaneOpenProperty = 
            DependencyProperty.Register(nameof(IsOptionsPaneOpen), typeof(bool), typeof(DemoModulePage), new PropertyMetadata(false));
        public static readonly DependencyProperty IsCodeVisibleProperty = 
            DependencyProperty.Register(nameof(IsCodeVisible), typeof(bool), typeof(DemoModulePage), new PropertyMetadata(false, 
                (d, e) => ((DemoModulePage)d).OnIsCodeVisibleChanged((bool)e.OldValue, (bool)e.NewValue)));
        
        public DemoModuleView DemoModuleView { get => (DemoModuleView)GetValue(DemoModuleViewProperty); private set => SetValue(DemoModuleViewProperty, value); }
        public bool IsOptionsPaneOpen { get => (bool)GetValue(IsOptionsPaneOpenProperty); set => SetValue(IsOptionsPaneOpenProperty, value); }
        public bool IsCodeVisible { get => (bool)GetValue(IsCodeVisibleProperty);  set => SetValue(IsCodeVisibleProperty, value); }
        
        public MainViewModel MainViewModel { get; } = MainViewModel.Instance;
        public DemoModulePage() {
            InitializeComponent();
            CurrentWindowHelper.CurrentWindow.SizeChanged += OnCurrentWindowSizeChanged;
            MainViewModel.PropertyChanged += OnMainViewModelPropertyChanged;
            LoadDemo();
        }
        void OnMainViewModelPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(MainViewModel.SelectedDemo))
                LoadDemo();
        }
        void OnCurrentWindowSizeChanged(object sender, WindowSizeChangedEventArgs args) {
            UpdateSplitViewDisplayMode();
        }

        void LoadDemo() {
            demoContainer.Opacity = 0;
            IsOptionsPaneOpen = false;
            IsCodeVisible = false;
            indicator.Visibility = Visibility.Visible;
            DemoModuleView = null;

            loadDemoCancellationTokenSource?.Cancel();
            loadDemoCancellationTokenSource = new CancellationTokenSource();
            LoadDemoAsync(loadDemoCancellationTokenSource.Token);
        }
        CancellationTokenSource loadDemoCancellationTokenSource;
        async void LoadDemoAsync(CancellationToken cancellationToken) {
            await Task.Delay(TimeSpan.FromSeconds(0.25));
            await DispatcherQueueHelper.RunAsync(DispatcherQueuePriority.Low, () => { });
            await DispatcherQueueHelper.RunAsync(DispatcherQueuePriority.Low, () => {
                if(cancellationToken.IsCancellationRequested) return;
                GC.GetTotalMemory(true);
            });
            await DispatcherQueueHelper.RunAsync(DispatcherQueuePriority.Low, () => {
                if(cancellationToken.IsCancellationRequested) return;
                if(MainViewModel.SelectedDemo == null)
                    return;
                TypeInfo moduleTypeInfo = typeof(DemoModuleViewModel).GetTypeInfo().Assembly.DefinedTypes.First((ti) => ti.Name == MainViewModel.SelectedDemo.DemoModule.ViewTypeName);
                DemoModuleView = (DemoModuleView)Activator.CreateInstance(moduleTypeInfo.AsType());
                IsOptionsPaneOpen = DemoModuleView.HasOptions && !DemoModuleView.HideOptionsInitially && !DemoModuleView.ShowOptionsInOverlay;
                UpdateSplitViewDisplayMode();
            });
            await DispatcherQueueHelper.RunAsync(DispatcherQueuePriority.Low, () => {
                if(cancellationToken.IsCancellationRequested) return;
                demoContainer.Opacity = 1;
                indicator.Visibility = Visibility.Collapsed;
            });
        }

        void OnIsCodeVisibleChanged(bool oldValue, bool newValue) {
            if(newValue && MainViewModel.SelectedDemo != null)
                MainViewModel.SelectedDemo.InitializeSourceList();
        }
        void UpdateSplitViewDisplayMode() =>
            splitView.DisplayMode = GetSplitViewDisplayMode();
        SplitViewDisplayMode GetSplitViewDisplayMode() {
            if(DemoModuleView == null || !DemoModuleView.HasOptions) return SplitViewDisplayMode.Inline;
            if(DemoModuleView.ShowOptionsInOverlay) return SplitViewDisplayMode.Overlay;
            var width = CurrentWindowHelper.CurrentWindow.Bounds.Width;
            return width - 44 - DemoModuleView.OptionsPaneWidth <= 546 ? SplitViewDisplayMode.Overlay : SplitViewDisplayMode.Inline;
        }

        void ToggleTheme() {
            var element = CurrentWindowHelper.CurrentWindow.Content as FrameworkElement;
            element.RequestedTheme = element.ActualTheme == ElementTheme.Light ? ElementTheme.Dark : ElementTheme.Light;
        }
    }
}
