using DevExpress.Data.Filtering;
using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace GridDemo {
    public class TextToCriteriaConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language) {
            FunctionOperator op = value as FunctionOperator;
            if(object.ReferenceEquals(op, null))
                return null;
            return ((OperandValue)op.Operands[1]).Value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language) {
            string text = value as string;
            if(string.IsNullOrEmpty(text))
                return null;
            return new FunctionOperator(FunctionOperatorType.StartsWith, new OperandProperty("City"), text);
        }
    }
    public class DoubleToCriteriaOperatorConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, string language) {
            BinaryOperator op = value as BinaryOperator;
            if(object.ReferenceEquals(op, null))
                return 0d;
            OperandValue operandValue = op.RightOperand as OperandValue;
            return operandValue.Value;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language) {
            return new BinaryOperator("Quantity", Math.Round((double)value), BinaryOperatorType.Greater);
        }
    }
    public class RangeFilter : ContentControl {
        public static readonly DependencyProperty FilterProperty;
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty MinValueProperty;
        public static readonly DependencyProperty MaxValueProperty;
        static RangeFilter() {
            FilterProperty = DependencyProperty.Register("Filter", typeof(GroupOperator), typeof(RangeFilter), new PropertyMetadata(null, (d, e) => ((RangeFilter)d).OnFilterChanged()));
            FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(RangeFilter), new PropertyMetadata(null, (d, e) => ((RangeFilter)d).UpdateFilter()));
            MinValueProperty = DependencyProperty.Register("MinValue", typeof(object), typeof(RangeFilter), new PropertyMetadata(null, (d, e) => ((RangeFilter)d).UpdateFilter()));
            MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(object), typeof(RangeFilter), new PropertyMetadata(null, (d, e) => ((RangeFilter)d).UpdateFilter()));
        }

        public CriteriaOperator Filter {
            get { return (CriteriaOperator)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }
        public string FieldName {
            get { return (string)GetValue(FieldNameProperty); }
            set { SetValue(FieldNameProperty, value); }
        }
        public object MinValue {
            get { return GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public object MaxValue {
            get { return GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        GroupOperator _gFilter { get { return (GroupOperator)Filter; } }
        BinaryOperator _bFilter { get { return (BinaryOperator)Filter; } }
        bool updateFilterLocked = false;
        void OnFilterChanged() {
            updateFilterLocked = true;
            if (Filter is GroupOperator) {
                if (!Equals(Filter, null) && _gFilter.Operands.Count == 2) {
                    BinaryOperator binaryFirstOperator = _gFilter.Operands[0] as BinaryOperator;
                    BinaryOperator binarySecondOperator = _gFilter.Operands[1] as BinaryOperator;
                    if (!ReferenceEquals(binaryFirstOperator, null) && !ReferenceEquals(binarySecondOperator, null)) {
                        MinValue = (binaryFirstOperator.RightOperand as OperandValue).Value;
                        MaxValue = (binarySecondOperator.RightOperand as OperandValue).Value;
                    }
                }
            } else if (Filter is BinaryOperator) {
                if (_bFilter.OperatorType == BinaryOperatorType.GreaterOrEqual) {
                    MinValue = _bFilter.RightOperand;
                    MaxValue = null;
                } else {
                    MinValue = null;
                    MaxValue = _bFilter.RightOperand;
                }
            } else {
                MinValue = null;
                MaxValue = null;
            }
            updateFilterLocked = false;
        }
        void UpdateFilter() {
            if (!updateFilterLocked)
                if (MinValue != null && MaxValue != null) {
                    Filter = (GroupOperator)CriteriaOperator.And(new BinaryOperator(FieldName, MinValue, BinaryOperatorType.GreaterOrEqual), new BinaryOperator(FieldName, MaxValue, BinaryOperatorType.LessOrEqual));
                } else if (MinValue != null) {
                    Filter = new BinaryOperator(FieldName, MinValue, BinaryOperatorType.GreaterOrEqual);
                } else if (MaxValue != null) {
                    Filter = new BinaryOperator(FieldName, MaxValue, BinaryOperatorType.LessOrEqual);
                } else {
                    Filter = null;
                }
        }
    }
}