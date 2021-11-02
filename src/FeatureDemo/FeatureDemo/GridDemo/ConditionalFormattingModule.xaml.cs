using DevExpress.Mvvm;
using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Core;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Mvvm.CodeGenerators;

namespace GridDemo {
    public sealed partial class ConditionalFormattingModule : GridDemoUserControl {
        public ConditionalFormattingViewModel ViewModel { get; } = new ConditionalFormattingViewModel();
        public ConditionalFormattingModule() {
            InitializeComponent();
            Loaded += (s, e) => ViewModel.Init(grid.FormatConditions);
        }
        protected internal override GridControl Grid { get { return grid; } }
        void OnCustomCellAppearance(object sender, CustomCellAppearanceEventArgs e) {
            e.ResultFormat.Foreground = e.CellConditionalFormat?.Foreground ?? e.RowConditionalFormat?.Foreground ?? e.ResultFormat.Foreground;
        }
    }

    [GenerateViewModel]
    public partial class ConditionalFormattingViewModel {
        public ObservableCollection<SaleOverviewData> Data { get; }
        public ObservableCollection<string> FormatNames { get; }

        [GenerateProperty]
        ObservableCollection<FormatConditionInfo> _Infos;
        [GenerateProperty]
        FormatConditionInfo _SelectedInfo;

        public ConditionalFormattingViewModel() {
            Data = new ObservableCollection<SaleOverviewData>(SaleOverviewDataGenerator.Sales);
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
        }
        public void Init(FormatConditionCollection baseCollection) {
            var infos = new ObservableCollection<FormatConditionInfo>();
            foreach(FormatConditionBase x in baseCollection)
                infos.Add(new FormatConditionInfo(x));
            Infos = infos;
            SelectedInfo = Infos.FirstOrDefault();
        }
    }

    [GenerateViewModel]
    public partial class FormatConditionInfo {
        public string Column { get; }
        public string Rule { get; }

        [GenerateProperty(SetterAccessModifier = AccessModifier.Private)]
        Format _Format;

        [GenerateProperty]
        bool _ApplyToRow;
        void OnApplyToRowChanged() => formatCondition.ApplyToRow = ApplyToRow;

        [GenerateProperty]
        bool _IsEnabled;
        void OnIsEnabledChanged() => formatCondition.IsEnabled = IsEnabled;

        [GenerateProperty]
        string _FormatName;
        void OnFormatNameChanged() { 
            formatCondition.PredefinedFormatName = FormatName; 
            Format = formatCondition.ActualFormat as Format; 
        }

        FormatConditionBase formatCondition;
        public FormatConditionInfo(FormatConditionBase formatCondition) {
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
    public class FormatApplierBehavior : Behavior<TextBlock> {
        Format format;
        public Format Format { get => format; set { format = value; ApplyFormat(); }  }

        protected override void OnAttached() {
            base.OnAttached();
            ApplyFormat();
        }
        void ApplyFormat() {
            if(AssociatedObject == null || Format == null) return;
            if(Format.Foreground != null)
                AssociatedObject.Foreground = Format.Foreground;
            else AssociatedObject.ClearValue(TextBlock.ForegroundProperty);
            if(Format.FontFamily != null)
                AssociatedObject.FontFamily = Format.FontFamily;
            else AssociatedObject.ClearValue(TextBlock.FontFamilyProperty);
            if(Format.FontSize > 0)
                AssociatedObject.FontSize = Format.FontSize;
            else AssociatedObject.ClearValue(TextBlock.FontSizeProperty);
            AssociatedObject.FontStretch = Format.FontStretch;
            AssociatedObject.FontStyle = Format.FontStyle;
            AssociatedObject.FontWeight = Format.FontWeight;
        }
    }
}
