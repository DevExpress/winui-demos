using DevExpress.Mvvm;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using System.Collections.Generic;
using DevExpress.Mvvm.CodeGenerators;

namespace GridDemo {
    [GenerateViewModel]
    public partial class GridSearchPanelViewModel {
        public IList<Invoices> Invoices { get; } = NWindData<Invoices>.DataSource;

        [GenerateProperty(OnChangedMethod = nameof(UpdateShowMode))]
        bool _IsShowModeDefault;
        [GenerateProperty(OnChangedMethod = nameof(UpdateShowMode))]
        bool _IsShowModeAlways;
        [GenerateProperty(OnChangedMethod = nameof(UpdateShowMode))]
        bool _IsShowModeNever;

        [GenerateProperty(OnChangedMethod = nameof(UpdateSearchMode))]
        bool _IsSearchModeAlways;
        [GenerateProperty(OnChangedMethod = nameof(UpdateSearchMode))]
        bool _IsSearchModeFindOnEnter;

        [GenerateProperty(OnChangedMethod = nameof(UpdateSearchColumns))]
        bool _IsSearchColumnAll;
        [GenerateProperty(OnChangedMethod = nameof(UpdateSearchColumns))]
        bool _IsSearchColumnCountry;
        [GenerateProperty(OnChangedMethod = nameof(UpdateSearchColumns))]
        bool _IsSearchColumnCity;
        [GenerateProperty(OnChangedMethod = nameof(UpdateSearchColumns))]
        bool _IsSearchColumnCountryCity;

        [GenerateProperty]
        ShowSearchPanelMode _ShowMode;
        [GenerateProperty]
        FindMode _SearchMode;
        [GenerateProperty]
        string _SearchColumns;

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
