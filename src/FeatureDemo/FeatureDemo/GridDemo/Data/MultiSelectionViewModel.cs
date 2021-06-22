using DevExpress.Mvvm;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using System.Collections.Generic;

namespace GridDemo {
    public class MultiSelectionViewModel : BindableBase {
        public MultiSelectionViewModel() {
            OnIsMultiSelectionEnabledChanged();
            MultiSelectModes = new List<MultiSelectMode>(new[] { MultiSelectMode.None, MultiSelectMode.Row, MultiSelectMode.RowExtended });
            SelectionMode = MultiSelectMode.Row;
            IsMultiSelectionEnabled = true;
        }

        public List<Invoices> Invoices { get; } = NWindData<Invoices>.DataSource;
        public List<MultiSelectMode> MultiSelectModes { get; private set; }
        public MultiSelectMode SelectionMode { get => GetValue<MultiSelectMode>(); set => SetValue(value); }
        public bool IsMultiSelectionEnabled { get => GetValue<bool>(); set => SetValue(value, OnIsMultiSelectionEnabledChanged); }
        public string Caption { get => GetValue<string>(); private set => SetValue(value); }

        void OnIsMultiSelectionEnabledChanged() {
            UpdateSelectionMode();
            UpdateCaption();
        }
        void UpdateSelectionMode() {
            SelectionMode = IsMultiSelectionEnabled ? MultiSelectMode.Row : MultiSelectMode.None;
        }
        void UpdateCaption() {
            Caption = IsMultiSelectionEnabled ? "Current Selection Total" : "Grand Total";
        }
    }
}