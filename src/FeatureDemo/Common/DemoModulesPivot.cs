using System;
using DevExpress.WinUI.Core.Internal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FeatureDemo.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core.Extensions;
using Microsoft.UI.Xaml.Data;
using DevExpress.Mvvm;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Input;
using System.Runtime.Serialization;

namespace FeatureDemo.Common {
    public class DemoModulesPivot : ContentControl {
        #region static
        public static readonly DependencyProperty GroupProperty;
        static DemoModulesPivot() {
            DependencyPropertyRegistrator<DemoModulesPivot>.New()
                .Register<DemoModuleGroupMenuItem>(nameof(DemoModuleGroupMenuItem), out GroupProperty, null, (d, oldValue) => d.OnGroupChanged(oldValue));
        }

        #endregion
        #region dep props
        public DemoModuleGroupMenuItem Group {
            get { return (DemoModuleGroupMenuItem)GetValue(GroupProperty); }
            set { SetValue(GroupProperty, value); }
        }
        #endregion
        DemoModulePivotItem PivotItem { get; }
        public DemoModulesPivot() {            
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            Unloaded += OnUnloaded;
            PivotItem = new DemoModulePivotItem(0);
            Content = PivotItem;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e) {
            Content = null;
        }

        protected virtual void OnGroupChanged(DemoModuleGroupMenuItem oldValue) {
            if(oldValue != null) {
                ((INotifyPropertyChanged)oldValue).PropertyChanged -= Group_OnPropertyChanged;
            }
            bool enableSwipe = false;
            if(Group != null) {
                ((INotifyPropertyChanged)Group).PropertyChanged += Group_OnPropertyChanged;
                enableSwipe = Group.Items.Count > 1;
            }

            LoadDemoModule(Group.SelectedItem.DemoModuleViewModel);
        }
        private void Group_OnPropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName != nameof(DemoModuleGroupMenuItem.SelectedIndex)) return;            
            LoadDemoModule(Group.SelectedItem.DemoModuleViewModel);
        }
        
        async void LoadDemoModule(DemoModuleViewModel demoModule) {
            PivotItem.ViewModel = null;
            await Task.Yield();
            PivotItem.ViewModel = demoModule;
        }
    }
    public class DemoModulePivotItem : Control {
        const double MinContentWidth = 546;
        #region static
        public static readonly DependencyProperty IsOptionsPaneOpenProperty;
        public static readonly DependencyProperty OptionsButtonVisibilityProperty;
        public static readonly DependencyProperty IsCodeVisibleProperty;
        public static readonly DependencyProperty IsOptionsButtonEnabledProperty;
        public static readonly DependencyProperty ShowOptionsInContentProperty;

        public static readonly DependencyProperty DemoModuleContentTemplateProperty;
        public static readonly DependencyProperty DemoModuleOptionsPaneTemplateProperty;

        public static readonly DependencyProperty AreOptionsInOverlayProperty;
        public static readonly DependencyProperty AreOptionsInlineProperty;

        static DemoModulePivotItem() {
            DependencyPropertyRegistrator<DemoModulePivotItem>.New()
                .Register<Visibility>(nameof(OptionsButtonVisibility), out OptionsButtonVisibilityProperty, Visibility.Visible)
                .Register<bool>(nameof(IsOptionsPaneOpen), out IsOptionsPaneOpenProperty, true, (s) => s.OnIsOptionsPaneOpenChanged())
                .Register<bool>(nameof(IsCodeVisible), out IsCodeVisibleProperty, false, (s) => s.OnIsCodeVisibleChanged())
                .Register<bool>(nameof(IsOptionsButtonEnabled), out IsOptionsButtonEnabledProperty, true)
                .Register<bool>(nameof(AreOptionsInOverlay), out AreOptionsInOverlayProperty, true)
                .Register<bool>(nameof(AreOptionsInline), out AreOptionsInlineProperty, true)
                .Register<bool>(nameof(ShowOptionsInContent), out ShowOptionsInContentProperty, false, (s) => s.OnShowOptionsInContentChanged())
                .Register<DataTemplate>(nameof(DemoModuleContentTemplate), out DemoModuleContentTemplateProperty, null)
                .Register<DataTemplate>(nameof(DemoModuleOptionsPaneTemplate), out DemoModuleOptionsPaneTemplateProperty, null)
                ;
        }
        #endregion
        #region dep props
        public Visibility OptionsButtonVisibility {
            get { return (Visibility)GetValue(OptionsButtonVisibilityProperty); }
            set { SetValue(OptionsButtonVisibilityProperty, value); }
        }
        public bool IsOptionsPaneOpen {
            get { return (bool)GetValue(IsOptionsPaneOpenProperty); }
            set { SetValue(IsOptionsPaneOpenProperty, value); }
        }
        public bool IsCodeVisible {
            get { return (bool)GetValue(IsCodeVisibleProperty); }
            set { SetValue(IsCodeVisibleProperty, value); }
        }
        public bool IsOptionsButtonEnabled {
            get { return (bool)GetValue(IsOptionsButtonEnabledProperty); }
            set { SetValue(IsOptionsButtonEnabledProperty, value); }
        }
        public bool AreOptionsInOverlay {
            get { return (bool)GetValue(AreOptionsInOverlayProperty); }
            set { SetValue(AreOptionsInOverlayProperty, value); }
        }
        public bool AreOptionsInline {
            get { return (bool)GetValue(AreOptionsInlineProperty); }
            set { SetValue(AreOptionsInlineProperty, value); }
        }
        public bool ShowOptionsInContent {
            get { return (bool)GetValue(ShowOptionsInContentProperty); }
            set { SetValue(ShowOptionsInContentProperty, value); }
        }
        public DataTemplate DemoModuleContentTemplate {
            get { return (DataTemplate)GetValue(DemoModuleContentTemplateProperty); }
            set { SetValue(DemoModuleContentTemplateProperty, value); }
        }
        public DataTemplate DemoModuleOptionsPaneTemplate {
            get { return (DataTemplate)GetValue(DemoModuleOptionsPaneTemplateProperty); }
            set { SetValue(DemoModuleOptionsPaneTemplateProperty, value); }
        }
        #endregion
        public DemoModuleView DemoModuleView { get; set; }
        internal bool PreferedOptionsVisibility { get; set; } = true;
        bool OptionsButtonWasAffected { get; set; }
        public int ContainerIndex { get; }
        public int DemoModuleIndex { get; set; }
        DemoModuleViewModel viewModel;
        public DemoModuleViewModel ViewModel {
            get { return viewModel; }
            set { PropertySetterHelper.SetProperty(ref viewModel, value, OnViewModelChanged); }
        }
        ContentControl DemoModuleContainer { get; set; }
        public DemoModulePivotItem(int containerIndex) {
            ContainerIndex = containerIndex;
            DataContext = null;
            DefaultStyleKey = typeof(DemoModulePivotItem);
            OpacityTransition = new ScalarTransition() { Duration = TimeSpan.FromMilliseconds(300) };
        }
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            DemoModuleContainer = GetTemplateChild("demoModuleContainer") as ContentControl;
            ReloadDemoModule();
        }
        private void OnViewModelChanged(DemoModuleViewModel oldValue) {
            if(DemoModuleContainer == null) return;
            ReloadDemoModule();           
        }
        void ReloadDemoModule() {
            DestroyDemoModuleView();
            CreateDemoModuleView();
        }
        void CreateDemoModuleView() {
            DataContext = ViewModel;
            if(ViewModel == null) return;
            
            TypeInfo moduleTypeInfo = typeof(DemoModuleViewModel).GetTypeInfo().Assembly.DefinedTypes.First((ti) => ti.Name == ViewModel.DemoModule.ViewTypeName);
            DemoModuleView = (DemoModuleView)Activator.CreateInstance(moduleTypeInfo.AsType());
            DemoModuleView.Initialized += OnDemoModuleViewInitialized;
            DemoModuleView.Loaded += OnDemoModuleViewLoaded;
            DemoModuleView.TemplateSettings.ViewModel = ViewModel;
            DemoModuleView.TemplateSettings.ContentTemplate = DemoModuleContentTemplate;
            DemoModuleView.TemplateSettings.OptionsTemplate = DemoModuleOptionsPaneTemplate;
            UpdateAreOptionsInOverlay();
            IsCodeVisible = false;
            IsOptionsButtonEnabled = true;
            DemoModuleContainer.Content = DemoModuleView;
        }

        private void DestroyDemoModuleView() {
            if(DemoModuleView == null) return;
            Opacity = 0;
            DemoModuleView.Initialized -= OnDemoModuleViewInitialized;
            DemoModuleView.SplitView.Do(x => x.PaneClosing -= OnOptionsPaneClosing);
            DemoModuleView.SplitView.Do(x => x.IsPaneOpen = x.DisplayMode == SplitViewDisplayMode.Inline ? x.IsPaneOpen : false);
            if(DemoModuleView.HideOptionsInitially)
                SetIsOptionsPaneOpenSilently(false);
            WindowSizeChangedListener.OnCurrentWindowSizeChanged -= OnCoreWindowSizeChanged;
            DemoModuleContainer.Content = null;
            DemoModuleView = null;
        }
        private void OnOptionsPaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args) {
            SetIsOptionsPaneOpenSilently(false);
        }
        private void UpdateAreOptionsInOverlay() {
            if(DemoModuleView?.TemplateSettings == null) return;
            DemoModuleView.TemplateSettings.AreOptionsInOverlay = DemoModuleView?.SplitView?.DisplayMode != SplitViewDisplayMode.Inline;
            AreOptionsInOverlay = DemoModuleView.TemplateSettings.AreOptionsInOverlay;
            AreOptionsInline = !DemoModuleView.TemplateSettings.AreOptionsInOverlay;
        }
        private void OnDemoModuleViewInitialized(object sender, EventArgs e) {
            OptionsButtonWasAffected = false;
            DemoModuleView.Initialized -= OnDemoModuleViewInitialized;
            UpdateOptionsButtonVisibility();
            if(DemoModuleView.SplitView == null) return;
            DemoModuleView.SplitView.Do(x => x.PaneClosing += OnOptionsPaneClosing);            
            DemoModuleView.SplitView.DisplayMode = GetSplitViewDisplayMode();
            SetIsOptionsPaneOpenSilently(DemoModuleView.SplitView.DisplayMode != SplitViewDisplayMode.Overlay && PreferedOptionsVisibility && !DemoModuleView.HideOptionsInitially);
            DemoModuleView.SplitView.IsPaneOpen = IsOptionsPaneOpen;
            WindowSizeChangedListener.OnCurrentWindowSizeChanged += OnCoreWindowSizeChanged;
            UpdateAreOptionsInOverlay();
        }
        private void OnDemoModuleViewLoaded(object sender, RoutedEventArgs e) {
            DemoModuleView.Loaded -= OnDemoModuleViewLoaded;
            Opacity = 1;
        }
        void UpdateOptionsButtonVisibility() {
            OptionsButtonVisibility = DemoModuleView.HasOptions.ToVisibility();
        }
        private void OnCoreWindowSizeChanged(object sender, WindowSizeChangedEventArgs e) {
            if(DemoModuleView == null || DemoModuleView.SplitView == null) return;
            UpdateLayoutState(e.Size.Width);
        }
        void UpdateLayoutState(double width) {
            SplitViewDisplayMode newMode = GetSplitViewDisplayMode(width);
            if(newMode == DemoModuleView.SplitView.DisplayMode) return;
            DemoModuleView.SplitView.DisplayMode = newMode;
            UpdateAreOptionsInOverlay();
            bool isOptionsPaneOpen = false;
            if(newMode == SplitViewDisplayMode.Inline) {
                isOptionsPaneOpen = OptionsButtonWasAffected ? PreferedOptionsVisibility : PreferedOptionsVisibility && !DemoModuleView.HideOptionsInitially;
            }
            SetIsOptionsPaneOpenSilently(isOptionsPaneOpen);
            DemoModuleView.SplitView.IsPaneOpen = isOptionsPaneOpen;
        }
        SplitViewDisplayMode GetSplitViewDisplayMode(double width = double.NaN) {
            if(DemoModuleView == null || DemoModuleView.SplitView == null) return SplitViewDisplayMode.Inline;
            if(DemoModuleView.ShowOptionsInOverlay) return SplitViewDisplayMode.Overlay;
            if(double.IsNaN(width))
                width = Windows.UI.Core.CoreWindow.GetForCurrentThread().Bounds.Size().Width;
            return width - 44 - DemoModuleView.OptionsPaneWidth <= MinContentWidth ? SplitViewDisplayMode.Overlay : SplitViewDisplayMode.Inline;
        }
        bool suppressIsOptionsPaneOpenChangedLogic;
        private void OnIsCodeVisibleChanged() {
            if(IsCodeVisible && ViewModel != null) {
                ViewModel.InitializeSourceList();
            }
            IsOptionsButtonEnabled = !IsCodeVisible;
        }
        private void OnIsOptionsPaneOpenChanged() {
            if(suppressIsOptionsPaneOpenChangedLogic) return;
            OptionsButtonWasAffected = true;
            if(DemoModuleView.SplitView.DisplayMode == SplitViewDisplayMode.Inline)
                PreferedOptionsVisibility = IsOptionsPaneOpen;
            DemoModuleView.SplitView.IsPaneOpen = IsOptionsPaneOpen;
        }
        private void OnShowOptionsInContentChanged() {
            
        }
        private void SetIsOptionsPaneOpenSilently(bool value) {
            suppressIsOptionsPaneOpenChangedLogic = true;
            IsOptionsPaneOpen = value;
            suppressIsOptionsPaneOpenChangedLogic = false;
        }
    }
}