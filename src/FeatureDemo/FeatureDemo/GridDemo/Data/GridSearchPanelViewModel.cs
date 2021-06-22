using DevExpress.Mvvm;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using System.Collections.Generic;

namespace GridDemo {
    public partial class GridSearchPanelViewModel : BindableBase {
        public IList<Invoices> Invoices { get; } = NWindData<Invoices>.DataSource;

        public bool IsShowModeDefault { get => GetValue<bool>(); set => SetValue(value, UpdateShowMode); }
        public bool IsShowModeAlways { get => GetValue<bool>(); set => SetValue(value, UpdateShowMode); }
        public bool IsShowModeNever { get => GetValue<bool>(); set => SetValue(value, UpdateShowMode); }

        public bool IsSearchModeAlways { get => GetValue<bool>(); set => SetValue(value, UpdateSearchMode); }
        public bool IsSearchModeFindOnEnter { get => GetValue<bool>(); set => SetValue(value, UpdateSearchMode); }
        
        public bool IsSearchColumnAll { get => GetValue<bool>(); set => SetValue(value, UpdateSearchColumns); }
        public bool IsSearchColumnCountry { get => GetValue<bool>(); set => SetValue(value, UpdateSearchColumns); }
        public bool IsSearchColumnCity { get => GetValue<bool>(); set => SetValue(value, UpdateSearchColumns); }
        public bool IsSearchColumnCountryCity { get => GetValue<bool>(); set => SetValue(value, UpdateSearchColumns); }

        public ShowSearchPanelMode ShowMode { get => GetValue<ShowSearchPanelMode>(); set => SetValue(value); }
        public FindMode SearchMode { get => GetValue<FindMode>(); set => SetValue(value); }
        public string SearchColumns { get => GetValue<string>(); set => SetValue(value); }

        public GridSearchPanelViewModel() {
            IsShowModeDefault = false;
            IsShowModeAlways = true;
            IsShowModeNever = false;
            IsSearchModeAlways = true;
            IsSearchModeFindOnEnter = false;
            IsSearchColumnAll = true;
            IsSearchColumnCountry = false;
            IsSearchColumnCity = false;
            IsSearchColumnCountryCity = false;

            UpdateShowMode();
            UpdateSearchMode();
        }
        void UpdateShowMode() {
            if(IsShowModeDefault)
                ShowMode = ShowSearchPanelMode.Default;
            if(IsShowModeAlways)
                ShowMode = ShowSearchPanelMode.Always;
            if(IsShowModeNever)
                ShowMode = ShowSearchPanelMode.Never;
        }
        void UpdateSearchMode() {
            if(IsSearchModeAlways)
                SearchMode = FindMode.Always;
            if(IsSearchModeFindOnEnter)
                SearchMode = FindMode.FindOnEnter;
        }
        void UpdateSearchColumns() {
            if(IsSearchColumnAll)
                SearchColumns = "*";
            if(IsSearchColumnCountry)
                SearchColumns = "Country";
            if(IsSearchColumnCity)
                SearchColumns = "City";
            if(IsSearchColumnCountryCity)
                SearchColumns = "Country;City";
        }
    }
}
