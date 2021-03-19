using System;
using System.Linq;
using DevExpress.Mvvm;
using FeatureDemo.Data;

namespace EditorsDemo {
    public class InputValidationViewModel : ViewModelBase {
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
