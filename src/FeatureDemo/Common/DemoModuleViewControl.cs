using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Foundation;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using DevExpress.WinUI.Core.Internal;
using FeatureDemo.ViewModel;
using DevExpress.Mvvm;

namespace FeatureDemo.Common {
    [ContentProperty(Name = nameof(DemoModuleContent))]
    public class DemoModuleView : UserControl {
        #region public props
        public DemoModuleViewTemplateSettings TemplateSettings { get; }
        public UIElement DemoModuleContent { get; set; }
        public UIElement Options { get; set; }
        public bool HideOptionsInitially { get; set; }
        public bool ShowOptionsInOverlay { get; set; }
        public bool HasOptions { get { return Options != null; } }
        public double OptionsPaneWidth { get; set; } = 300;
        #endregion
        #region props
        internal SplitView SplitView { get; set; }
        internal bool IsLoadedEx { get; set; }
        public IList<VisualStateGroup> VisualStateGroups { get; } = new List<VisualStateGroup>();
        #endregion
        #region events
        internal event EventHandler Initialized;        
        #endregion
        public DemoModuleView() {
            TemplateSettings = new DemoModuleViewTemplateSettings();
            Loading += OnLoading;
            Loaded += OnLoaded;
            DataContextChanged += OnDataContextChanged;
        }
        protected virtual void OnLoaded(object sender, RoutedEventArgs e) {
            IsLoadedEx = true;
        }
        private void CopyVisualStateGroups(FrameworkElement element) {
            if(element == null) return;
            IList<VisualStateGroup> elementGroups = VisualStateManager.GetVisualStateGroups(element);
            if(elementGroups.Count == 0 && VisualStateGroups.Count != 0) {
                foreach(VisualStateGroup group in VisualStateGroups) {
                    elementGroups.Add(group);
                }
            }
        }
        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args) {
            FrameworkElement rootContentElement = DemoModuleContent as FrameworkElement;
            if(rootContentElement != null) {
                rootContentElement.DataContext = args.NewValue;
            }
            FrameworkElement optionsRootElement = Options as FrameworkElement;
            if(optionsRootElement != null) {
                optionsRootElement.DataContext = args.NewValue;
            }
        }
        private void OnLoading(FrameworkElement sender, object args) {
            if(Content != null) return;
            UpdateTemplateSettings();
            if(!HasOptions) {
                
                Content = CreateDemoModuleContent();
                CopyVisualStateGroups(Content as FrameworkElement);
            } else {
                SplitView = new SplitView() { PanePlacement = SplitViewPanePlacement.Right, OpenPaneLength = OptionsPaneWidth, PaneBackground = new SolidColorBrush(Colors.Transparent) };
                SplitView.Content = CreateDemoModuleContent();
                SplitView.Pane = CreatePaneContent();
                CopyVisualStateGroups(SplitView);
                Content = SplitView;
                if(DesignMode.DesignModeEnabled) {
                    SplitView.DisplayMode = SplitViewDisplayMode.Inline;
                    SplitView.IsPaneOpen = true;
                }
            }
            if(!DesignMode.DesignModeEnabled)
                Initialized.Invoke(this, null);
        }
        void UpdateTemplateSettings() {
            TemplateSettings.Content = DemoModuleContent;
            TemplateSettings.Options = Options;
        }
        UIElement CreateDemoModuleContent() => new ContentPresenter() { Content = TemplateSettings, ContentTemplate = TemplateSettings.ContentTemplate };
        UIElement CreatePaneContent() => new ContentPresenter() { Content = TemplateSettings, ContentTemplate = TemplateSettings.OptionsTemplate };
    }
    public class DemoSubModuleView : UserControl {
        public UIElement Options { get; set; }
    }
    public class DemoModuleViewTemplateSettings : ViewModelBase {
        public DemoModuleViewModel ViewModel { get; set; }
        public UIElement Content { get; set; }
        public UIElement Options { get; set; }
        public DataTemplate ContentTemplate { get; set; }
        public DataTemplate OptionsTemplate { get; set; }
        public bool AreOptionsInOverlay {
            get => GetProperty<bool>();
            set => SetProperty(value, ()=> AreOptionsInline = !value);
        }
        public bool AreOptionsInline {
            get => GetProperty<bool>();
            private set => SetProperty(value);
        }
    }
}