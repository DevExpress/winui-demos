using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.UI.Xaml.Controls;

namespace FeatureDemo {
    public class DemoItem : BindableBase {
        public string Name {
            get { return GetProperty(() => Name); }
            set { SetProperty(() => Name, value); }
        }

        public string GroupName {
            get { return GetProperty(() => GroupName); }
            set { SetProperty(() => GroupName, value); }
        }

        public override string ToString() {
            return Name;
        }
    }
    public class DXItemsControlViewModel : ViewModelBase {
        public DemoItem SelectedItem {
            get { return GetProperty(() => SelectedItem); }
            set { SetProperty(() => SelectedItem, value); }
        }

        public bool IsGrouping {
            get { return GetProperty(() => IsGrouping); }
            set { SetProperty(() => IsGrouping, value); }
        }

        public ListViewSelectionMode SelectionMode {
            get { return GetProperty(() => SelectionMode); }
            set {
                SetProperty(() => SelectionMode, value, () => {
                    UpdateAllowClearSelection();
                });
            }
        }

        void UpdateAllowClearSelection() {
            if (SelectionMode == ListViewSelectionMode.None) 
                AllowClearSelection = false;
            else 
                AllowClearSelection = SelectionMode == ListViewSelectionMode.Extended || SelectionMode == ListViewSelectionMode.Multiple || (SelectionMode == ListViewSelectionMode.Single && AllowNullSelection == true);
        }

        public bool ShowCheckBoxes {
            get { return GetProperty(() => ShowCheckBoxes); }
            set { SetProperty(() => ShowCheckBoxes, value); }
        }

        public bool ShowSelectAllItem {
            get { return GetProperty(() => ShowSelectAllItem); }
            set { SetProperty(() => ShowSelectAllItem, value); }
        }

        public bool ShowClearSelection {
            get { return GetProperty(() => ShowClearSelection); }
            set { SetProperty(() => ShowClearSelection, value); }
        }

        public bool ShowFooterItems {
            get { return GetProperty(() => ShowFooterItems); }
            set { SetProperty(() => ShowFooterItems, value); }
        }

        public bool AllowNullSelection {
            get { return GetProperty(() => AllowNullSelection); }
            set { SetProperty(() => AllowNullSelection, value, () => { UpdateAllowClearSelection(); }); }
        }

        public bool AllowClearSelection {
            get { return GetProperty(() => AllowClearSelection); }
            set { SetProperty(() => AllowClearSelection, value); }
        }

        public IList<ListViewSelectionMode> SelectionModes { get; private set; } = Enum.GetValues(typeof(ListViewSelectionMode)).Cast<ListViewSelectionMode>().ToList();
        public object ItemsSource { get; private set; } = null;
        public ObservableCollection<object> SelectedItems { get; private set; } = new ObservableCollection<object>();
        public List<DemoItem> Items { get; private set; } = new List<DemoItem>();
        public object GroupedItems { get; private set; }
        public DXItemsControlViewModel() {
            AllowNullSelection = true;
            for (int g = 1; g < 3; g++)
                for (int i = 1; i < 11; i++)
                    Items.Add(new DemoItem() { GroupName = "Group " + g, Name = "Item " + (9 * (g - 1) + i).ToString() });
            GroupedItems = from item in Items
                           group item by item.GroupName;
            SetGroupedSource = new DelegateCommand(() => {
                ItemsSource = GroupedItems;
                RaisePropertyChanged(nameof(ItemsSource));
                IsGrouping = true;
            });
            SetSimpleSource = new DelegateCommand(() => {
                ItemsSource = Items;
                RaisePropertyChanged(nameof(ItemsSource));
                IsGrouping = false;
            });
            ItemsSource = Items;
            SelectionMode = ListViewSelectionMode.Single;
            SelectedItem = Items[0];
        }

        public DelegateCommand SetGroupedSource { get; private set; }
        public DelegateCommand SetSimpleSource { get; private set; }
    }
}