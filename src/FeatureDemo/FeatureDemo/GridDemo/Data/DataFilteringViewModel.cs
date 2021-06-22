using System.Collections.Generic;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;

namespace GridDemo {
    public class DataFilteringViewModel : DevExpress.Mvvm.BindableBase {
        IList<FilterPopupMode> popupModes = new[] {
            FilterPopupMode.Custom,
            FilterPopupMode.Date,
            FilterPopupMode.DateAlt,
            FilterPopupMode.DateSmart,
            FilterPopupMode.DateCompact,
        };
        FilterPopupMode currentMode = FilterPopupMode.Custom;
        int selectedModeIndex;

        public IList<FilterPopupMode> PopupModes {
            get { return popupModes; }
            set { SetProperty(ref popupModes, value, nameof(PopupModes)); }
        }
        public FilterPopupMode CurrentMode {
            get { return currentMode; }
            set { SetProperty(ref currentMode, value, nameof(CurrentMode)); }
        }
        public int SelectedModeIndex {
            get { return selectedModeIndex; }
            set {
                selectedModeIndex = value;
                OnUpdateCurrentMode();
            }
        }

        void OnUpdateCurrentMode() {
            if(!(SelectedModeIndex >= 0 && SelectedModeIndex < PopupModes.Count)) return;
            CurrentMode = PopupModes[SelectedModeIndex];
        }

        public IList<Invoices> Invoices {
            get { return NWindData<Invoices>.DataSource; }
        }
    }
}
