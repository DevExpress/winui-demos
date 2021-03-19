using System.Collections.Generic;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using Microsoft.UI.Xaml;
using DevExpress.WinUI.Core.Internal;
using System;
using System.ComponentModel.DataAnnotations;

namespace GridDemo {
    public class ValidationInvoices {
        [MaxLength(5, ErrorMessage = "Customer ID must be 5 - characters long"), MinLength(5, ErrorMessage = "Customer ID must be 5-characters long")]
        public string CustomerID { get; set; }
        [Range(-15000,15000)]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        [MaxLength(12)]
        public string ProductName { get; set; }
        public decimal Freight { get; set; }
    }
    public class GridValidationViewModel : DependencyObject {
        public static readonly DependencyProperty AllowLeaveInvalidEditorProperty;
        public static readonly DependencyProperty InvalidRowExceptionHandleModeProperty;

        static GridValidationViewModel() {
            new DependencyPropertyRegistrator<GridValidationViewModel>()
		        .Register<ExceptionMode>(nameof(InvalidRowExceptionHandleMode), out InvalidRowExceptionHandleModeProperty, ExceptionMode.DisplayError)
                .Register<bool>(nameof(AllowLeaveInvalidEditor), out AllowLeaveInvalidEditorProperty, true)
        ;
        }

		public ExceptionMode InvalidRowExceptionHandleMode {
            get { return (ExceptionMode)GetValue(InvalidRowExceptionHandleModeProperty); }
            set { SetValue(InvalidRowExceptionHandleModeProperty, value); }
        }
        public bool AllowLeaveInvalidEditor {
            get { return (bool)GetValue(AllowLeaveInvalidEditorProperty); }
            set { SetValue(AllowLeaveInvalidEditorProperty, value); }
        }
        public List<ValidationInvoices> Invoices { get; set; } = new List<ValidationInvoices>();

        public GridValidationViewModel() {
            var fullInvoices = NWindData<Invoices>.DataSource;
            Random rand = new Random();
            foreach(Invoices inv in fullInvoices) {
                ValidationInvoices vi = new ValidationInvoices() {
                    CustomerID = inv.CustomerID,
                    OrderDate = inv.OrderDate,
                    RequiredDate = inv.RequiredDate,
                    OrderID = rand.Next() % 7 == 0 ? -inv.OrderID : inv.OrderID,
                    ProductName = rand.Next() % 5 == 0 ? string.Empty : inv.ProductName,
                    Freight = rand.Next() % 11 == 0 ? -inv.Freight : inv.Freight,
                };
                Invoices.Add(vi);
            }
        }
    }
}