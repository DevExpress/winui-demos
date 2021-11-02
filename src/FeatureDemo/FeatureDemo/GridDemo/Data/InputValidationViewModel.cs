using System.Collections.Generic;
using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using Microsoft.UI.Xaml;
using DevExpress.WinUI.Core.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.CodeGenerators;

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
    [GenerateViewModel]
    public partial class InputValidationViewModel {
        [GenerateProperty]
        bool _AllowLeaveInvalidEditor;

        public List<ValidationInvoices> Invoices { get; } = new List<ValidationInvoices>();

        public InputValidationViewModel() {
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
            AllowLeaveInvalidEditor = true;
        }
    }
}