using System.Collections.Generic;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using Microsoft.UI.Xaml;

namespace GridDemo {
    public class MultiSelectionViewModel : DependencyObject {
        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectionMode", typeof(MultiSelectMode), typeof(MultiSelectionViewModel), new PropertyMetadata(MultiSelectMode.Row));
        public static readonly DependencyProperty IsMultiSelectionEnabledProperty =
            DependencyProperty.Register("IsMultiSelectionEnabled", typeof(bool), typeof(MultiSelectionViewModel), new PropertyMetadata(true, (d, e) => ((MultiSelectionViewModel)d).OnIsMultiSelectionEnabledChanged()));
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(MultiSelectionViewModel), new PropertyMetadata(null));

        public MultiSelectionViewModel() {
            OnIsMultiSelectionEnabledChanged();
            MultiSelectModes = new List<MultiSelectMode>(new[] { MultiSelectMode.None, MultiSelectMode.Row, MultiSelectMode.RowExtended });
        }

        public List<Invoices> Invoices {
            get { return NWindData<Invoices>.DataSource; }
        }

        public MultiSelectMode SelectionMode {
            get { return (MultiSelectMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }
        public bool IsMultiSelectionEnabled {
            get { return (bool)GetValue(IsMultiSelectionEnabledProperty); }
            set { SetValue(IsMultiSelectionEnabledProperty, value); }
        }
        public string Caption {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        public List<MultiSelectMode> MultiSelectModes { get; set; }

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