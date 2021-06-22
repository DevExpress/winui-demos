using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core;
using DevExpress.WinUI.Core.Internal;
using FeatureDemo.Common;
using FeatureDemo.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Windows.Foundation;

namespace FeatureDemo.View {
    public sealed partial class MainPage : Page {
        public MainViewModel MainViewModel { get; } = MainViewModel.Instance;
        public MainPage() {
            this.InitializeComponent();
            InitializeMenu();
            RootFrame.Navigate(typeof(GetStartedPage));
            var navService = new DemoNavigationService();
            Interaction.GetBehaviors(RootFrame).Add(navService);
            MainViewModel.NavigationService = navService;
            MainViewModel.ExpandGroupCallback = group => {
                var item = GetMenuItem(group);
                if(item != null && navView.DisplayMode == SplitViewDisplayMode.Inline)
                    item.IsExpanded = true;
            };
            navView.SelectedItemChanged += (d, e) => {
                var vm = navView.SelectedItem.Tag as MenuItemViewModelBase;
                if(vm != null)
                    MainViewModel.MenuItemClickCommand.Execute(vm);
            };
            MainViewModel.PropertyChanged += (d, e) => {
                if(e.PropertyName == nameof(MainViewModel.SelectedMenuItem)) {
                    var menuItem = GetMenuItem(MainViewModel.SelectedMenuItem);
                    if(menuItem != null)
                        menuItem.IsSelected = true;
                }
            };
        }
        void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if(e.PropertyName == nameof(MainViewModel.SelectedMenuItem)) {
                var menuItem = GetMenuItem(MainViewModel.SelectedMenuItem);
                if(menuItem != null)
                    menuItem.IsSelected = true;
            }
        }
        void InitializeMenu() {
            foreach(var item in MainViewModel.MenuItems) {
                if(item is GetStartedMenuItem)
                    navView.Items.Add(CreateMenuItem(item));
                if(item is DemoModuleGroupMenuItem group) {
                    var groupMenuItem = CreateMenuItem(group);
                    navView.Items.Add(groupMenuItem);
                    foreach(var module in group.Items)
                        groupMenuItem.Items.Add(CreateMenuItem(module));
                }
            }
        }
        DXNavigationViewItem CreateMenuItem(MenuItemViewModelBase viewModel) {
            var item = new DXNavigationViewItem() {
                Content = viewModel.Title,
                Tag = viewModel,
            };
            if(!string.IsNullOrEmpty(viewModel.IconData))
                item.Icon = new PathIconView { Data = (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), viewModel.IconData), IconStretch = Stretch.Uniform };
            return item;
        }
        DXNavigationViewItem GetMenuItem(object tag) =>
            GetMenuItems().FirstOrDefault(x => x.Tag?.Equals(tag) == true);
        IEnumerable<DXNavigationViewItem> GetMenuItems() =>
            navView.Items.OfType<DXNavigationViewItem>()
            .Flatten(x => x.Items.OfType<DXNavigationViewItem>());

        
    }

    public class DXNavigationView : ContentControl {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(DXNavigationViewItem), typeof(DXNavigationView),
                new PropertyMetadata(null, (d, e) => ((DXNavigationView)d).OnSelectedItemChanged((DXNavigationViewItem)e.OldValue, (DXNavigationViewItem)e.NewValue)));
        public DXNavigationViewItem SelectedItem { get { return (DXNavigationViewItem)GetValue(SelectedItemProperty); } set { SetValue(SelectedItemProperty, value); } }
        public ObservableCollection<DXNavigationViewItem> Items { get; } = new ObservableCollection<DXNavigationViewItem>();
        public SplitViewDisplayMode DisplayMode { get => PART_SplitView?.DisplayMode ?? SplitViewDisplayMode.Inline; }

        public event EventHandler SelectedItemChanged;

        SplitView PART_SplitView { get; set; }
        DXNavigationViewItem PART_ToggleButton { get; set; }
        bool InlineIsPaneOpen { get; set; } = true;
        const double OverlayThreshold = 800;
        public DXNavigationView() {
            Items.CollectionChanged += OnItemsCollectionChanged;
        }
        protected override void OnApplyTemplate() {
            if(PART_SplitView != null) {
                PART_SplitView.SizeChanged -= OnSplitViewSizeChanged;
                PART_SplitView.PaneClosing -= OnSplitViewPaneClosing;
            }
            if(PART_ToggleButton != null)
                PART_ToggleButton.Click -= OnToggleButtonClick;
            base.OnApplyTemplate();
            PART_SplitView = (SplitView)GetTemplateChild(nameof(PART_SplitView));
            PART_SplitView.SizeChanged += OnSplitViewSizeChanged;
            PART_SplitView.PaneClosing += OnSplitViewPaneClosing;
            PART_ToggleButton = (DXNavigationViewItem)GetTemplateChild(nameof(PART_ToggleButton));
            PART_ToggleButton.Click += OnToggleButtonClick;
        }
        void OnSplitViewPaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args) {
            foreach(var item in Items)
                item.IsExpanded = false;
        }
        void OnSplitViewSizeChanged(object sender, SizeChangedEventArgs e) {
            bool isOverlayMode = e.NewSize.Width < OverlayThreshold;

            if(isOverlayMode && PART_SplitView.DisplayMode != SplitViewDisplayMode.CompactOverlay) {
                PART_SplitView.IsPaneOpen = false;
                PART_SplitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                return;
            }
            if(!isOverlayMode && PART_SplitView.DisplayMode == SplitViewDisplayMode.CompactOverlay) {
                PART_SplitView.IsPaneOpen = InlineIsPaneOpen;
                PART_SplitView.DisplayMode = SplitViewDisplayMode.CompactInline;
            }
        }
        void OnToggleButtonClick(object sender, EventArgs e) {
            if(PART_SplitView == null) return;
            var displayMode = PART_SplitView.DisplayMode;
            switch(displayMode) {
                case SplitViewDisplayMode.CompactInline:
                    PART_SplitView.IsPaneOpen = !PART_SplitView.IsPaneOpen;
                    InlineIsPaneOpen = PART_SplitView.IsPaneOpen;
                    break;
                case SplitViewDisplayMode.CompactOverlay:
                    PART_SplitView.IsPaneOpen = !PART_SplitView.IsPaneOpen;
                    break;
            }
            PART_SplitView.DisplayMode = displayMode;
        }
        void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.OldItems != null) {
                foreach(DXNavigationViewItem item in e.OldItems) {
                    item.PreviewClick -= OnItemPreviewClick;
                    item.Owner = null;
                }
            }
            if(e.NewItems != null) {
                foreach(DXNavigationViewItem item in e.NewItems) {
                    item.Owner = this;
                    item.PreviewClick += OnItemPreviewClick;
                }
            }
        }
        void OnSelectedItemChanged(DXNavigationViewItem oldValue, DXNavigationViewItem newValue) {
            if(oldValue != null)
                oldValue.IsSelected = false;
            if(newValue != null)
                newValue.IsSelected = true;

            var allItems = Items.Flatten(x => x.Items).ToList();
            var i1 = oldValue != null ? allItems.IndexOf(oldValue) : -1;
            var i2 = newValue != null ? allItems.IndexOf(newValue) : -1;
            var p1 = oldValue != null ? LayoutTreeHelper.GetVisualParents(oldValue, this).OfType<DXNavigationViewItemsPresenter>().FirstOrDefault() : null;
            var p2 = newValue != null ? LayoutTreeHelper.GetVisualParents(newValue, this).OfType<DXNavigationViewItemsPresenter>().FirstOrDefault() : null;
            if(p1 == p2) {
                p2?.HighlightItem(newValue);
            }
            else if(i1 > i2) {
                p1?.HideSelectionToBottom();
                p2?.HighlightItem(newValue);
            }
            else {
                p1?.HideSelectionToTop();
                p2?.HighlightItem(newValue);
            }

            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }
        void OnItemPreviewClick(object sender, CancelEventArgs e) {
            if(!PART_SplitView.IsPaneOpen) {
                PART_SplitView.IsPaneOpen = true;
                e.Cancel = true;
            }
        }
    }
    public class DXNavigationViewItem : ContentControl {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(DXNavigationViewItem),
                new PropertyMetadata(false, (d, e) => ((DXNavigationViewItem)d).OnIsSelectedChanged((bool)e.OldValue, (bool)e.NewValue)));
        public static readonly DependencyProperty LevelIndentProperty =
            DependencyProperty.Register("LevelIndent", typeof(double), typeof(DXNavigationViewItem),
                new PropertyMetadata(28d, (d, e) => ((DXNavigationViewItem)d).UpdateLevelIndent()));
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(DXNavigationViewItem),
                new PropertyMetadata(null, (d, e) => ((DXNavigationViewItem)d).UpdateIcon()));
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DXNavigationViewItem),
                new PropertyMetadata(false, (d, e) => ((DXNavigationViewItem)d).OnIsExpandedChanged()));
        public bool IsSelected { get { return (bool)GetValue(IsSelectedProperty); } set { SetValue(IsSelectedProperty, value); } }
        public double LevelIndent { get { return (double)GetValue(LevelIndentProperty); } set { SetValue(LevelIndentProperty, value); } }
        public object Icon { get { return (object)GetValue(IconProperty); } set { SetValue(IconProperty, value); } }
        public bool IsExpanded { get { return (bool)GetValue(IsExpandedProperty); } set { SetValue(IsExpandedProperty, value); } }
        public ObservableCollection<DXNavigationViewItem> Items { get; } = new ObservableCollection<DXNavigationViewItem>();

        public event CancelEventHandler PreviewClick;
        public event EventHandler Click;

        internal DXNavigationView Owner {
            get => owner;
            set {
                if(owner == value) return;
                owner = value;
                OnOwnerChanged();
            }
        }
        int Level {
            get => level;
            set {
                if(level == value) return;
                level = value;
                OnLevelChanged();
            }
        }
        FrameworkElement PART_Content { get; set; }
        FrameworkElement PART_DropDown { get; set; }
        ContentPresenter PART_Icon { get; set; }
        FrameworkElement PART_Indent { get; set; }
        FrameworkElement PART_SelectionIndent { get; set; }
        bool IsPointerOver { get; set; }
        bool IsPointerPressed { get; set; }

        DXNavigationView owner;
        int level = 0;
        public DXNavigationViewItem() {
            Items.CollectionChanged += OnItemsCollectionChanged;
        }

        protected override void OnApplyTemplate() {
            if(PART_Content != null) {
                PART_Content.PointerEntered -= OnContentPointerEntered;
                PART_Content.PointerExited -= OnContentPointerExited;
                PART_Content.PointerPressed -= OnContentPointerPressed;
                PART_Content.PointerReleased -= OnContentPointerReleased;
            }
            base.OnApplyTemplate();
            PART_Content = (FrameworkElement)GetTemplateChild(nameof(PART_Content));
            PART_DropDown = (FrameworkElement)GetTemplateChild(nameof(PART_DropDown));
            PART_Icon = (ContentPresenter)GetTemplateChild(nameof(PART_Icon));
            PART_Indent = (FrameworkElement)GetTemplateChild(nameof(PART_Indent));
            PART_SelectionIndent = (FrameworkElement)GetTemplateChild(nameof(PART_SelectionIndent));
            PART_Content.PointerEntered += OnContentPointerEntered;
            PART_Content.PointerExited += OnContentPointerExited;
            PART_Content.PointerPressed += OnContentPointerPressed;
            PART_Content.PointerReleased += OnContentPointerReleased;
            UpdateDropDown();
            UpdateIcon();
            UpdateLevelIndent();
        }
        void OnContentPointerEntered(object sender, PointerRoutedEventArgs e) {
            IsPointerOver = true;
            UpdateCommonState();
        }
        void OnContentPointerExited(object sender, PointerRoutedEventArgs e) {
            IsPointerOver = false;
            UpdateCommonState();
        }
        void OnContentPointerPressed(object sender, PointerRoutedEventArgs e) {
            IsPointerPressed = true;
            PART_Content.CapturePointer(e.Pointer);
            UpdateCommonState();
            UpdatePressedState();
        }
        void OnContentPointerReleased(object sender, PointerRoutedEventArgs e) {
            IsPointerPressed = false;
            PART_Content.ReleasePointerCapture(e.Pointer);
            UpdateCommonState();
            UpdatePressedState();
            if(IsPointerOver)
                OnClick();
        }
        void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.OldItems != null) {
                foreach(DXNavigationViewItem oldItem in e.OldItems) {
                    oldItem.Level = 0;
                    oldItem.Owner = null;
                }
            }
            if(e.NewItems != null) {
                foreach(DXNavigationViewItem newItem in e.NewItems) {
                    newItem.Level = Level + 1;
                    newItem.Owner = Owner;
                }
            }
            if(Items.Count == 0)
                IsExpanded = false;
            UpdateDropDown();
        }
        void OnIsSelectedChanged(bool oldValue, bool newValue) {
            if(Owner == null) return;
            if(newValue) {
                Owner.SelectedItem = this;
            } else {
                if(Level == 0) return;
                var itemsPresenter = LayoutTreeHelper.GetVisualParents(this).OfType<DXNavigationViewItemsPresenter>().Skip(1).FirstOrDefault();
                if(itemsPresenter == null) return;
                itemsPresenter.HighlightItem(null);
            }
        }
        void OnLevelChanged() {
            UpdateLevelIndent();
            foreach(var item in Items)
                item.Level = Level + 1;
        }
        void OnOwnerChanged() {
            foreach(var item in Items)
                item.Owner = Owner;
            if(IsSelected)
                Owner.SelectedItem = this;
        }
        void OnClick() {
            var argsPreviewClick = new CancelEventArgs();
            PreviewClick?.Invoke(this, argsPreviewClick);
            if(argsPreviewClick.Cancel)
                return;
            ToggleExpand();
            if(Items.Count == 0) {
                IsSelected = true;
            } 
            Click?.Invoke(this, EventArgs.Empty);
        }

        void UpdateIcon() {
            if(PART_Icon == null) return;
            PART_Icon.Visibility = Icon == null ? Visibility.Collapsed : Visibility.Visible;
            PART_Icon.Content = Icon == null ? string.Empty : Icon;
        }
        void UpdateDropDown() {
            if(PART_DropDown == null) return;
            PART_DropDown.Visibility = Items.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        void UpdateLevelIndent() {
            if(PART_Indent != null)
                PART_Indent.Width = LevelIndent * Level;
            if(PART_SelectionIndent != null)
                PART_SelectionIndent.Width = LevelIndent * (Level + 1);
        }

        void ToggleExpand() {
            IsExpanded = Items.Count > 0 ? !IsExpanded : false;
        }
        void UpdateCommonState() {
            var state =
                IsPointerOver
                ? "PointerOver"
                : "Normal";
            VisualStateManager.GoToState(this, state, true);
        }
        void UpdatePressedState() {
            var state =
                IsPointerPressed
                ? "Pressed"
                : "Released";
            VisualStateManager.GoToState(this, state, true);
        }
        void OnIsExpandedChanged() {
            var itemsPresenter = LayoutTreeHelper.GetVisualParents(this).OfType<DXNavigationViewItemsPresenter>().FirstOrDefault();
            if(itemsPresenter == null) return;
            bool isChildSelected = Items.Any(x => x.IsSelected);
            if(!isChildSelected) return;
            if(IsExpanded) {
                itemsPresenter.HighlightItem(null);
            }
            else {
                itemsPresenter.HighlightItem(this);
            }

            UpdateExpandState();
        }
        void UpdateExpandState() {
            var state = IsExpanded ? "Expanded" : "Collapsed";
            VisualStateManager.GoToState(this, state, true);
        }
    }
    [ContentProperty]
    public class DXNavigationViewItemsPresenter : DXPanel {
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable<DXNavigationViewItem>), typeof(DXNavigationViewItemsPresenter),
                new PropertyMetadata(null, (d, e) => ((DXNavigationViewItemsPresenter)d).OnItemsSourceChanged((IEnumerable<DXNavigationViewItem>)e.OldValue, (IEnumerable<DXNavigationViewItem>)e.NewValue)));
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(DXNavigationViewItemsPresenter),
                new PropertyMetadata(true, (d, e) => ((DXNavigationViewItemsPresenter)d).OnIsExpandedChanged()));
        static readonly DependencyProperty AnimatedSelectedIndexProperty =
            DependencyProperty.Register("AnimatedSelectedIndex", typeof(double), typeof(DXNavigationViewItemsPresenter),
                new PropertyMetadata(-1d, (d, e) => {
                    ((DXNavigationViewItemsPresenter)d).InvalidateMeasure();
                    ((DXNavigationViewItemsPresenter)d).InvalidateArrange();
                }));
        static readonly DependencyProperty AnimatedHeightProperty =
            DependencyProperty.Register("AnimatedHeight", typeof(double), typeof(DXNavigationViewItemsPresenter),
                new PropertyMetadata(1d, (d, e) => ((DXNavigationViewItemsPresenter)d).InvalidateMeasure()));
        public IEnumerable<DXNavigationViewItem> ItemsSource { get { return (IEnumerable<DXNavigationViewItem>)GetValue(ItemsSourceProperty); } set { SetValue(ItemsSourceProperty, value); } }
        public bool IsExpanded { get { return (bool)GetValue(IsExpandedProperty); } set { SetValue(IsExpandedProperty, value); } }
        double AnimatedSelectedIndex { get { return (double)GetValue(AnimatedSelectedIndexProperty); } set { SetValue(AnimatedSelectedIndexProperty, value); } }
        double AnimatedHeight { get { return (double)GetValue(AnimatedHeightProperty); } set { SetValue(AnimatedHeightProperty, value); } }

        public UIElement SelectionElement {
            get => selectionElement;
            set {
                if(selectionElement == value) return;
                var oldValue = selectionElement;
                selectionElement = value;
                OnSelectionElementChanged(oldValue, value);
            }
        }
        public DoubleAnimation ExpandAnimation {
            get => expandAnimation;
            set {
                if(expandAnimation == value) return;
                expandAnimation = value;
                OnExpandAnimationChanged();
            }
        }
        public DoubleAnimation SelectionAnimation {
            get => selectionAnimation;
            set {
                if(selectionAnimation == value) return;
                selectionAnimation = value;
                OnSelectionAnimationChanged();
            }
        }
        DXNavigationViewItemsPanel Panel { get; set; }

        UIElement selectionElement;
        DoubleAnimation expandAnimation;
        DoubleAnimation selectionAnimation;
        Storyboard expandStoryboard = new Storyboard();
        Storyboard selectionStoryboard = new Storyboard();
        public DXNavigationViewItemsPresenter() {
            FrameworkElementHelper.SetIsClipped(this, true);
            Panel = new DXNavigationViewItemsPanel();
            Children.Add(Panel);
        }

        public void HighlightItem(DXNavigationViewItem item) {
            var i = ItemsSource == null ? -1 : ItemsSource.IndexOf(x => x == item);
            SetAnimatedSelectedIndex(i);
        }
        public void HideSelectionToTop() {
            SetAnimatedSelectedIndex(-1);
        }
        public void HideSelectionToBottom() {
            SetAnimatedSelectedIndex(-1);
        }

        void OnItemsSourceChanged(IEnumerable<DXNavigationViewItem> oldValue, IEnumerable<DXNavigationViewItem> newValue) {
            Panel.ItemsSource = newValue;
        }
        void OnSelectionElementChanged(UIElement oldValue, UIElement newValue) {
            if(oldValue != null)
                Children.RemoveAt(Children.Count - 1);
            if(newValue != null)
                Children.Add(newValue);
        }
        void OnExpandAnimationChanged() {
            OnAnimationChanged(ExpandAnimation, expandStoryboard);
            Storyboard.SetTarget(ExpandAnimation, this);
            Storyboard.SetTargetProperty(ExpandAnimation, nameof(AnimatedHeight));
        }
        void OnSelectionAnimationChanged() {
            OnAnimationChanged(SelectionAnimation, selectionStoryboard);
            Storyboard.SetTarget(SelectionAnimation, this);
            Storyboard.SetTargetProperty(SelectionAnimation, nameof(AnimatedSelectedIndex));
        }
        void OnAnimationChanged(DoubleAnimation animation, Storyboard storyboard) {
            storyboard.Children.Clear();
            if(animation == null) return;
            animation.EnableDependentAnimation = true;
            storyboard.Children.Add(animation);

        }

        void OnIsExpandedChanged() {
            SetAnimatedHeight(IsExpanded ? 1 : 0);
        }
        void SetAnimatedHeight(double v) {
            if(ExpandAnimation == null) {
                AnimatedHeight = v;
                return;
            }
            ExpandAnimation.To = v;
            expandStoryboard.Begin();
        }
        void SetAnimatedSelectedIndex(double v) {
            if(SelectionAnimation == null || v == -1 || AnimatedSelectedIndex == -1) {
                AnimatedSelectedIndex = v;
                return;
            }
            SelectionAnimation.To = v;
            selectionStoryboard.Begin();
        }

        protected override Size MeasureOverride(Size s) {
            if(Panel == null)
                return new Size();
            Panel.Measure(new Size(s.Width, double.PositiveInfinity));
            var ds = Panel.DesiredSize;
            if(ds.Height > s.Height)
                ds.Height = s.Height;
            ds.Height = ds.Height * AnimatedHeight;

            if(SelectionElement != null)
                SelectionElement.Measure(new Size(s.Width, double.PositiveInfinity));
            return ds;
        }
        protected override Size ArrangeOverride(Size s) {
            if(Panel != null) {
                var ds = Panel.DesiredSize;
                Panel.Arrange(new Rect(new Point(0, 0), new Size(s.Width, ds.Height)));
            }
            if(SelectionElement != null) {
                var ds = SelectionElement.DesiredSize;
                var selected = Panel?.Children.ElementAtOrDefault((int)AnimatedSelectedIndex);
                if(selected == null)
                    SelectionElement.Arrange(new Rect(new Point(-ds.Width, -ds.Height), new Size(ds.Width, ds.Height)));
                else {
                    var y = selected.ActualOffset.Y + selected.ActualSize.Y * (AnimatedSelectedIndex % 1);
                    SelectionElement.Arrange(new Rect(new Point(0, y), new Size(ds.Width, ds.Height)));
                }
            }
            return s;
        }

        class DXNavigationViewItemsPanel : ItemsSourcePanel {
            protected override Size MeasureOverrideCore(Size s) {
                double h = 0;
                double w = 0;
                foreach(var child in Children) {
                    child.Measure(new Size(s.Width, double.PositiveInfinity));
                    var ds = child.DesiredSize;
                    w = Math.Max(w, ds.Width);
                    h += ds.Height;
                }
                if(h > s.Height)
                    h = s.Height;
                return new Size(w, h);
            }
            protected override Size ArrangeOverrideCore(Size s) {
                double y = 0;
                foreach(var child in Children) {
                    var ds = child.DesiredSize;
                    child.Arrange(new Rect(new Point(0, y), new Size(s.Width, ds.Height)));
                    y += ds.Height;
                }
                return s;
            }
        }
    }
}
