using System;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.WinUI.Editors;
using FeatureDemo.Common;
using FeatureDemo.Data;



namespace EditorsDemo {
    public sealed partial class InputValidationModule : DemoModuleView {
        public InputValidationModule() {
            ViewModel = new InputValidationViewModel();
            this.InitializeComponent();
        }

        public InputValidationViewModel ViewModel { get; }

        void ValidateCritical(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (e.Value == null) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "The value cannot be empty.";
            }

        }
        void ValidateInfo(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (e.Value == null) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Information;
                e.ErrorContent = "The value is empty.";
            }
        }
        void ValidateTitle(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
             if(string.IsNullOrEmpty(e.Value?.ToString())) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Warning;
                e.ErrorContent = "The job title is empty.";
            }
        }
        void ValidateHireDate(object sender, DevExpress.WinUI.Editors.ValidationEventArgs e) {
            if (e.Value is DateTime date && date > DateTime.Now) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Warning;
                e.ErrorContent = "The hire date is greater than today.";
            }
            else if(e.Value == null) {
                e.Handled = true;
                e.IsValid = false;
                e.ErrorType = ErrorType.Critical;
                e.ErrorContent = "The hire date cannot be empty.";
            }
        }
    }

    public class InputValidationViewModel {
        public InputValidationViewModel() {
            var employee = new DataStorage().Employees.ElementAt(1);
            Employee = new Employee() {
                Id = employee.Id,
                ParentId = employee.ParentId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                JobTitle = employee.JobTitle,
                Phone = employee.Phone,
                EmailAddress = employee.EmailAddress,
                AddressLine1 = employee.AddressLine1,
                City = employee.City,
                PostalCode = employee.PostalCode,
                CountryRegionName = employee.CountryRegionName,
                GroupName = employee.GroupName,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Gender = employee.Gender,
                MaritalStatus = employee.MaritalStatus,
                Title = employee.Title,
                ImageData = employee.ImageData,
            };
            Employee.LastName = null;
            Employee.JobTitle = null;
            Employee.HireDate = DateTime.Now.AddMonths(1);
            Employee.Phone = null;
        }

        public Employee Employee { get; }
    }
}
