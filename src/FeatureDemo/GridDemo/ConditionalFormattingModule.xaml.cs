using DevExpress.Mvvm;
using DevExpress.WinUI.Grid;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GridDemo {
    public sealed partial class ConditionalFormattingModule : GridDemoUserControl {
        public ConditionalFormattingModule() {
            this.InitializeComponent();
            Unloaded += OnUnloaded;

        }
        protected override void OnLoaded(object sender, RoutedEventArgs e) {
            base.OnLoaded(sender, e);
            DataContext = new FormatConditionEditingViewModel(gridControl.FormatConditions);
        }
        protected internal override GridControl Grid { get { return gridControl; } }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            gridControl.DataContext = null;
            gridRoot.DataContext = null;
        }
        void gridControl_CustomCellAppearance(object sender, CustomCellAppearanceEventArgs e) {
            e.ResultFormat.Foreground = e.CellConditionalFormat?.Foreground ?? e.RowConditionalFormat?.Foreground ?? e.ResultFormat.Foreground;
        }
    }

    public class FormatConditionEditingViewModel : ViewModelBase {
        public ObservableCollection<SaleOverviewData> Data { get; }
        public ObservableCollection<FormatConditionInfo> Infos { get; }
        public FormatConditionInfo SelectedInfo { get { return GetProperty<FormatConditionInfo>(); } set { SetProperty(value); } }
        public ObservableCollection<string> FormatNames { get; }

        readonly FormatConditionCollection baseCollection;
        public FormatConditionEditingViewModel(FormatConditionCollection baseCollection) {
            Data = new ObservableCollection<SaleOverviewData>(SaleOverviewDataGenerator.Sales);
            Infos = new ObservableCollection<FormatConditionInfo>();
            FormatNames = new ObservableCollection<string>() {
                PredefinedFormatNames.LightRedFillWithDarkRedText,
                PredefinedFormatNames.YellowFillWithDarkYellowText,
                PredefinedFormatNames.GreenFillWithDarkGreenText,
                PredefinedFormatNames.LightRedFill,
                PredefinedFormatNames.LightGreenFill,
                PredefinedFormatNames.RedText,
                PredefinedFormatNames.GreenText,
                PredefinedFormatNames.BoldText,
                PredefinedFormatNames.ItalicText,
            };
            this.baseCollection = baseCollection;
            foreach(FormatConditionBase x in baseCollection) {
                var info = new FormatConditionInfo(this, x);
                Infos.Add(info);
            }
            SelectedInfo = Infos.FirstOrDefault();
        }
    }
    public class FormatConditionInfo : ViewModelBase {
        public FormatConditionEditingViewModel ParentViewModel { get; }
        public string Column { get; }
        public string Rule { get; }

        public Format Format { get { return GetProperty<Format>(); } private set { SetProperty(value); } }
        public bool ApplyToRow { get { return GetProperty<bool>(); } set { SetProperty(value, () => formatCondition.ApplyToRow = ApplyToRow); } }
        public bool IsEnabled { get { return GetProperty<bool>(); } set { SetProperty(value, () => formatCondition.IsEnabled = IsEnabled); } }
        public string FormatName { get { return GetProperty<string>(); } set { SetProperty(value, () => { formatCondition.PredefinedFormatName = FormatName; Format = formatCondition.ActualFormat as Format; }); } }

        FormatConditionBase formatCondition;
        public FormatConditionInfo(FormatConditionEditingViewModel parentViewModel, FormatConditionBase formatCondition) {
            ParentViewModel = parentViewModel;
            this.formatCondition = formatCondition;

            Column = formatCondition.FieldName;
            ApplyToRow = formatCondition.ApplyToRow;
            IsEnabled = formatCondition.IsEnabled;
            Rule = ToString(formatCondition);
            FormatName = formatCondition.PredefinedFormatName;
            Format = formatCondition.ActualFormat as Format;
        }

        static string ToString(FormatConditionBase x) {
            if(x is FormatCondition)
                return ToString((FormatCondition)x);
            if(x is TopBottomRuleFormatCondition)
                return ToString((TopBottomRuleFormatCondition)x);
            if(x is UniqueDuplicateRuleFormatCondition)
                return ToString((UniqueDuplicateRuleFormatCondition)x);
            throw new NotImplementedException();
        }
        static string ToString(FormatCondition x) {
            switch(x.ValueRule) {
                case ConditionRule.None: return "None";
                case ConditionRule.Between: return $"[{x.FieldName}] Between ({x.Value1}, {x.Value2})";
                case ConditionRule.NotBetween: return $"Not [{x.FieldName}] Between ({x.Value1}, {x.Value2})";
                case ConditionRule.Equal: return $"[{x.FieldName}] = ({x.Value1})";
                case ConditionRule.NotEqual: return $"[{x.FieldName}] <> ({x.Value1})";
                case ConditionRule.Greater: return $"[{x.FieldName}] > ({x.Value1})";
                case ConditionRule.GreaterOrEqual: return $"[{x.FieldName}] >= ({x.Value1})";
                case ConditionRule.Less: return $"[{x.FieldName}] < ({x.Value1})";
                case ConditionRule.LessOrEqual: return $"[{x.FieldName}] <= ({x.Value1})";
                case ConditionRule.Expression: return x.Expression;
                default: throw new NotImplementedException();
            }
        }
        static string ToString(TopBottomRuleFormatCondition x) {
            switch(x.Rule) {
                case TopBottomRule.TopItems: return $"Top {x.Threshold}";
                case TopBottomRule.BottomItems: return $"Bottom {x.Threshold}";
                case TopBottomRule.TopPercent: return $"Top {x.Threshold}%";
                case TopBottomRule.BottomPercent: return $"Bottom {x.Threshold}%";
                case TopBottomRule.AboveAverage: return $"Above Average";
                case TopBottomRule.BelowAverage: return $"Below Average";
                default: throw new NotImplementedException();
            }
        }
        static string ToString(UniqueDuplicateRuleFormatCondition x) {
            return x.Rule == UniqueDuplicateRule.Unique ? "Unique" : "Duplicate";
        }
    }
    public class FormatValueConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if(value == null)
                return DependencyProperty.UnsetValue;
            if(value is double)
                return ((double)value) < 0 ? 16 : value;
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
