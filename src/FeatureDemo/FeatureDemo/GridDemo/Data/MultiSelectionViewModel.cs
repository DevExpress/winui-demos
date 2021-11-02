using DevExpress.Mvvm;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using System.Collections.Generic;
using DevExpress.Mvvm.CodeGenerators;

namespace GridDemo {
    [GenerateViewModel]
    public partial class MultiSelectionViewModel {
        public MultiSelectionViewModel() {
            OnIsMultiSelectionEnabledChanged();
            MultiSelectModes = new List<MultiSelectMode>(new[] { MultiSelectMode.None, MultiSelectMode.Row, MultiSelectMode.RowExtended });
            SelectionMode = MultiSelectMode.Row;
            IsMultiSelectionEnabled = true;
        }

        public List<Invoices> Invoices { get; } = NWindData<Invoices>.DataSource;
        public List<MultiSelectMode> MultiSelectModes { get; private set; }
        [GenerateProperty]
        MultiSelectMode _SelectionMode;
        [GenerateProperty]
        bool _IsMultiSelectionEnabled;
        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        string _Caption;

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